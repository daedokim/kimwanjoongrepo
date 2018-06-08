using com.dug.UI.events;
using com.dug.UI.manager;
using com.dug.UI.model;
using com.dug.UI.view;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using System;
using com.dug.UI.dto;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class StatusPresenter : IPresenter
    {
        private StatusView view;
        private GameManager manager;
        private RoomModel model = new RoomModel();
        IDisposable waitTimeoutDispose = null;

        public StatusPresenter(StatusView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            GameEvent.Instance.AddRoomEvent(OnRoomUpdate);
            
            model.ObserveEveryValueChanged(x => x.state).Subscribe(_ => {
                SetStateHandler();
            });

        }

        private void SetStateHandler()
        {
            DisposeWaitTimeout();

            RoomModel.RoomState state = model.state;
            if (state == RoomModel.RoomState.Wait)
            {
                view.SetStatusText("Wait GamePlayer");
            }
            else if (state == RoomModel.RoomState.Ready)
            {
                SetGameStartWaitTime(model.waitTimeout);
            }
        }

        private void SetGameStartWaitTime(int time)
        {            
            int waitTimeout = time - 1000;
            waitTimeoutDispose = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(x =>
            {
                waitTimeout -= 1000;

                if (waitTimeout >= 0)
                {
                    view.SetStatusText("" + (waitTimeout / 1000 + 1));
                }
                else
                {
                    
                    DisposeWaitTimeout();
                }
            }).AddTo(this.view);
        }

        private void DisposeWaitTimeout()
        {
            if(waitTimeoutDispose != null)
            {
                waitTimeoutDispose.Dispose();
                waitTimeoutDispose = null;
            }

            view.SetStatusText("");
        }

        public void OnRoomUpdate(RoomModel model)
        {
            this.model.Update(model);
        }
    }
}

