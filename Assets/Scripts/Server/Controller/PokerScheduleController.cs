namespace com.dug.Server.Controller
{
    using System.Collections.Generic;
    using com.dug.Server.vo;
    using com.dug.Server.Util;
    using System;

    public class PokerScheduleController
    {
        private DataTable table;
        private long timeTick = GameUtil.GetCurrentTick();
        private CardSortingHelper helper;


        public PokerScheduleController(DataTable table)
        {
            this.table = table;            
        }

        public void Thread()
        {
            long diffTime = GameUtil.GetCurrentTick() - timeTick;
            timeTick = GameUtil.GetCurrentTick();

            Room room = table.SelectRoom(1);
            CheckWaitTime(room, (int)diffTime);

            if (room != null)
            {
                switch (room.state)
                {
                    case RoomState.Wait:
                        GotoReady(room);
                        break;
                    case RoomState.Ready:
                        GotoSetting(room);
                        break;
                    case RoomState.Setting:
                        GotoPlay(room);
                        break;
                    case RoomState.Playing:
                        SetPlaying(room);
                        break;
                }

                GameUtil.DebugLog("스테이지 :" + room.stage, ", 현재 벳 사용자 인덱스 :" + room.currentUserIndex, ", 현재 정렬 : " + room.currentOrderNo
              , ", 스테이지 벳금액 : " + room.stageBet, ", 마지막 벳 금액 :" + room.lastRaise, ", 마지막 벳 타입 : " + room.lastBetType, "토탈뱃 : " + room.totalBet
              );
            }
        }

        /**
         * 대기중인 방일때
         */
        private void GotoReady(Room room)
        {
            List<GamePlayer> playerList = table.SelectSitGamePlayer(room.index);

            // 두명 이상일때 게임 시작 준비로 상태 변경
            if (playerList != null && playerList.Count >= 2)
            {
                room.state = RoomState.Ready;
                room.stage = 1;
                room.waitTimeout = Room.WAITTIMEOUT_BY_READY;

                helper = new CardSortingHelper();
                room.card1 = helper.Pop();
                room.card2 = helper.Pop();
                room.card3 = helper.Pop();
                room.card4 = helper.Pop();
                room.card5 = helper.Pop();


                table.UpdateRoom(room);
            }
        }

        private void GotoSetting(Room room)
        {
            List<GamePlayer> playerList = table.SelectSitGamePlayer(room.index);

            if (room.waitTimeout <= 0)
            {
                room.state = RoomState.Setting;
                room.stage = 2;
                room.waitTimeout = Room.WAITTIMEOUT_BY_SETTING;

                room.dealerChairIndex = GetNearChairIndex(room.dealerChairIndex, playerList);
                room.ownerIndex = GetNearChairIndex(room.dealerChairIndex, playerList);
                room.lastbet = 0;
                room.stageBet = 0;
                room.currentUserIndex = table.SelectUserIndexByOwnerIndex(room.index, room.ownerIndex);
                room.currentOrderNo = table.selectOrderNoByOwnerIndex(room.index, room.ownerIndex);

                playerList = SetCards(helper, playerList);
                playerList = SetOrderNo(room.ownerIndex, playerList);

                // 정렬을 세팅한다.
                for (int i = 0; i < playerList.Count; i++)
                {
                    table.UpdateGamePlayer(room.index, playerList[i]);
                }

                InitGamePlayerMember(room.index);

                table.UpdateRoom(room);
            }
        }

        private void GotoPlay(Room room)
        {
            if (room.waitTimeout <= 0)
            {
                room.state = RoomState.Playing;
                room.stage = 3;
                room.waitTimeout = Room.WAITTIMEOUT_BY_GAME_PLAYER;
                table.UpdateRoom(room);
            }
        }

        private void CheckWaitTime(Room room, int diffTime)
        {
            if (room.waitTimeout > 0)
            {
                room.waitTimeout -= diffTime;

                if (room.waitTimeout < 0)
                    room.waitTimeout = 0;

                table.UpdateRoom(room);
            }
        }

        private void SetPlaying(Room room)
        {
            int stageSet = room.stage % 3;

            if(room.stage >= 3)
            {
                switch (stageSet)
                {
                    case 0:
                        CheckBetStatus(room);
                        break;
                    case 1:
                        CheckWinner(room);
                        break;
                    case 2:
                        CheckSetting(room);
                        break;
                }

                CheckGameStatus(room);
            }
        }

        /**
         * 시스템이 세팅하는 스테이지
         */
        private void CheckSetting(Room room)
        {
            if (room.waitTimeout <= 0)
            {
                room.stage += 1;
                room.lastRaise = room.minbetAmount * 2;
                room.waitTimeout = Room.WAITTIMEOUT_BY_GAME_PLAYER;

                room.currentOrderNo = table.SelectFirstOrderNo(room.index);
                room.currentUserIndex = table.SelectUserIndexByOrderNo(room.index, room.currentOrderNo);


                table.UpdateRoom(room);

                ClearGamePlayerByStage(room.index, room.stage);
            }
        }

        private void CheckGameStatus(Room room)
        {
            List<GamePlayer> playerList = table.SelectSitGamePlayer(room.index);

            int playerCount = 0;
            long userIndex = 0;

            for (int i = 0; i < playerList.Count; i++) 
            {
                if (playerList[i].lastBetType != BetType.Fold 
                    && (playerList[i].state == GamePlayerState.Play || playerList[i].state == GamePlayerState.StandWait)
                )
                {
                    playerCount++;                    
                }
            }

            if (playerCount == 0)
            {
                GameUtil.DebugLog("게임 무효화");

                room.waitTimeout = 15000;
                room.stage = -1;
                room.ownerIndex = -1;
                room.winnerUserIndex = -1;
                room.currentUserIndex = 0;
                room.currentOrderNo = 0;
                table.UpdateRoom(room);
            }
            else if (playerCount == 1)
            {
                GameUtil.DebugLog("남은자가 승자");

                room.waitTimeout = 15000;
                room.stage = 14;
                room.winnerUserIndex = userIndex;
                table.UpdateRoom(room);
            }
        }

        private void CheckWinner(Room room)
        {
            if (room.stage == 13)
            {

            }
            else
            {
                room.stage += 1;
                room.waitTimeout = Room.WAITTIMEOUT_BY_SETTING;
                room.stageBet = 0;
                

                table.UpdateRoom(room);
            }
        }

        /**
        *  게임플레이어 베팅 상태 체크 
        */
        private void CheckBetStatus(Room room)
        {
            GamePlayer gamePlayer = table.SelectGamePlayerByUserIdx(room.index, room.currentUserIndex);
            long currentUserIndex = 0;
            int currentOrderNo = -1;

            if (gamePlayer != null)
            {
                bool isCompleteBetUser = false;

                if ((gamePlayer.betStatus == BetStatus.BlindBetComplete || gamePlayer.betStatus == BetStatus.BetComplete)
                    && (gamePlayer.state == GamePlayerState.Play || gamePlayer.state == GamePlayerState.StandWait)
                   )
                {
                    isCompleteBetUser = true;
                }

                if (room.waitTimeout <= 0 || isCompleteBetUser == true)
                {
                    if (gamePlayer.betStatus == BetStatus.BetReady)
                    {
                        gamePlayer.betType = BetType.Fold;
                        gamePlayer.betStatus = BetStatus.BetComplete;

                        gamePlayer.lastBetType = gamePlayer.betType;
                        gamePlayer.stage = room.stage;
                        table.UpdateGamePlayer(room.index, gamePlayer);
                    }

                    // 대기하고있는 사용자가 하나도 없다면 다음 스테이지로 이동 한다.
                    if (table.SelectReadyCount(room.index) == 0)
                    {
                        room.stage += 1;
                        room.betCount = 0;
                        room.waitTimeout = Room.WAITTIMEOUT_BY_SETTING;
                        currentOrderNo = 0;
                        currentUserIndex = 0;
                        table.UpdateRoom(room);
                    }
                    else
                    {
                        room.waitTimeout = Room.WAITTIMEOUT_BY_GAME_PLAYER;
                        currentOrderNo = GetNextOrderNo(room.index, room.currentOrderNo);
                        currentUserIndex = table.SelectUserIndexByOrderNo(room.index, currentOrderNo);

                        if (currentUserIndex > 0)
                        {
                            room.currentUserIndex = currentUserIndex;
                            room.currentOrderNo = currentOrderNo;
                            table.UpdateRoom(room);
                        }
                    }
                }
            }
        }

        private void ClearGamePlayerByStage(int roomIndex, int stage)
        {
            List<GamePlayer> playerList = table.SelectSitGamePlayer(roomIndex);

            for(int i = 0; i < playerList.Count; i++)
            {
                if ((playerList[i].betStatus == BetStatus.BetComplete || playerList[i].betStatus == BetStatus.BlindBetComplete) 
                    && playerList[i].lastBetType != BetType.Fold && playerList[i].lastBetType != BetType.Allin 
                    && (playerList[i].state == GamePlayerState.Play || playerList[i].state == GamePlayerState.StandWait)
                 )
                {
                    playerList[i].state = GamePlayerState.Play;
                    playerList[i].betStatus = BetStatus.BetReady;
                    playerList[i].lastBetType = 0;
                    playerList[i].stageBet = 0;

                    table.UpdateGamePlayer(roomIndex, playerList[i]);
                }
            }
        }

        /**
         *  각 게임 플레이어들의 정렬 순서를 세팅한다.
         */
        private List<GamePlayer> SetOrderNo(int ownerIndex, List<GamePlayer> playerList)
        {
            int ownerSeq = 0;
            int i = 0;

            for(i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].chairIndex == ownerIndex)
                {
                    ownerSeq = i;
                    break;
                }
            }

            for (i = 0; i < playerList.Count; i++)
            {
                if (i < ownerSeq) playerList[i].orderNo = playerList.Count - ownerSeq + i;
                else playerList[i].orderNo = i - ownerSeq;
            }

            return playerList;
        }



        /**
         * 게임을 시작하기전에 딜러를 할 체어 인덱스를 찾는다. 
         * 
         */
        private int GetNearChairIndex(int dealerChiarIdx, List<GamePlayer> playerList)
        {
            int chairIndex = 0;
            int tempChairIndex = 0;
            int selectedChairIndex = 0;
            int minDiff = 999;

            for (int i = 0; i < playerList.Count; i++)
            {
                chairIndex = playerList[i].chairIndex;

                // 이전 딜러 체어 인덱스에 가장 가까운 체어 인덱스를 찾는다.
                if (chairIndex < dealerChiarIdx) tempChairIndex = chairIndex + Room.MAX_GAME_PLAYER_COUNT;
                else tempChairIndex = chairIndex;

                if (chairIndex != dealerChiarIdx && tempChairIndex - dealerChiarIdx < minDiff)
                {
                    minDiff = tempChairIndex - dealerChiarIdx;
                    selectedChairIndex = chairIndex;
                }
            }

            return selectedChairIndex;
        }

        /**
         * 
         *  각각의 플레이어에게 카드를 섞어서 나눠준다.
         */
        private List<GamePlayer> SetCards(CardSortingHelper helper, List<GamePlayer> playerList)
        {
            List<GamePlayer> list = new List<GamePlayer>();

            if (playerList != null && playerList.Count > 0)
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].card1 = helper.Pop();
                    playerList[i].card2 = helper.Pop();

                    list.Add(playerList[i]);
                }
            }

            return list;
        }


        private void InitGamePlayerMember(int roomIndex)
        {
            List<GamePlayer> playerList = table.SelectSitGamePlayer(roomIndex);

            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].state = GamePlayerState.Play;
                playerList[i].betStatus = BetStatus.BetReady;
                playerList[i].lastBetType = 0;
                playerList[i].stageBet = 0;

                table.UpdateGamePlayer(roomIndex, playerList[i]);
            }
        }

       

        /**
         *  다음 OrderNo를 구한다.
         */
        private int GetNextOrderNo(int index, int orderNo)
        {
            int currentOrderNo = -1;
            List<GamePlayer> playerList = table.SelectSitGamePlayer(index);
            playerList.Sort(table.CompareOrderByOrderNo);

            orderNo = (orderNo + 1) % playerList.Count;
            
            for (int i = 0; i < playerList.Count; i++)
            {
                if (orderNo == playerList[i].orderNo)
                {
                    if (playerList[i].betStatus == BetStatus.BetReady && playerList[i].lastBetType != BetType.Fold
                   && playerList[i].lastBetType != BetType.Allin && (playerList[i].state == GamePlayerState.Play
                   || playerList[i].state == GamePlayerState.StandWait)
                    )
                    {
                        currentOrderNo = orderNo;
                    }
                    else
                    {
                        currentOrderNo = GetNextOrderNo(index, orderNo);
                    }

                    break;
                }
            }
            
            return currentOrderNo;

        }
    }
}

