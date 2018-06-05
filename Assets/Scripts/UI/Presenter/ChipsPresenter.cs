using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using com.dug.UI.view;
using com.dug.UI.model;
using com.dug.UI.manager;
using com.dug.UI.events;
using System.Text;
using System;
using UniRx;

namespace com.dug.UI.presenter
{
    public class ChipsPresenter : IPresenter
    {
        private ChipsView view;
        private GameManager manager;

        private GamePlayerModel model = new GamePlayerModel();
        private RoomModel roomModel = new RoomModel();

        public ChipsPresenter(ChipsView view)
        {
            this.view = view;
            manager = GameManager.Instance;

            GameEvent.Instance.AddChipsEvent(OnChipsUpdate);
            GameEvent.Instance.AddRoomEvent(OnUpdateRoom);
            GameEvent.Instance.AddClearEvent(OnClearAll);

            this.view.CreateGamePlayeChips();

            roomModel.ObserveEveryValueChanged(x => x.stage).Subscribe(x =>
            {
                if (x % 3 == 2)
                {
                    if(roomModel.totalBet > 0)
                    {
                        this.view.CollectChips(roomModel.totalBet);
                    }
                }
            });
        }

        private void OnClearAll()
        {
            this.view.Clear();
        }

        private void OnUpdateRoom(RoomModel model)
        {
            roomModel.Update(model);
        }

        private void OnChipsUpdate(int chairIndex, long amount)
        {
            this.view.ThrowChips(chairIndex, amount);
        }
    }
}
