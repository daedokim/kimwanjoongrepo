using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.dto;


namespace com.dug.UI.model
{
    public class RoomModel : IModel
    {
        public static int MAX_GAME_PLAYER_COUNT = 9;
        public static int WAITTIMEOUT_BY_GAME_PLAYER = 10000;

        public int roomIndex = 0;
        public int stage = 0;
        public int currentOrderNo = 0;
        public long currentUserIndex = 0;
        public int waitTimeout = 0;
        public long totalBet = 0;
        public long stageBet = 0;
        public int card1 = 0;
        public int card2 = 0;
        public int card3 = 0;
        public int card4 = 0;
        public int card5 = 0;
        public RoomState state;


        public void SetRoomData(Room room)
        {
            roomIndex = room.index;
            stage = room.stage;
            currentOrderNo = room.currentOrderNo;
            currentUserIndex = room.currentUserIndex;
            totalBet = room.totalBet;
            stageBet = room.stageBet;
            state = (RoomState)room.state;
            waitTimeout = room.waitTimeout;

            card1 = room.card1;
            card2 = room.card2;
            card3 = room.card3;
            card4 = room.card4;
            card5 = room.card5;
        }


        internal void Update(RoomModel model)
        {
            roomIndex = model.roomIndex;
            stage = model.stage;
            currentOrderNo = model.currentOrderNo;
            currentUserIndex = model.currentUserIndex;
            totalBet = model.totalBet;
            stageBet = model.stageBet;
            state = model.state;
            waitTimeout = model.waitTimeout;

            card1 = model.card1;
            card2 = model.card2;
            card3 = model.card3;
            card4 = model.card4;
            card5 = model.card5;
        }



        public enum GamePlayerState
        {
            Play = 1, Stand = 0, SitWait = 2, StandWait = 3
        }


        public enum BetStatus
        {
            BetComplete = 1, BetReady = 2, BlindBetComplete = 3
        }

        public enum BetType
        {
            Init = 0, Check = 1, Call = 2, Blind = 30, Raise = 3, Allin = 5, Fold = 4
        }

        public enum RoomState
        {
            Wait = 0, Ready = 1, Playing = 2
        }

    }
}

