using com.dug.common;
using com.dug.UI.Events;
using com.dug.UI.Managers;
using com.dug.UI.Networks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTest : MonoBehaviour {

    private void Start()
    {
        GameEventHandler.Instance.AddHandler(this, ResponseManager.ON_DATA_RECIEVE, OnDataReceive);
        //CommonNetwork.Instance.Login();

    }

    private void OnDataReceive(object obj)
    {
        PacketData data = (PacketData)obj;
        
    }
}
