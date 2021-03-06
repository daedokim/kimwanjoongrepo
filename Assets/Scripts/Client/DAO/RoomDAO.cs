﻿
namespace com.dug.UI.DAO
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using com.dug.Server;
    using com.dug.UI.DTO;
    using System;

    public class RoomDAO
    {
        public Room GetRoom(int roomIndex)
        {
            string str = JsonUtility.ToJson(GameServer.Instance.GetRoom(roomIndex), true);
            Room returnVal = JsonUtility.FromJson<Room>(str);

            return returnVal;
        }

        /**
         * 
         *  사용자에게 현재 스테이지(턴)이 완료 되었다는것을 보고 
         */
        public Room SetStageClear(int roomIndex, long useridx, int stage)
        {
            string str = JsonUtility.ToJson(GameServer.Instance.SetStageClear(roomIndex, useridx, stage), true);
            Room returnVal = JsonUtility.FromJson<Room>(str);

            return returnVal;
        }

        /**
         *  플레이어 베팅상태 업데이트 
         */
        public Room SetPlayerBetting(int roomIndex, long userIdx, BetType betType, long callAmount, long betAmount)
        {
            string str = JsonUtility.ToJson(GameServer.Instance.SetPlayerBetting(roomIndex, userIdx, (int)betType, callAmount, betAmount), true);
            Room returnVal = JsonUtility.FromJson<Room>(str);

            return returnVal;
        }

        public CRUDResult DoSit(int roomIndex, long userIndex, int chairIndex, long buyInLeft)
        {
            string str = JsonUtility.ToJson(GameServer.Instance.DoSit(roomIndex, userIndex, chairIndex, buyInLeft), true);
            CRUDResult returnVal = JsonUtility.FromJson<CRUDResult>(str);

            return returnVal;
        }

        public CRUDResult StandUp(int roomIndex, long userIndex)
        {
            string str = JsonUtility.ToJson(GameServer.Instance.DoStandUp(roomIndex, userIndex), true);
            CRUDResult returnVal = JsonUtility.FromJson<CRUDResult>(str);

            return returnVal;
        }
    }
}
 
