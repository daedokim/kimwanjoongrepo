﻿using com.dug.common;
using com.dug.UI.Events;
using com.dug.UI.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


namespace com.dug.UI.Networks
{
    public class CommonNetwork
    {        
        SocketBase socket;
        private Timer enterFrameTimer;
        private static CommonNetwork instance;

        private bool isConnected = false;
        private bool isReconnected = false;                

        public CommonNetwork()
        {
            socket = new SocketBase();
            GameEventHandler.Instance.AddHandler(this, SocketBase.SOCKET_CONNECTED, OnSocketConnect);
            GameEventHandler.Instance.AddHandler(this, SocketBase.SOCKET_CLOSED, OnSocketClose);
            socket.Connect("localhost", 1213, "ws");
        }

        public static CommonNetwork Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommonNetwork();
                }

                return instance;
            }
        }

        protected void OnSocketConnect(object obj)
        {
            isConnected = true;

            enterFrameTimer = new Timer();
            enterFrameTimer.Interval = 100; // 0.1 second
            enterFrameTimer.Elapsed += new ElapsedEventHandler(OnEnterFrame);
            enterFrameTimer.Start();
        }

        protected void OnSocketClose(object obj)
        {
            isConnected = false;
        }

        protected void OnEnterFrame(object sender, ElapsedEventArgs e)
        {
            if (socket != null)
            {
                socket.CheckStatus();
            }
        }

        public void Login(User user)
        {
            PacketData packet = new PacketData();
            packet.packetNum = (int)PacketNumConstants.PacketNum.LOGIN;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userId", user.userId);
            data.Add("osType", user.osType);
            data.Add("guestMode", user.guestMode);

            packet.data = data;                               

            socket.Send(packet);
        }

        public void GetRoom(long userIndex, int roomIndex)
        {
            PacketData packet = new PacketData();
            packet.packetNum = (int)PacketNumConstants.PacketNum.GET_ROOM;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userIndex", userIndex);
            data.Add("roomIndex", roomIndex);

            packet.data = data;

            socket.Send(packet);
        }

    }

}
