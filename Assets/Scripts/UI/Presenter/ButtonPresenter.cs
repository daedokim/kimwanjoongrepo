﻿using com.dug.UI.model;
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
        private bool isClickable = false;


        public ButtonPresenter(ButtonView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.view.OnCallButtonClicked.Where(_=> isClickable).Subscribe(x =>
            {
                manager.OnCall(currentGamePlayer.userIndex, currentGamePlayer.stageBet);
                isClickable = false;
            });

            this.view.OnRaiseButtonClicked.Where(_ => isClickable).Subscribe(_ =>
            {
                manager.OnRaise(currentGamePlayer.userIndex, currentGamePlayer.stageBet, 1000);
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

            this.view.OnAllinButtonClicked.Where(_ => isClickable).Subscribe(_ =>
            {
                manager.OnAllIn(currentGamePlayer.userIndex, currentGamePlayer.stageBet, currentGamePlayer.buyInLeft);
                isClickable = false;
            });

            GameEvent.Instance.AddPlayerTurnEvent(OnPlayerTurnEvent);
        }

        private void OnPlayerTurnEvent(GamePlayerModel model)
        {
            currentGamePlayer = model;
            bool isBlind = currentGamePlayer.roomStage == (int)Stage.PreFlop && currentGamePlayer.betCount <= 1;
            if (isBlind == false && model.lastBetType != GamePlayerModel.BetType.Allin && model.lastBetType != GamePlayerModel.BetType.Fold)
            {
                view.EnableAllButtons(true);
                
                view.EnableCheckButton(model.betCount == 0);
                
                isClickable = true;
            }
            else
            {
                view.EnableAllButtons(false);
            }
        }
    }

}
