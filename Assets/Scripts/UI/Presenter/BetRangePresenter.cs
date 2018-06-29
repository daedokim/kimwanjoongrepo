using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.view;
using com.dug.UI.Managers;
using com.dug.UI.Models;
using com.dug.UI.Events;
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

                OnChangeBetRange(x);
            });

            gameEvent.AddPlayerTurnEvent(OnUpdatePlayerEvent);
            gameEvent.AddClearEvent(OnClearAll);
        }

        private void OnChangeBetRange(float rate)
        {
            double minRaiseBet = manager.Room.lastRaise;

            if (model.betCount > 0)
                minRaiseBet *= 2;

            if (model.buyInLeft < minRaiseBet)
                minRaiseBet = model.buyInLeft;

            double rangeAmount = minRaiseBet + (model.buyInLeft - minRaiseBet) * rate;

            long raiseBet = (long)rangeAmount;

            if (raiseBet > model.buyInLeft)
                raiseBet = model.buyInLeft;

            raiseBet = (long)Math.Floor((double)raiseBet / 1000) * 1000;

            
            buttonView.SendMessage("SetRaiseBetAmount", raiseBet);
            this.view.SetRangeText("Raise $" + util.GameUtil.MakePriceString(raiseBet));
        }

        private void OnUpdatePlayerEvent(GamePlayerModel model)
        {
            this.model.Update(model);
            OnChangeBetRange(this.view.GetScrollRate());

            editable = true;

            this.view.EnableScrollBar(editable);
        }

        private void OnClearAll()
        {
            this.view.Clear();
        }
    }
}

