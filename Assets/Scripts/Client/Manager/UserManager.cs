
using com.dug.common;
using com.dug.UI.Events;
using com.dug.UI.Managers;
using com.dug.UI.Networks;
using com.dug.UI.util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    public static readonly string USER_INFO_UPDATE = "UserManager.USER_INFO_UPDATE";
    public static readonly string USER_LOGIN_COMPLETE = "UserManager.USER_LOGIN_COMPLETE";

    void Start()
    {
        GameEventHandler.Instance.AddHandler(this, ResponseManager.ON_DATA_RECIEVE, OnDataReceive);

        Login();
    }

    public void Login()
    {
        User user = new User();

        user.userId = GameUtil.GetDeviceId();
        user.osType = 1;
        user.guestMode = true;

        CommonNetwork.Instance.Login(user);
    }

    private void OnDataReceive(object obj)
    {
        PacketData data = (PacketData)obj;

        switch((PacketNumConstants.PacketNum)data.packetNum)
        {
            case PacketNumConstants.PacketNum.LOGIN:
                OnLoginComplete(data);
                break;
        }
    }

    private void OnLoginComplete(PacketData data)
    {
        object dic = null;
        data.data.TryGetValue("User", out dic);
        User user = JsonConverter.GetObject<User>((Dictionary<string, object>)dic);

        if(user != null)
        {
            UserData session = UserData.Instance;
            session.Update(user);
            session.isLogin = true;

            GameEventHandler.Instance.Invoke(UserManager.USER_INFO_UPDATE);
            GameEventHandler.Instance.Invoke(UserManager.USER_LOGIN_COMPLETE);
        }
    }
}
