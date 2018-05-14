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
        private long lastRaiseBet = 0;
        private bool editable = false;

        public BetRangePresenter(BetRangeView view)
        {
            this.view = view;
            this.buttonView = this.view.gameObject.GetComponent<ButtonView>();
            manager = GameManager.Instance;
            gameEvent = GameEvent.Instance;

            this.view.OnMaxButtonClicked.Where(_=> editable).Subscribe(x => {
                
                this.view.scrollBar.value = 1;
            });

            this.view.OnMinButtonClicked.Where(_ => editable).Subscribe(x => {

                this.view.scrollBar.value = 0;
            });

            this.view.OnPlusButtonClicked.Where(_ => editable).Subscribe(x => {

                this.view.scrollBar.value += 0.1f;
            });

            this.view.OnMinusButtonClicked.Where(_ => editable).Subscribe(x => {
                this.view.scrollBar.value -= 0.1f;
            });

            this.view.OnScrollBarChanged.Where(_ => editable).Subscribe(x => {

                double v = Math.Floor(x * 10) / 10;
                OnChangeBetRange((float)v);
            });

            gameEvent.AddPlayerTurnEvent(OnUpdatePlayerEvent);
            gameEvent.AddPlayerTurnEndEvent(OnTurnEndEvent);
        }
      
        private void OnChangeBetRange(float rate)
        {
            double minRaiseBet = lastRaiseBet * 2;

            if (model.buyInLeft < minRaiseBet)
                minRaiseBet = model.buyInLeft;

            double rangeAmount = minRaiseBet + (model.buyInLeft - minRaiseBet) * rate;

            long raiseBet = (long)rangeAmount;

            if (raiseBet > model.buyInLeft)
                raiseBet = model.buyInLeft;

            
            buttonView.SendMessage("SetRaiseBetAmount", raiseBet);
            this.view.SetRangeText("Raise $" + raiseBet);
        }

        private void OnUpdatePlayerEvent(GamePlayerModel model)
        {
            this.model.Update(model);
            lastRaiseBet = manager.Room.lastRaise;
            OnChangeBetRange(this.view.GetScrollRate());

            editable = true;

            this.view.EnableScrollBar(editable);
        }

        private void OnTurnEndEvent()
        {
            editable = false;
            this.view.EnableScrollBar(editable);
        }


    }
}

