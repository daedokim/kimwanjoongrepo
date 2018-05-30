using com.dug.UI.model;
using com.dug.UI.dto;
using UnityEngine.Events;

namespace com.dug.UI.events
{
    [System.Serializable]
    public class GamePlayerEvent : UnityEvent<GamePlayerModel> {}
    [System.Serializable]
    public class RoomEvent : UnityEvent<RoomModel> { }
    [System.Serializable]
    public class PlayerTurnEvent : UnityEvent<GamePlayerModel> { }
    [System.Serializable]
    public class PlayerTurnEndEvent : UnityEvent { }
    [System.Serializable]
    public class GamePlayerAcionEvent : UnityEvent<GamePlayerModel> { }
    [System.Serializable]
    public class ClearEvent: UnityEvent { }
    
    [System.Serializable]
    public class HandoutCompleteEvent : UnityEvent<int> { }


    public class GameEvent : Singleton<GameEvent>
    {
        private GamePlayerEvent gamePlayerEvent = new GamePlayerEvent();
        private PlayerTurnEvent playerTurnEvent = new PlayerTurnEvent();
        private PlayerTurnEndEvent playerTurnEndEvent = new PlayerTurnEndEvent();
        private GamePlayerAcionEvent gamePlayerActionEvent = new GamePlayerAcionEvent();
        private RoomEvent roomEvent = new RoomEvent();
        private HandoutCompleteEvent handoutCompleteEvent = new HandoutCompleteEvent();
        private ClearEvent clearEvent = new ClearEvent();
        

        public void AddGamePlayerEvent(UnityAction<GamePlayerModel> call)
        {
            gamePlayerEvent.AddListener(call);
        }

        public void RemoveGamePlayerEvent(UnityAction<GamePlayerModel> call)
        {
            gamePlayerEvent.RemoveListener(call);
        }

        public void RemoveAllGamePlayerEvent()
        {
            gamePlayerEvent.RemoveAllListeners();
        }

        public void InvokeGamePlayerEvent(GamePlayerModel gameplayer)
        {
            gamePlayerEvent.Invoke(gameplayer);
        }

        public void AddPlayerTurnEvent(UnityAction<GamePlayerModel> call)
        {
            playerTurnEvent.AddListener(call);
        }

        public void RemovePlayerTurnEvent(UnityAction<GamePlayerModel> call)
        {
            playerTurnEvent.RemoveListener(call);
        }

        public void RemoveAllPlayerTurnEvent()
        {
            playerTurnEvent.RemoveAllListeners();
        }

        public void InvokePlayerTurnEvent(GamePlayerModel model)
        {
            playerTurnEvent.Invoke(model);
        }

        //public void AddPlayerTurnEndEvent(UnityAction call)
        //{
        //    playerTurnEndEvent.AddListener(call);
        //}

        //public void RemovePlayerTurnEndEvent(UnityAction call)
        //{
        //    playerTurnEndEvent.RemoveListener(call);
        //}

        //public void RemoveAllPlayerTurnEndEvent()
        //{
        //    playerTurnEndEvent.RemoveAllListeners();
        //}

        //public void InvokePlayerTurnEndEvent()
        //{
        //    playerTurnEndEvent.Invoke();
        //}

        public void AddGamePlayerActionEvent(UnityAction<GamePlayerModel> call)
        {
            gamePlayerActionEvent.AddListener(call);
        }

        public void RemoveGamePlayerActionEvent(UnityAction<GamePlayerModel> call)
        {
            gamePlayerActionEvent.RemoveListener(call);
        }

        public void RemoveAllGamePlayerActionEvent()
        {
            gamePlayerActionEvent.RemoveAllListeners();
        }

        public void InvokeGamePlayerActionEvent(GamePlayerModel model)
        {
            gamePlayerActionEvent.Invoke(model);
        }

        public void AddRoomEvent(UnityAction<RoomModel> call)
        {
            roomEvent.AddListener(call);
        }

        public void RemoveRoomEvent(UnityAction<RoomModel> call)
        {
            roomEvent.RemoveListener(call);
        }

        public void RemoveAllRoomEvent()
        {
            roomEvent.RemoveAllListeners();
        }

        public void InvokeRoomEvent(RoomModel model)
        {
            roomEvent.Invoke(model);
        }

        public void AddHandoutCompleteEvent(UnityAction<int> call)
        {
            handoutCompleteEvent.AddListener(call);
        }

        public void RemovHandoutCompleteEvent(UnityAction<int> call)
        {
            handoutCompleteEvent.RemoveListener(call);
        }

        public void RemoveAllHandoutCompleteEvent()
        {
            handoutCompleteEvent.RemoveAllListeners();
        }

        public void InvokeHandoutCompleteEvent(int chairIndex)
        {
            handoutCompleteEvent.Invoke(chairIndex);
        }

        public void AddClearEvent(UnityAction call)
        {
            clearEvent.AddListener(call);
        }

        public void RemoveClearEvent(UnityAction call)
        {
            clearEvent.RemoveListener(call);
        }

        public void RemoveClearEvent()
        {
            clearEvent.RemoveAllListeners();
        }

        public void InvokeClearEvent()
        {
            clearEvent.Invoke();
        }
    }
}

