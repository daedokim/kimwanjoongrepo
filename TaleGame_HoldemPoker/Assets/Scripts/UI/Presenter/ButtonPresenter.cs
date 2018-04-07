using com.dug.UI.model;
using com.dug.UI.view;
using com.dug.UI.manager;
using UnityEngine;
using UniRx;
using com.dug.UI.events;
using com.dug.UI.dto;
using System;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class ButtonPresenter : IPresenter
    {
        private ButtonView view;
        private GameManager manager;
        private GamePlayerModel currentGamePlayer;


        public ButtonPresenter(ButtonView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            if(view != null)
            {
                this.view.OnCallButtonClicked.Subscribe(_ =>
                {
                    view.EnableAllButtons(false);
                    manager.OnCall(currentGamePlayer.userIndex, currentGamePlayer.stageBet);
                });

                this.view.OnRaiseButtonClicked.Subscribe(_ =>
                {
                    view.EnableAllButtons(false);
                    manager.OnRaise(currentGamePlayer.userIndex, currentGamePlayer.stageBet, 1000);
                }); 

                this.view.OnCheckButtonClicked.Subscribe(_ =>
                {
                    view.EnableAllButtons(false);
                    manager.OnCheck(currentGamePlayer.userIndex);
                });

                this.view.OnFoldButtonClicked.Subscribe(_ =>
                {
                    view.EnableAllButtons(false);
                    manager.OnFold(currentGamePlayer.userIndex);
                });

                this.view.OnAllinButtonClicked.Subscribe(_ =>
                {
                    view.EnableAllButtons(false);
                    manager.OnAllIn(currentGamePlayer.userIndex, currentGamePlayer.stageBet, currentGamePlayer.buyInLeft);
                });

                GameEvent.Instance.AddPlayerTurnEvent(OnPlayerTurnEvent);
;            }
        }

        private void OnPlayerTurnEvent(GamePlayerModel model)
        {
            currentGamePlayer = model;
            bool isBlind = currentGamePlayer.roomStage == (int)Stage.PreFlop && currentGamePlayer.betCount <= 1;
            if (isBlind == false && model.lastBetType != GamePlayerModel.BetType.Allin && model.lastBetType != GamePlayerModel.BetType.Fold)
            {
                view.EnableAllButtons(true);

                if(model.betCount > 0)
                {
                    view.EnableCheckButton(false);
                }
            }
            else
            {
                view.EnableAllButtons(false);
            }
        }
    }

}
