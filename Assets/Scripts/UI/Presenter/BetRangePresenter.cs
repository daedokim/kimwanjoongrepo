using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.view;
using com.dug.UI.manager;
using com.dug.UI.model;
using com.dug.UI.events;
using UniRx;
using System;

namespace com.dug.UI.presenter
{
    public class BetRangePresenter : IPresenter
    {
        private BetRangeView view;
        private ButtonView buttonView;
        private GameManager manager;
        private GameEvent gameEvent;
        private GamePlayerModel model = new GamePlayerModel();
        private float selectedRate = 0;
        private long lastRaiseBet = 0;

        public BetRangePresenter(BetRangeView view)
        {
            this.view = view;
            this.buttonView = this.view.gameObject.GetComponent<ButtonView>();
            manager = GameManager.Instance;
            gameEvent = GameEvent.Instance;

            this.view.OnMaxButtonClicked.Subscribe(x => {

                this.view.scrollBar.value = 1;
            });

            this.view.OnMinButtonClicked.Subscribe(x => {

                this.view.scrollBar.value = 0;
            });

            this.view.OnPlusButtonClicked.Subscribe(x => {

                this.view.scrollBar.value += 0.1f;
            });

            this.view.OnMinusButtonClicked.Subscribe(x => {
                this.view.scrollBar.value -= 0.1f;
            });

            this.view.OnScrollBarChanged.Subscribe(x => {

                double v = Math.Floor(x * 10) / 10;
                OnChangeBetRange((float)v);
            });

            gameEvent.AddPlayerTurnEvent(OnUpdatePlayerEvent);
        }

        private void OnChangeBetRange(float rate)
        {
            selectedRate = rate;

            double minRaiseBet = lastRaiseBet * 2;
            double maxRaiseBet = model.buyInLeft;
            if (model.buyInLeft < minRaiseBet)
                minRaiseBet = model.buyInLeft;

            double rangeAmount = minRaiseBet + (model.buyInLeft - minRaiseBet) * selectedRate;

            long raiseBet = (long)rangeAmount;

            buttonView.SendMessage("SetRaiseBetAmount", raiseBet);
            this.view.SetRangeText("Raise $" + raiseBet);
        }

        private void OnUpdatePlayerEvent(GamePlayerModel model)
        {
            this.model.Update(model);
            lastRaiseBet = manager.Room.lastRaise;


            Debug.Log("lastRaiseBet :  " + lastRaiseBet);

            OnChangeBetRange(selectedRate);
        }
    }
}

