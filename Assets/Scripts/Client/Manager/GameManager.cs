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

            InvokeRepeating("Thread", 0, 0.5f);            
        }

        private void Thread()
        {
            CommonNetwork.Instance.GetRoom(UserData.Instance.userIndex, roomIndex);
        }

        private void OnGetRoomHandler(PacketData data)
        {
            room = JsonConverter.GetObject<Room>((Dictionary<string, object>) data.data["Room"]);

            if (data.data["GamePlayers"] != null)
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
                if (room.gamePlayers[i].userIndex == room.currentUserIndex)
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

        public void SitChair(long userIndex, int chairIndex, long buyInLeft)
        {
            CommonNetwork.Instance.Sit(roomIndex, userIndex, chairIndex, buyInLeft);
        }

        private bool CheckPreFlopBlind(GamePlayer gamePlayer)
        {
            bool isBlindBet = false;

            // Blind 체크
            if (room.stage == (int)Stage.PreFlop)
            {
                bool isMyTurn = gamePlayer.userIndex == room.currentUserIndex;

                if (isMyTurn)
                {
                    if (room.betCount == 0)
                    {
                        // Small Blind
                        CommonNetwork.Instance.SetPlayerBet(roomIndex, gamePlayer.userIndex, (int)BetType.Blind, 0, Room.minbetAmount);
                        isBlindBet = true;
                    }
                    else if (room.betCount == 1)
                    {
                        // Big Blind
                        CommonNetwork.Instance.SetPlayerBet(roomIndex, gamePlayer.userIndex, (int)BetType.Blind, Room.minbetAmount, Room.minbetAmount);
                        isBlindBet = true;
                    }
                }
            }
            return isBlindBet;
        }

        public void StandUp(long userIndex)
        {
            CommonNetwork.Instance.StandUp(roomIndex, userIndex);
        }

        public void OnCall(long userIdx, long stageBet) 
        {
            CommonNetwork.Instance.SetPlayerBet(roomIndex, userIdx, (int)BetType.Call, Room.stageBet - stageBet, 0);
        }

        public void OnRaise(long userIdx, long stageBet, long betAmount)
        {
            CommonNetwork.Instance.SetPlayerBet(roomIndex, userIdx, (int)BetType.Raise, Room.stageBet - stageBet, betAmount);
        }

        public void OnCheck(long userIdx)
        {
            CommonNetwork.Instance.SetPlayerBet(roomIndex, userIdx, (int)BetType.Check, 0, 0);
        }

        public void OnFold(long userIndex)
        {
            CommonNetwork.Instance.SetPlayerBet(roomIndex, userIndex, (int)BetType.Fold, 0, 0);
        }

        public void OnAllIn(long userIndex, long stageBet, long buyInLeft)
        {
            CommonNetwork.Instance.SetPlayerBet(roomIndex, userIndex, (int)BetType.Allin, Room.stageBet - stageBet, buyInLeft);
        }

        public GamePlayerModel GetGamePlayerByUserIndex(long userIndex)
        {
            GamePlayerModel gamePlayer = null;

            if (Room == null) return null;

            for (int i = 0; i < Room.gamePlayers.Count; i++)
            {
                if (Room.gamePlayers[i].userIndex == userIndex)
                {
                    gamePlayer = new GamePlayerModel();
                    gamePlayer.SetGamePlayer(Room.gamePlayers[i], Room);
                    break;
                }
            }
            return gamePlayer;
        }

        private void OnDataReceive(object obj)
        {
            PacketData data = (PacketData)obj;

            switch ((PacketNumConstants.PacketNum)data.packetNum)
            {
                case PacketNumConstants.PacketNum.GET_ROOM:
                    OnGetRoomHandler(data);
                    break;
                case PacketNumConstants.PacketNum.SIT:
                    OnJoinGameHandler(data);
                    break;
                case PacketNumConstants.PacketNum.SET_PLAYER_BET:
                    OnGetRoomHandler(data);
                    break;
            }
        }

        private void OnJoinGameHandler(PacketData data)
        {
            CRUDResult result = new CRUDResult();
            result.resultType = String.IsNullOrEmpty(data.error) ? CRUDResult.ResultType.SUCCESS : CRUDResult.ResultType.FAILED;
            eventHandler.Invoke(popups.SelectBuyInPopup.SIT_CHAIR_COMPLETE, result);
        }
    }



}
