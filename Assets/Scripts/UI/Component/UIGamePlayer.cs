using System;
using com.dug.UI.model;
using com.dug.UI.dto;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.manager;
using com.dug.UI.view;
using com.dug.UI.events;

namespace com.dug.UI.component
{
    public class UIGamePlayer : MonoBehaviour
    {
        [SerializeField]
        public Text nameText = null;
        [SerializeField]
        public Text betTypeText = null;
        [SerializeField]
        public Image picture = null;
        [SerializeField]
        public GameObject timeLineGameObject = null;
        [SerializeField]
        private Image handObject = null;
        [SerializeField]
        private GameObject winnerObject = null;
        [SerializeField]
        private GameObject winnerEffect= null;
        [SerializeField]
        private GameObject dealearMark = null;


        [HideInInspector]
        private UICard firstCard = null;
        [HideInInspector]
        private UICard secondCard = null;
        [HideInInspector]
        private UITimeline timeline = null;
        [HideInInspector]
        public GamePlayersView view = null;

        public int chairIndex = -1;

        GamePlayerModel model = new GamePlayerModel();


        public GamePlayerModel Model {get {return model;} }
         
        public void UpdateGamePlayer(GamePlayerModel model)
        {
            this.model.Update(model);
        }

        public int ChairIndex
        {
            set
            {
                this.chairIndex = value;
            }
        }

        void Awake()
        {

            timeline = timeLineGameObject.GetComponent<UITimeline>();

            model.ObserveEveryValueChanged(x => x.status).Skip(1).Subscribe(_ =>
            {
                this.gameObject.SetActive(model.status != GamePlayerModel.GamePlayerState.Stand);
            });

            model.ObserveEveryValueChanged(x => x.nickName).Subscribe(_ =>
                   {
                       nameText.text = model.nickName;
                   });

            model.ObserveEveryValueChanged(x => x.lastBetType).Subscribe(_ =>
                {
                    if (model.lastBetType == 0)
                        return;

                    betTypeText.text = GetBetTypeName(model.lastBetType);
                }); 

            model.ObserveEveryValueChanged(x => x.isMyTurn).Subscribe(x =>
              {
                  if (x == true)
                  {
                      picture.color = Color.black;

                      timeline.StartCountDown();
                  }
                  else
                  {
                      picture.color = Color.white;
                      timeline.StopCountDown();
                  }
              });

            model.ObserveEveryValueChanged(x => x.stageBet).Subscribe(x => {
                if(x > 0)
                {
                    if (model.lastBetType != GamePlayerModel.BetType.Fold && model.lastBetType != GamePlayerModel.BetType.Check)
                    {
                        GameEvent.Instance.InvokeChipsEvent(chairIndex, x);
                    }
                }
            });

            model.ObserveEveryValueChanged(x => x.lastBetType).Where(x => x == GamePlayerModel.BetType.Fold).Subscribe(x => {
                GameEvent.Instance.InvokeFoldEvent(model.chairIndex);
            });

            model.ObserveEveryValueChanged(x => x.roomStage).Where(x => x == 14 || x == 15).Subscribe(x => {
                ShowHandResult(x);
            });
        }

        public void ShowDelarMark()
        {
            dealearMark.SetActive(GameManager.Instance.Room.dealerChairIndex == chairIndex);
        }

        public void SetTimeLine(RuntimeAnimatorController controller)
        {
            timeline.Draw(controller, chairIndex);
        }

        private void CreatePlayerCard()
        {
            firstCard = CardManager.Instance.GetCards(model.card1);
            firstCard.transform.SetParent(transform);
            firstCard.transform.localPosition = new Vector2(90.8f, -17f);
            firstCard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            firstCard.gameObject.SetActive(true);

            secondCard = CardManager.Instance.GetCards(model.card2);
            secondCard.transform.SetParent(transform);
            secondCard.transform.localPosition = new Vector2(153f, -17f);
            secondCard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            secondCard.gameObject.SetActive(true);
        }

        private string GetBetTypeName(GamePlayerModel.BetType lastBetType)
        {
            string betName = "";
            if (model.roomStage == (int)Stage.PreFlop && model.betCount == 1 && model.lastBetType == GamePlayerModel.BetType.Raise)
            {
                betName = "SB";
            }
            else if (model.roomStage == (int)Stage.PreFlop && model.betCount == 2 && model.lastBetType == GamePlayerModel.BetType.Raise)
            {
                betName = "BB";
            }
            else
            {
                if(model.betCount == 1 && model.lastBetType == GamePlayerModel.BetType.Raise)
                {
                    betName = "Betting";
                }
                else
                {
                    betName = lastBetType.ToString();
                }
                
            }
            return betName;
        }

        public void ResetGamePlayer()
        {
            this.gameObject.SetActive(false);
        }

        private void OnUpdateUI()
        {
            if (model.userIndex > 0)
            {
                nameText.text = model.nickName;
                betTypeText.text = model.lastBetType.ToString();
            }
            else
            {
                Clear();
            }
        }

        public void Clear()
        {
            picture.color = Color.white;

            winnerEffect.SetActive(false);
            winnerObject.SetActive(false);
            handObject.gameObject.SetActive(false);
            betTypeText.text = "";


            if (firstCard != null)
                firstCard.gameObject.SetActive(false);

            if(secondCard != null)
                secondCard.gameObject.SetActive(false);

            dealearMark.SetActive(false);

        }

        public void ShowOwnCard()
        {
            CreatePlayerCard();

            firstCard.SetFace(true);
            secondCard.SetFace(true);
        }

        private void ShowHandResult(int stage)
        {
            if (this.model.isWinner == true)
            {
                winnerObject.SetActive(true);
                winnerEffect.SetActive(true);
            }

            if(stage == (int)RoomModel.Stage.Winner)
            {
                handObject.gameObject.SetActive(true);
                handObject.sprite = view.GetHandSprite((int)this.model.result.handType);

                if (firstCard == null && secondCard == null)
                    ShowOwnCard();

                ShowCardResult();
            }
        }

        private void ShowCardResult()
        {
            int[] madeCards = this.model.result.madeCards;

            firstCard.SetAlpha(0.3f);
            secondCard.SetAlpha(0.3f);

            firstCard.transform.localPosition = new Vector2(firstCard.transform.localPosition.x, firstCard.transform.localPosition.y - 10);
            secondCard.transform.localPosition = new Vector2(secondCard.transform.localPosition.x, secondCard.transform.localPosition.y - 10);


            if (this.model.isWinner == true)
            {
                for (int i = 0; i < madeCards.Length; i++)
                {
                    if (this.model.card1 == madeCards[i])
                    {
                        firstCard.transform.localPosition = new Vector2(firstCard.transform.localPosition.x, firstCard.transform.localPosition.y + 10);
                        firstCard.SetAlpha(1f);
                    }
                    if (this.model.card2 == madeCards[i])
                    {
                        secondCard.transform.localPosition = new Vector2(secondCard.transform.localPosition.x, secondCard.transform.localPosition.y + 10);
                        secondCard.SetAlpha(1f);
                    }
                }
            }

        }

    }
}

