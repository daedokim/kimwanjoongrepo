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
        public Text nameText = null;
        public Text betTypeText = null;
        public Image picture = null;

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
                  }
                  else
                  {
                      picture.color = Color.white;
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
    }
}

