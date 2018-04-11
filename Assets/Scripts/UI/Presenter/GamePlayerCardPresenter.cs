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

            GameEvent.Instance.AddRoomEvent(OnRoomUpdate);

            roomModel.ObserveEveryValueChanged(x => x.state).Subscribe(x => {
                if(x == RoomModel.RoomState.Ready)
                {
                    int waitTime = RoomModel.WAITTIMEOUT_BY_GAME_START/1000;

                    Observable.FromCoroutine<int>(observer => HandoutCardsCoroutine(observer, waitTime)).Subscribe(_ => { });
                }
            });

        }

        public IEnumerator HandoutCardsCoroutine(IObserver<int> observer, int waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            observer.OnCompleted();


            HandoutCards();
        }

        private void HandoutCards()
        {
            List<dto.GamePlayer> gamePlayers = manager.Room.gamePlayers;

            if(gamePlayers != null)
            {
                for(int i = 0; i < gamePlayers.Count; i++)
                {
                    if(gamePlayers[i].state != dto.GamePlayerState.Stand)
                    {
                        this.view.GetTableCard(gamePlayers[i].chairIndex);
                    }
                }
            }
        }

        private void OnRoomUpdate(RoomModel model)
        {
            this.roomModel.Update(model);
        }
    }
}


