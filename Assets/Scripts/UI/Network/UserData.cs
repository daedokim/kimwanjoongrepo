using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.network
{
    public class UserData
    {
        private static UserData instance;

        public long userIndex = 1000;
        public string userId;
        public string nickName = "dkdkdk";
        public long coin = 10000000;
        public long buyInLeft = 10000;
        public bool isAutoRefill = false;
        public bool guestMode;
        public int osType;
        public bool isLogin = false;

        public static UserData Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new UserData();
                }

                return instance;
            }
        }

        public void Update(User user)
        {
            userIndex = user.userIndex;
            coin = user.coin;
            userId = user.userId;
            guestMode = user.guestMode;
        }
    }

}

