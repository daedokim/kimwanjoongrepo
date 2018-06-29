using com.dug.UI.Events;
using com.dug.UI.Networks;
using com.dug.UI.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WebSocketSharp;

namespace com.dug.common
{
    public class SocketBase
    {
        public const int BUFFER_SIZE = 1024 * 10;
        public const int RECEIVE_BUFFER_SIZE = 1024 * 10 * 4;
        public const int CONNECT_TIMEOUT = 30 * 1000;
        public const string REQUEST_ORIGIN = "http://localhost:1213";

        public const string SOCKET_CONNECTED = "SocketBase.SOCKET_CONNECTED";
        public const string SOCKET_CLOSED = "SocketBase.SOCKET_CLOSED";

        WebSocket socket;

        private string host;
        private int port;
        private string channel;
        private string connectStr;
        Queue<string> messages = new Queue<string>();
        Queue<PacketData> requestQue = new Queue<PacketData>();

        public bool IsConnected { get; set; }

        public SocketBase()
        {

        }

        public void Connect(string host, int port, string channel)
        {
            this.host = host;
            this.port = port;
            this.channel = channel;
            this.connectStr = "ws://" + this.host + ":" + this.port + "/" + this.channel;            

            socket = new WebSocket(connectStr);
            socket.OnMessage += OnGetMessage;
            socket.OnOpen += OnOpen;
            socket.OnClose += OnClose;
            socket.Origin = REQUEST_ORIGIN;

            socket.ConnectAsync();
        }

        public void OnOpen(object sender, EventArgs e)
        {
            IsConnected = true;

            GameUtil.DebugLog("웹 소켓 연결");
            GameEventHandler.Instance.Invoke(SOCKET_CONNECTED);
        }

        private void CheckRequest()
        {
            PacketData data = null;
            if (requestQue.Count > 0)
            {
                data = requestQue.Dequeue();

                if (data != null)
                {
                    Send(data);
                }
            }
        }

        public void CheckStatus()
        {
            if(IsConnected == true)
            {
                CheckRequest();
            }
        }

        public void Send(PacketData data)
        {
            if(socket != null && data != null)
            {
                if (IsConnected == false)
                {
                    requestQue.Enqueue(data);
                    return;
                }

                string str = JsonConverter.ToJson(data);
                socket.Send(str);
            }
        }        

        private void OnClose(object sender, CloseEventArgs e)
        {
            IsConnected = false;

            GameUtil.DebugLog("웹 소켓 연결 끊김");
            GameEventHandler.Instance.Invoke(SOCKET_CLOSED);
        }

        private void OnGetMessage(object sender, MessageEventArgs e)
        {
            string jsonStr = Encoding.Default.GetString(e.RawData);
            jsonStr = jsonStr.Replace("\\", "");
            jsonStr = jsonStr.Substring(1, jsonStr.Length - 2);

            ResponseData.Instance.PushResponseData(jsonStr);
        }
    }
}
