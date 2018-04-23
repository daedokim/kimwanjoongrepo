using System;
using com.dug.UI.model;
using com.dug.UI.dto;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.manager;

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

        [HideInInspector]
        private UICard firstCard = null;
        [HideInInspector]
        private UICard secondCard = null;


        private UITimeline timeline = null;

        
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

            model.ObserveEveryValueChanged(x => x.status).Subscribe(_ =>
            {
                if (model.status != GamePlayerModel.GamePlayerState.Stand)
                {
                    this.gameObject.SetActive(true);
                }
            });

            model.ObserveEveryValueChanged(x => x.nickName).Subscribe(_ =>
                   {
                       nameText.text = model.nickName;
                   });

            model.ObserveEveryValueChanged(x => x.lastBetType).Subscribe(_ =>
                {
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

            model.ObserveEveryValueChanged(x => x.userIndex).Where(x => x == 0).Subscribe(_ =>
                {
                    if (model.userIndex == 0)
                        ClearUI();
                });

            model.ObserveEveryValueChanged(x => x.stage).Where(x => x != 0).Subscribe(_ =>
            {
                GameManager.Instance.GameEvent.InvokeGamePlayerActionEvent(model);   
            });
        }

        public void SetTimeLine(RuntimeAnimatorController controller)
        {
            timeline.Draw(controller, chairIndex);
        }

        private void CreatePlayerCard()
        {
            firstCard = CardManager.Instance.GetCards(model.card1);
            firstCard.transform.SetParent(transform);
            firstCard.transform.localPosition = new Vector2(80.4f, 0);
            firstCard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            firstCard.gameObject.SetActive(true);

            secondCard = CardManager.Instance.GetCards(model.card2);
            secondCard.transform.SetParent(transform);
            secondCard.transform.localPosition = new Vector2(141.7f, 0);
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
                ClearUI();
            }
        }

        private void ClearUI()
        {
            nameText.text = "";
            betTypeText.text = "";

            picture.color = Color.white;
        }

        public void ShowOwnCard()
        {
            CreatePlayerCard();

            firstCard.SetFace(true);
            secondCard.SetFace(true);
        }

    }
}

