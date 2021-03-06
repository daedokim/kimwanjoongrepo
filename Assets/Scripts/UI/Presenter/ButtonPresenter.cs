﻿using com.dug.UI.Models;
using com.dug.UI.view;
using com.dug.UI.Managers;
using UnityEngine;
using UniRx;
using com.dug.UI.Events;
using com.dug.UI.DTO;
using System;
using com.dug.UI.Networks;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class ButtonPresenter : IPresenter
    {
        private ButtonView view;
        private GameManager manager;
        private GamePlayerModel currentGamePlayer;
        private bool isClickable = false;
        private long raiseBetAmount = 0;

        public ButtonPresenter(ButtonView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.ObserveEveryValueChanged(x => x.isClickable).Subscribe(x => {

                if(x == false)
                {
                    view.EnableAllButtons(false);
                }
                
            });

            this.view.OnCallButtonClicked.Where(_=> isClickable).Subscribe(x =>
            {
                manager.OnCall(currentGamePlayer.userIndex, currentGamePlayer.stageBet);
                isClickable = false;
            });

            this.view.OnCall2ButtonClicked.Where(_ => isClickable).Subscribe(x =>
            {
                manager.OnCall(currentGamePlayer.userIndex, currentGamePlayer.stageBet);
                isClickable = false;
            });



            this.view.OnCallButtonClicked.Where(_ => isClickable).Subscribe(x =>
            {
                if (currentGamePlayer.buyInLeft <= raiseBetAmount)
                    manager.OnAllIn(currentGamePlayer.userIndex, currentGamePlayer.stageBet, 0);
                else
                    manager.OnCall(currentGamePlayer.userIndex, currentGamePlayer.stageBet);
                isClickable = false;
            });

            this.view.OnRaiseButtonClicked.Where(_ => isClickable).Subscribe(_ =>
            {
                if(currentGamePlayer.buyInLeft <= raiseBetAmount)
                    manager.OnAllIn(currentGamePlayer.userIndex, currentGamePlayer.stageBet, currentGamePlayer.buyInLeft);
                else
                    manager.OnRaise(currentGamePlayer.userIndex, currentGamePlayer.stageBet, raiseBetAmount);

                isClickable = false;
            }); 

            this.view.OnCheckButtonClicked.Where(_ => isClickable).Subscribe(_ =>
            {
                manager.OnCheck(currentGamePlayer.userIndex);
                isClickable = false;
            });

            this.view.OnFoldButtonClicked.Where(_ => isClickable).Subscribe(_ =>
            {
                manager.OnFold(currentGamePlayer.userIndex);
                isClickable = false;
            });

            this.view.OnToLobbyButtonClicked.Subscribe(_ => {
                manager.StandUp(UserData.Instance.userIndex);
                
            });

            this.view.OnStandUpButtonClicked.Subscribe(_ => {
                manager.StandUp(UserData.Instance.userIndex);

            });

            GameEvent.Instance.AddPlayerTurnEvent(OnPlayerTurnEvent);
            GameEvent.Instance.AddClearEvent(OnClearAll);
        }

        private void OnClearAll()
        {
            view.EnableAllButtons(false);
            raiseBetAmount = 0;
            isClickable = false;
        }

        private void OnPlayerTurnEvent(GamePlayerModel model)
        {
            currentGamePlayer = model;
            bool isBlind = currentGamePlayer.roomStage == (int)Stage.PreFlop && currentGamePlayer.betCount <= 1;
            if (isBlind == false && model.lastBetType != GamePlayerModel.BetType.Allin && model.lastBetType != GamePlayerModel.BetType.Fold)
            {
                view.SetCallButton(model.betCount != 0, manager.Room.stageBet - model.stageBet);

                view.EnableAllButtons(true);

                view.EnableCallButton(model.betCount != 0);
                view.EnableCheckButton(model.betCount == 0);
                
                isClickable = true;
            }
            else
            {
                view.EnableAllButtons(false);
            }
        }

        public void SetRaiseBetAmount(long amount)
        {
            raiseBetAmount = amount;
        }
    }

}
