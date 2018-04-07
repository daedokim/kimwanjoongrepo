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
        public StatusModel model = new StatusModel();
        IDisposable waitTimeoutDispose = null;

        public StatusPresenter(StatusView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            GameEvent.Instance.AddStatusEvent(new UnityAction<StatusModel>(OnStatusUpdate));
            GameEvent.Instance.AddPlayerTurnEvent(new UnityAction<GamePlayerModel>(OnPlayerTurnUpdate));
            model.ObserveEveryValueChanged(x => x.state).Subscribe(_ => {
                SetStateHandler();
            });

            GameEvent.Instance.AddPlayerTurnEndEvent(new UnityAction(OnPlayerTurnEnd));
        }

        private void OnPlayerTurnEnd()
        {
            DisposeWaitTimeout();
        }

        private void OnPlayerTurnUpdate(GamePlayerModel model)
        {
            view.SetStatusText(manager.Room.stage + "에서 "  + model.nickName + "의 턴");
            SetWaitTimeout(RoomModel.WAITTIMEOUT_BY_GAME_PLAYER);
        }

        private void SetStateHandler()
        {
            StatusModel.RoomState state = model.state;
            if (state == StatusModel.RoomState.Wait)
            {
                view.SetStatusText("게임 준비" + state);
            }
            else if (state == StatusModel.RoomState.Ready)
            {
                view.SetStatusText("딜러가 카드를 나눠줌 게임 시작 남은 시간 : " + model.waitTimeout / 1000);
                SetWaitTimeout(model.waitTimeout);
            }
        }

        private void SetWaitTimeout(int time)
        {
            if(waitTimeoutDispose != null)
            {
                waitTimeoutDispose.Dispose();
                waitTimeoutDispose = null;
            }

            int waitTimeout = time;
            waitTimeoutDispose = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(x =>
            {
                waitTimeout -= 1000;

                if (waitTimeout >= 0)
                {
                    view.SetStatusText("남은 시간 : " + waitTimeout / 1000);
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


        public void OnStatusUpdate(StatusModel model)
        {
            this.model.Update(model);
        }
    }
}

