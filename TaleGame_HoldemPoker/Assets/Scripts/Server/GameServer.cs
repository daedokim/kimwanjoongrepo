
namespace com.dug.Server
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using com.dug.Server;
    using com.dug.Server.vo;
    using com.dug.Server.Util;
    using com.dug.Server.Controller;
    using System;

    public class GameServer : Singleton<GameServer>
    { 
        protected GameServer() { }
        private DataTable table;
        private PokerController controller;
        private PokerScheduleController scheduler;
      

        private void Awake()
        {
            table = new DataTable();
            table.InitTable();

            controller = new PokerController(table);
            scheduler = new PokerScheduleController(table);
        }

        private void Start()
        {
            InvokeRepeating("Thread", 1.0f, 2.0f);
        }      
        
        /**
         * 서버 스케줄러를 실행한다.
         */
        private void Thread()
        {
            scheduler.Thread();
        }
       
        public Room GetRoom(int roomIndex)
        {
            return controller.GetRoom(roomIndex);
        }

        public Room SetStageClear(int roomIndex, long useridx, int stage)
        {
            return controller.SetStageClear(roomIndex, useridx, stage);
        }

        public Room SetPlayerBetting(int roomIndex, long userIdx, int betType, long callAmount, long betAmount)
        {
            return controller.SetPlayerBetting(roomIndex, userIdx, betType, callAmount, betAmount);
        }


    }
}
    

