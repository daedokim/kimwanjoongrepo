using com.dug.UI.Models;
using com.dug.UI.DTO;
using UnityEngine.Events;

namespace com.dug.UI.Events
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
    public class ChipsEvent : UnityEvent<int, long> { }
    [System.Serializable]
    public class ClearEvent: UnityEvent { }
    [System.Serializable]
    public class HandoutCompleteEvent : UnityEvent<int> { }
    [System.Serializable]
    public class FoldEvent : UnityEvent<int> { }



    public class GameEvent : Singleton<GameEvent>
    {
        private GamePlayerEvent gamePlayerEvent = new GamePlayerEvent();
        private PlayerTurnEvent playerTurnEvent = new PlayerTurnEvent();
        private PlayerTurnEndEvent playerTurnEndEvent = new PlayerTurnEndEvent();
        private ChipsEvent chipsEvent = new ChipsEvent();
        private RoomEvent roomEvent = new RoomEvent();
        private HandoutCompleteEvent handoutCompleteEvent = new HandoutCompleteEvent();
        private ClearEvent clearEvent = new ClearEvent();
        private FoldEvent foldEvent = new FoldEvent();
        

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

        public void AddFoldEvent(UnityAction<int> call)
        {
            foldEvent.AddListener(call);
        }

        public void RemoveFoldEvent(UnityAction<int> call)
        {
            foldEvent.RemoveListener(call);
        }

        public void RemoveAllFoldEvent()
        {
            foldEvent.RemoveAllListeners();
        }

        public void InvokeFoldEvent(int chairIndex)
        {
            foldEvent.Invoke(chairIndex);
        }

        public void AddChipsEvent(UnityAction<int, long> call)
        {
            chipsEvent.AddListener(call);
        }

        public void RemoveChipsEvent(UnityAction<int, long> call)
        {
            chipsEvent.RemoveListener(call);
        }

        public void RemoveAllChipsEvent()
        {
            chipsEvent.RemoveAllListeners();
        }

        public void InvokeChipsEvent(int chairIndex, long diffAmount)
        {
            chipsEvent.Invoke(chairIndex, diffAmount);
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

