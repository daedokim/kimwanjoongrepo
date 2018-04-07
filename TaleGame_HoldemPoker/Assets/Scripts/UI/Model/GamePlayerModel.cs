using UnityEngine;
using UniRx;
using com.dug.UI.dto;
using System;

namespace com.dug.UI.model
{
    [System.Serializable]
    public class GamePlayerModel : IModel
    {
        public GamePlayerState status = GamePlayerState.Stand;
        public int chairIndex = 0;
        public int card1 = 0;
        public int card2 = 0;


        public int stage = 0;
        public int orderNo = 0;
        public long buyInLeft = 0;

        public BetType lastBetType;
        public int betCount = 0;
        public long lastBet = 0;
        public long lastCall = 0;
        public long lastRaise = 0;
        public long stageBet = 0;
        public long totalBet = 0;


        public long userIndex = 0;
        public long coin = 0;
        public string nickName;
        public bool isMyTurn;
        public int roomStage;

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
            Check = 1, Call = 2, Blind = 30, Raise = 3, Allin = 5, Fold = 4
        }

        public void SetGamePlayer(GamePlayer gamePlayer, Room room)
        {
            stage = gamePlayer.stage;
            status = (GamePlayerState)gamePlayer.state;
            chairIndex = gamePlayer.chairIndex;
            card1 = gamePlayer.card1;
            card2 = gamePlayer.card2;
            orderNo = gamePlayer.orderNo;
            buyInLeft = gamePlayer.buyInLeft;
            lastBetType = (BetType)gamePlayer.lastBetType;
            betCount = gamePlayer.betCount;
            lastBet = gamePlayer.lastBet;
            lastCall = gamePlayer.lastCall;
            lastRaise = gamePlayer.lastRaise;
            stageBet = gamePlayer.stageBet;
            totalBet = gamePlayer.totalBet;

            userIndex = gamePlayer.useridx;
            coin = gamePlayer.coin;
            nickName = gamePlayer.nickName;

            isMyTurn = gamePlayer.useridx == room.currentUserIndex;
            betCount = room.betCount;
            roomStage = room.stage;
        }

        public void Update(GamePlayerModel model)
        {
            stage = model.stage;
            status = model.status;
            chairIndex = model.chairIndex;
            card1 = model.card1;
            card2 = model.card2;
            orderNo = model.orderNo;
            buyInLeft = model.buyInLeft;
            lastBetType = model.lastBetType;
            betCount = model.betCount;
            lastBet = model.lastBet;
            lastCall = model.lastCall;
            lastRaise = model.lastRaise;
            stageBet = model.stageBet;
            totalBet = model.totalBet;

            userIndex = model.userIndex;
            coin = model.coin;
            nickName = model.nickName;
            isMyTurn = model.isMyTurn;

            roomStage = model.roomStage;

        }
    }

}
