
namespace com.dug.Server.Controller
{
    using System.Collections.Generic;
    using com.dug.Server.vo;
    using com.dug.Server.Util;
    using System;

    public class PokerController
    {
        private DataTable table;

        public PokerController(DataTable table)
        {
            this.table = table;
        }

        public Room GetRoom(int roomIndex)
        {
            Room room = table.SelectRoom(roomIndex);
            room.gamePlayers = table.SelectGamePlayers(roomIndex);
            

            return room;
        }

        public Room SetStageClear(int roomIndex, long userIndex, int stage)
        {
            GamePlayer gamePlayer = table.SelectGamePlayerByUserIdx(roomIndex, userIndex);
            gamePlayer.stage = stage;

            table.UpdateGamePlayer(roomIndex, gamePlayer);

            return GetRoom(roomIndex);
        }

        public Room SetPlayerBetting(int roomIndex, long userIdx, int betType, long callAmount, long betAmount)
        {
            GamePlayer gamePlayer = table.SelectGamePlayerByUserIdx(roomIndex, userIdx);
            Room room = table.SelectRoom(roomIndex);
            if (gamePlayer != null)
            {
                bool isBlindBet = false;
                long totalAmount = callAmount + betAmount;


                if (betType == (int)BetType.Blind)
                {
                    betType = (int)BetType.Raise;
                    isBlindBet = true;
                }

                if(isBlindBet == true)
                {
                    gamePlayer.betStatus = BetStatus.BlindBetComplete;

                    if(callAmount == 0)
                    {
                        room.stageBet = 0;
                        room.totalBet = 0;
                        room.betCount = 0;
                    }
                }
                else
                {
                    gamePlayer.betStatus = BetStatus.BetComplete;
                    gamePlayer.lastActionDate = System.DateTime.Now;
                    gamePlayer.noActionCount = 0;
                }

                gamePlayer.betCount += 1;
                gamePlayer.lastBetType = (BetType)betType;
                gamePlayer.lastBet = totalAmount;
                gamePlayer.lastCall = callAmount;
                gamePlayer.lastRaise = betAmount;
                gamePlayer.stageBet += totalAmount;
                gamePlayer.totalBet += totalAmount;
                gamePlayer.buyInLeft -= totalAmount;
                gamePlayer.stage = room.stage;

                table.UpdateGamePlayer(roomIndex, gamePlayer);

                if((BetType)betType != BetType.Fold && (BetType)betType != BetType.Check)
                {
                    room.betCount += 1;
                }
                room.stageBet += betAmount;
                room.totalBet += totalAmount;
                room.lastRaise = betAmount;
                room.lastBetType = (BetType)betType;

                table.UpdateRoom(room);

                if (isBlindBet == false && (BetType)betType == BetType.Raise)
                {
                    SetAnotherBetStatusReady(roomIndex, userIdx);
                }
            }
            
            return GetRoom(roomIndex);
        }

        public void DoSit(int roomIndex, long userIndex, int chairIndex, long buyInLeft)
        {
            table.InsertGamePlayer(roomIndex, userIndex, chairIndex, buyInLeft);
        }

        private void SetAnotherBetStatusReady(int roomIndex, long userIdx)
        {
            List<GamePlayer> list = table.SelectSitGamePlayer(roomIndex);

            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].useridx == userIdx)
                    continue;
                if(list[i].betStatus == BetStatus.BetComplete || list[i].betStatus == BetStatus.BlindBetComplete)
                {
                    if (list[i].lastBetType != BetType.Fold && list[i].lastBetType != BetType.Allin)
                    {
                        list[i].betStatus = BetStatus.BetReady;
                        table.UpdateGamePlayer(roomIndex, list[i]);
                    }
                }
            }
        }
    }
}
