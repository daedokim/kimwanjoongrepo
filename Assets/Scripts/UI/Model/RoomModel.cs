using com.dug.UI.DTO;
using System.Collections.Generic;

namespace com.dug.UI.Models
{
    public class RoomModel : IModel
    {
        public static int MAX_GAME_PLAYER_COUNT = 9;
        public static int WAITTIMEOUT_BY_SETTING = 5000;
        public static int WAITTIMEOUT_BY_GAME_PLAYER = 16000;
        public static int WAITTIMEOUT_BY_READY = 5000;

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
        public int playerCount = 0;
        public long winnerUserIndex = 0;
        public RoomState state = RoomState.Wait;
        public List<GamePlayer> gamePlayers;

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
            gamePlayers = room.gamePlayers;

            card1 = room.card1;
            card2 = room.card2;
            card3 = room.card3;
            card4 = room.card4;
            card5 = room.card5;

            winnerUserIndex = room.winnerUserIndex;
            playerCount = room.gamePlayers.Count;
        }

        public void Update(RoomModel model)
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

            gamePlayers = model.gamePlayers;
            winnerUserIndex = model.winnerUserIndex;
            playerCount = model.playerCount;
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
            Wait = -1, Ready = 0, Setting = 1, Playing = 2
        }

        public enum Stage
        {
            PreFlop = 3, Flop = 6, Turn = 9, River = 12, Winner = 14, WinnerSolo = 15
        }
    }
}

