namespace com.dug.UI.dto
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class GamePlayer
    {

        public GamePlayerState state;
        [SerializeField]
        public int chairIndex;
        public int roomIndex;
        public int round;

        public int card1;
        public int card2;

        public long buyInLeft;
        public int orderNo;
        public int stage;
        public BetStatus betStatus;
        public BetType betType;
        public BetType lastBetType;
        public int betCount;
        public long lastBet;
        public long lastCall;
        public long lastRaise;
        public long totalBet;
        public long stageBet;
        public DateTime lastActionDate;
        public int noActionCount;

        public long coin;
        public string nickName;
        public long useridx;


        // 초기화 여부
        public bool IsInitialize { get; set; }

        public void Init()
        {
            round = 0;
            card1 = 0;
            card2 = 0;
            orderNo = 999;

            IsInitialize = true;
        }
    }

    public enum GamePlayerState
    {
        Play = 1, Stand = 0, SitWait = 2, StandWait = 3
    }

    public enum BetStatus
    {
        BetComplete = 1, BetReady = 0, BlindBetComplete = 3, AllBetComplete = 2
    }

    public enum BetType
    {
        Check = 1, Call = 2, Blind = 30, Raise = 3, Allin = 5, Fold = 4
    }
}
