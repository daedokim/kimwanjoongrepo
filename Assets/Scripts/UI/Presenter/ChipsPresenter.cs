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

            this.view.CreateGamePlayeChips();

            GameEvent.Instance.AddGamePlayerActionEvent(OnGamePlayerActionUpdate);
            GameEvent.Instance.AddRoomEvent(OnUpdateRoom);

            roomModel.ObserveEveryValueChanged(x => x.stage).Subscribe(x =>
            {
                if (x % 3 == 1)
                {
                    this.view.CollectChips(roomModel.totalBet);
                }
            });
        }

        private void OnUpdateRoom(RoomModel model)
        {
            roomModel.Update(model);
        }

        private void OnGamePlayerActionUpdate(GamePlayerModel model)
        {

            this.model.Update(model);

            this.view.ThrowChips(model);
        }
    }
}
