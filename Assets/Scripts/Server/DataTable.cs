
namespace com.dug.Server
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using com.dug.Server.vo;
    using System;

    public class DataTable
    {

        public static Dictionary<int, Room> RoomTable = new Dictionary<int, Room>();
        public static Dictionary<int, List<GamePlayer>> GamePlayertable = new Dictionary<int, List<GamePlayer>>();



        public void InitTable()
        {
            Room info = new Room();
            info.index = 1;
            info.state = RoomState.Wait;
            InitGamePlayerList(info.index);

            RoomTable.Add(info.index, info);
        }

        private void InitGamePlayerList(int roomIndex)
        {
            List<GamePlayer> gamePlayers = new List<GamePlayer>();
            GamePlayer info = new GamePlayer();
            info.state = GamePlayerState.Stand;
            info.roomIndex = roomIndex;
            info.chairIndex = -1;
            info.useridx = 1000;
            info.coin = 100000;
            info.nickName = "James.P";
            gamePlayers.Add(info);

            info = new GamePlayer();
            info.state = GamePlayerState.SitWait;
            info.roomIndex = roomIndex;
            info.chairIndex = 2;
            info.useridx = 1001;
            info.coin = 100000;
            info.nickName = "James.P1";
            gamePlayers.Add(info);

            info = new GamePlayer();
            info.state = GamePlayerState.Stand;
            info.roomIndex = roomIndex;
            info.chairIndex = 4;
            info.useridx = 1002;
            info.coin = 100000;
            info.nickName = "James.P2";
            gamePlayers.Add(info);

            info = new GamePlayer();
            info.state = GamePlayerState.Stand;
            info.roomIndex = roomIndex;
            info.chairIndex = 6;
            info.useridx  = 1003;
            info.coin = 500000;
            info.nickName = "Player";
            gamePlayers.Add(info);

            GamePlayertable.Add(roomIndex, gamePlayers);
        }

        public Room SelectRoom(int roomIndex)
        {
            Room info = null;

            if (RoomTable.ContainsKey(roomIndex))
            {
                RoomTable.TryGetValue(roomIndex, out info);
            }
            return info;
        }

        private Room InsertRoom()
        {
            Room info = new Room();

            int roomIndex = 0;

            while (RoomTable.ContainsKey(roomIndex))
            {
                roomIndex++;
            }
            info.index = roomIndex;
            info.state = RoomState.Wait;

            return info;
        }

        public int selectOrderNoByOwnerIndex(int roomIndex, int ownerIndex)
        {
            List<GamePlayer> list = SelectSitGamePlayer(roomIndex);
            int orderNo = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].chairIndex == ownerIndex)
                {
                    orderNo = list[i].orderNo;
                    break;
                }
            }
            return orderNo;
        }

        public long SelectUserIndexByOwnerIndex(int roomIndex, int ownerIndex)
        {
            List<GamePlayer> list = SelectSitGamePlayer(roomIndex);
            long userIndex = 0;
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].chairIndex == ownerIndex)
                {
                    userIndex = list[i].useridx;
                    break;
                }
            }
            return userIndex;
        }

        public void UpdateRoom(Room Room)
        {
            RoomTable[Room.index] = Room;
        }

        public int SelectCountSitGamePlayer(int roomIndex)
        {
            return SelectSitGamePlayer(roomIndex).Count;
        }

        public List<GamePlayer> SelectSitGamePlayer(int roomIndex)
        {
            List<GamePlayer> gamePlayers = SelectGamePlayers(roomIndex);
            List<GamePlayer> list = new List<GamePlayer>();

            for (int i = 0; i < gamePlayers.Count; i++)
            {
                if (gamePlayers[i] != null && (gamePlayers[i].state == GamePlayerState.SitWait || gamePlayers[i].state == GamePlayerState.Play))
                {
                    list.Add(gamePlayers[i]);
                }
            }
            list.Sort(CompareOrderByChairIdx);
            return list;
        }

        public List<GamePlayer> SelectGamePlayers(int roomIndex)
        {
            List<GamePlayer> gamePlayers = GamePlayertable[roomIndex];
            List<GamePlayer> list = new List<GamePlayer>();

            for (int i = 0; i < gamePlayers.Count; i++)
            {
                list.Add(gamePlayers[i]);
            }

            list.Sort(CompareOrderByChairIdx);
            return list;
        }


        public int CompareOrderByChairIdx(GamePlayer x, GamePlayer y)
        {
            return x.chairIndex.CompareTo(y.chairIndex);
        }

        public int CompareOrderByOrderNo(GamePlayer x, GamePlayer y)
        {
            return x.orderNo.CompareTo(y.orderNo);
        }

        public GamePlayer SelectGamePlayerByUserIdx(int roomIndex, long userIdx)
        {
            List<GamePlayer> gamePlayers = SelectGamePlayers(roomIndex);
            GamePlayer gamePlayer = null;

            for (int i = 0; i < gamePlayers.Count; i++)
            {
                if (gamePlayers[i] != null && gamePlayers[i].useridx == userIdx)
                {
                    gamePlayer = gamePlayers[i];
                }
            }

            return gamePlayer;
        }

        public void InsertGamePlayer(int roomIndex, GamePlayer gamePlayer)
        {
             List<GamePlayer> gamePlayers = SelectGamePlayers(roomIndex);
            gamePlayer.roomIndex = roomIndex;
            gamePlayers.Add(gamePlayer);

            GamePlayertable[roomIndex] = gamePlayers;
            
        }

        public void UpdateGamePlayer(int roomIndex, GamePlayer gamePlayer)
        {
            List<GamePlayer> gamePlayers = SelectGamePlayers(roomIndex);

            if (gamePlayers != null)
            {
                for (int i = 0; i < gamePlayers.Count; i++)
                {
                    if (gamePlayers[i].useridx == gamePlayer.useridx)
                    {
                        gamePlayers[i] = gamePlayer;
                        break;
                    }
                }
            }
        }

        public long SelectUserIndexByOrderNo(int roomIndex, int orderNo)
        {
            List<GamePlayer> list = SelectSitGamePlayer(roomIndex);
            long userIndex = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].orderNo == orderNo)
                {
                    userIndex = list[i].useridx;
                    break;
                }
            }
            return userIndex;
        }

        public int SelectReadyCount(int roomIndex)
        {
            List<GamePlayer> playerList = SelectSitGamePlayer(roomIndex);
            int count = 0;
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].betStatus == BetStatus.BetReady && playerList[i].lastBetType != BetType.Fold
                   && playerList[i].lastBetType != BetType.Allin && (playerList[i].state == GamePlayerState.Play
                   || playerList[i].state == GamePlayerState.StandWait))
                 {
                    count++;
                 }
            }
            return count;
        }
    }
}

