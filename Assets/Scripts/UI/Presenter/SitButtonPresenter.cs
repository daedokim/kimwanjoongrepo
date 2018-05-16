using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.view;
using com.dug.UI.manager;
using com.dug.UI.events;
using com.dug.UI.model;
using com.dug.UI.component;
using System;
using UniRx;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class SitButtonPresenter : IPresenter
    {
        private SitButtonView view;
        private GameManager manager;
        private RoomModel model = new RoomModel();

        public SitButtonPresenter(SitButtonView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.view.CreateSitButtons();

            this.model.ObserveEveryValueChanged(x => x.playerCount).Skip(1).Subscribe(_ => {

                UpdateChair();
            });

            GameEvent.Instance.AddRoomEvent(OnRoomUpdate);
        }

        private void UpdateChair()
        {
            List<dto.GamePlayer> gamePlayers = manager.Room.gamePlayers;

            List<int> chairIndice = new List<int>();

            if (gamePlayers != null)
            {
                for (int i = 0; i < gamePlayers.Count; i++)
                {
                    if (gamePlayers[i].chairIndex != -1)
                    {
                        chairIndice.Add(gamePlayers[i].chairIndex);
                    }
                }
            }
            this.view.OnUpateUI(chairIndice);
        }

        private void OnRoomUpdate(RoomModel model)
        {
            this.model.Update(model);
        }
    }
}
