using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using com.dug.UI.view;
using com.dug.UI.Managers;
using com.dug.UI.Models;
using com.dug.UI.Events;
using System;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class TableCardPresenter : IPresenter
    {
        private TableCardView view;
        private GameManager manager;
        private RoomModel model = new RoomModel();

        public TableCardPresenter(TableCardView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            model.ObserveEveryValueChanged(x => x.stage).Subscribe(x => {

                if(x == (int)RoomModel.Stage.Flop - 1)
                {
                    this.view.Flop(model.card1, model.card2, model.card3);
                }
                else if (x == (int)RoomModel.Stage.Turn - 1)
                {
                    this.view.Turn(model.card4);
                }
                else if (x == (int)RoomModel.Stage.River - 1)
                {
                    this.view.River(model.card5);
                }
                else if(x == (int) RoomModel.Stage.Winner)
                {
                    this.view.ShowResult(manager.GetGamePlayerByUserIndex(this.model.winnerUserIndex));
                }
                else if (x == (int)RoomModel.Stage.WinnerSolo)
                {
                    
                }
            });

            GameEvent.Instance.AddClearEvent(ClearAll);
            GameEvent.Instance.AddRoomEvent(OnUpdateRoomEvent);
        }

        private void OnUpdateRoomEvent(RoomModel model)
        {
            this.model.Update(model);
        }

        private void ClearAll()
        {
            this.view.Clear();
        }

    }
}
