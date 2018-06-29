using System;
using com.dug.UI.DTO;
using com.dug.UI.Events;
using com.dug.UI.Managers;
using com.dug.UI.Models;
using com.dug.UI.view;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class GamePlayersPresenter : IPresenter
    {
        private GamePlayersView view;
        private GameManager manager;

        private RoomModel roomModel = new RoomModel();
        private GameEvent gameEvent = GameEvent.Instance;

        public GamePlayersPresenter(GamePlayersView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.view.CreateGamePlayers();

            gameEvent.AddGamePlayerEvent(OnUpdateGamePlayer);
            gameEvent.AddHandoutCompleteEvent(OnHandOutComplete);
            gameEvent.AddClearEvent(OnClearAll);
            gameEvent.AddRoomEvent(OnRoomUpdate);
        }

        private void OnRoomUpdate(RoomModel model)
        {
            roomModel.Update(model);

            List<DTO.GamePlayer> gamePlayers = model.gamePlayers;

            List<int> chairIndice = new List<int>();
            int i = 0;
            if (gamePlayers != null)
            {
                for (i = 0; i < gamePlayers.Count; i++)
                {
                    if (gamePlayers[i].chairIndex != -1)
                    {
                        chairIndice.Add(gamePlayers[i].chairIndex);
                    }
                }

                this.view.CheckSitGamePlayers(chairIndice);
            }
        }

        private void OnHandOutComplete(int charIndex)
        {
            this.view.ShowOwnCards(charIndex);
        }

        private void OnUpdateGamePlayer(GamePlayerModel model)
        {   
            this.view.OnUpateUI(model);
        }
        
        private void OnClearAll()
        {
            this.view.Clear();
        }

    }
}

