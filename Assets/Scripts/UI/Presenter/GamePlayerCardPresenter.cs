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
    public class GamePlayerCardPresenter : IPresenter
    {
        private GamePlayerCardView view;
        private GameManager manager;
        private RoomModel roomModel = new RoomModel();

        public GamePlayerCardPresenter(GamePlayerCardView view)
        {
            this.view = view;
            manager = GameManager.Instance;

            this.view.CreateTableCards();

            // 준비 상태일때 딜러가 카드를 사용자에게 나눠준다.
            roomModel.ObserveEveryValueChanged(x => x.state).Subscribe(x => {
                if(x == RoomModel.RoomState.Setting)
                {
                    HandoutCards();
                }
            });

            GameEvent.Instance.AddRoomEvent(OnRoomUpdate);
            GameEvent.Instance.AddClearEvent(OnClearAll);
            GameEvent.Instance.AddFoldEvent(OnFold);
        }

        private void HandoutCards()
        {
            List<DTO.GamePlayer> gamePlayers = manager.Room.gamePlayers;

            int count = 1;

            if(gamePlayers != null)
            {
                for(int i = 0; i < gamePlayers.Count; i++)
                {
                    if(gamePlayers[i].state != DTO.GamePlayerState.Stand)
                    {
                        this.view.HandOut(gamePlayers[i].chairIndex, 0.2f, 0.2f * count);

                        count++;
                    }
                }
            }
        }

        private void OnFold(int chairIndex)
        {
            this.view.ReturnCard(chairIndex);
        }

        private void OnRoomUpdate(RoomModel model)
        {
            this.roomModel.Update(model);
        }

        private void OnClearAll()
        {
            this.view.Clear(0.3f);
        }

    }
}


