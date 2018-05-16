
namespace com.dug.Server.vo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Room
    {
        public static int MAX_GAME_PLAYER_COUNT = 9;
        public static int MINIMUM_GAME_PLAYER_COUNT_FOR_GAME = 2;
        public static int WAITTIMEOUT_BY_SETTING = 1000;
        public static int WAITTIMEOUT_BY_GAME_PLAYER = 17000;
        public static int WAITTIMEOUT_BY_READY = 5000;
        public bool isInit;
        public int index;
        public int doingGame;
        public RoomState state = RoomState.Wait;
        public int round;
        public int card1;
        public int card2;
        public int card3;
        public int card4;
        public int card5;
        public int lastbet;
        public long lastRaise;
        public long winnerUserIndex = 0;
        public long currentUserIndex = 0;
        public int dealerChairIndex = -1;
        public int ownerIndex = 0;

        public long totalBet = 0;
        public long lastBet = 0;
        public long stageBet = 0;
        public BetType lastBetType;

        public long buyInMin = 20000;
        public long buyInMax = 200000;
        public int stage = 0;
        public int betfinished = 0;
        public int betCount = 0;
        public int currentOrderNo = 0;
        public int minbetAmount = 0;

        public int waitTimeout = 10000;

        [SerializeField]
        public List<GamePlayer> gamePlayers = new List<GamePlayer>();

    }

    public enum RoomState
    {
        Wait = -1, Ready = 0, Setting = 1, Playing = 2
    }

    public enum Stage
    {
        PreFlop = 3, Flop = 6, Turn = 9, River = 12
    }

    
}
   