using com.dug.UI.DAO;
using com.dug.UI.DTO;
using com.dug.UI.Models;
using com.dug.UI.Events;
using UnityEngine;
using System;
using UniRx;
using com.dug.UI.Networks;
using com.dug.common;
using System.Collections.Generic;

namespace com.dug.UI.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        protected RoomDAO dao;
        protected Room room = null;

        private int roomIndex = 1;
        private RoomState currentState;

        private RoomModel roomModel = new RoomModel();
        private GamePlayerModel gamePlayerModel = new GamePlayerModel();
        private GamePlayerModel gamePlayerUpdateModel = new GamePlayerModel();

        private GamePlayer currentGamePlayer = null;
        private GameEvent gameEvent = null;

        public Room Room { get { return room; } }
        public GameEvent GameEvent { get {return gameEvent;} }

        public GameEventHandler eventHandler;

        private void Awake()
        {
            dao = new RoomDAO();
            eventHandler = GameEventHandler.Instance;
            eventHandler.AddHandler(this, UserManager.USER_LOGIN_COMPLETE, OnLoginCompleteHandler);
            eventHandler.AddHandler(this, ResponseManager.ON_DATA_RECIEVE, OnDataReceive);
            gameEvent = GameEvent.Instance;
        }
        
        private void OnLoginCompleteHandler(object obj)
        {
            roomModel.ObserveEveryValueChanged(x => x.currentUserIndex).Subscribe(x =>
            {
                if (x == 0 || currentGamePlayer == null)
                    return;

                if (CheckPreFlopBlind(currentGamePlayer) == false)
                {
                    gamePlayerModel.SetGamePlayer(currentGamePlayer, Room);
                    gameEvent.InvokePlayerTurnEvent(gamePlayerModel);
                }
            }).AddTo(this);

            roomModel.ObserveEveryValueChanged(x => x.stage).Where(x => x == 17).Subscribe(x => {

                gameEvent.InvokeClearEvent();
            });

            InvokeRepeating("Thread", 0.10f, 0.10f);            
        }

   
        private void Thread()
        {
            CommonNetwork.Instance.GetRoom(UserData.Instance.userIndex, roomIndex);
        }

        private void OnDataReceive(object obj)
        {
            PacketData data = (PacketData)obj;

            switch ((PacketNumConstants.PacketNum)data.packetNum)
            {
                case PacketNumConstants.PacketNum.GET_ROOM:
                    OnGetRoomHandler(data);
                    break;
            }
        }

        private void OnGetRoomHandler(PacketData data)
        {
            Debug.Log("data 받아온다.");
            room = JsonConverter.GetObject<Room>((Dictionary<string, object>) data.data["Room"]);

            if(data.data["GamePlayers"] != null)
            {
                object[] temp = (object[])data.data["GamePlayers"];

                if(temp.Length > 0)
                {
                    List<GamePlayer> gamePlayers = JsonConverter.GetObjectList<GamePlayer>((Dictionary<string, object>[])temp);
                    room.gamePlayers = gamePlayers;
                }
            }
            

            

            currentState = room.state;

            if (currentState != RoomState.Playing)
            {
                room.currentOrderNo = -1;
                room.currentUserIndex = 0;
            }

            for (int i = 0; i < room.gamePlayers.Count; i++)
            {
                if (room.gamePlayers[i].useridx == room.currentUserIndex)
                    currentGamePlayer = room.gamePlayers[i];

                gamePlayerUpdateModel.SetGamePlayer(room.gamePlayers[i], Room);
                gameEvent.InvokeGamePlayerEvent(gamePlayerUpdateModel);
            }
            roomModel.SetRoomData(room);

            if (roomModel.stage < 17)
            {
                gameEvent.InvokeRoomEvent(roomModel);
            }

        }

        private bool CheckPreFlopBlind(GamePlayer gamePlayer)
        {
            bool isBlindBet = false;
            // Blind 체크
            if (room.stage == (int)Stage.PreFlop)
            {
                bool isMyTurn = gamePlayer.useridx == room.currentUserIndex;

                isMyTurn = true;
                if (isMyTurn)
                {
                    if (room.betCount == 0)
                    {
                        // Small Blind
                        dao.SetPlayerBetting(roomIndex, gamePlayer.useridx, BetType.Blind, 0, Room.minbetAmount);
                        isBlindBet = true;
                    }
                    else if (room.betCount == 1)
                    {
                        // Big Blind
                        dao.SetPlayerBetting(roomIndex, gamePlayer.useridx, BetType.Blind, Room.minbetAmount, Room.minbetAmount);
                        isBlindBet = true;
                    }
                }
            }
            return isBlindBet;
        }

        public CRUDResult StandUp(long userIndex)
        {
            return dao.StandUp(roomIndex, userIndex);
        }

        public void OnCall(long userIdx, long stageBet) 
        {            
            dao.SetPlayerBetting(roomIndex, userIdx, BetType.Call, Room.stageBet - stageBet, 0);
            //gameEvent.InvokePlayerTurnEndEvent();
        }

        public void OnRaise(long userIdx, long stageBet, long betAmount)
        {            
            dao.SetPlayerBetting(roomIndex, userIdx, BetType.Raise, Room.stageBet - stageBet, betAmount);
            //gameEvent.InvokePlayerTurnEndEvent();
        }

        public void OnCheck(long userIdx)
        {
            dao.SetPlayerBetting(roomIndex, userIdx, BetType.Check, 0, 0);
            //gameEvent.InvokePlayerTurnEndEvent();
        }

        public void OnFold(long userIndex)
        {
            dao.SetPlayerBetting(roomIndex, userIndex, BetType.Fold, 0, 0);
            //gameEvent.InvokePlayerTurnEndEvent();
        }

        public void OnAllIn(long userIndex, long stageBet, long buyInLeft)
        {
            dao.SetPlayerBetting(roomIndex, userIndex, BetType.Allin, Room.stageBet - stageBet, buyInLeft);
            //gameEvent.InvokePlayerTurnEndEvent();
        }

        public CRUDResult SitChair(long userIndex, int chairIndex, long buyInLeft)
        {
            CRUDResult result = dao.DoSit(roomIndex, userIndex, chairIndex, buyInLeft);

            return result;
        }
        public GamePlayerModel GetGamePlayerByUserIndex(long userIndex)
        {
            GamePlayerModel gamePlayer = null;

            if (Room == null) return null;


            for (int i = 0; i < Room.gamePlayers.Count; i++)
            {
                if (Room.gamePlayers[i].useridx == userIndex)
                {
                    gamePlayer = new GamePlayerModel();
                    gamePlayer.SetGamePlayer(Room.gamePlayers[i], Room);
                    break;
                }
            }
            return gamePlayer;
        }
    }

}
