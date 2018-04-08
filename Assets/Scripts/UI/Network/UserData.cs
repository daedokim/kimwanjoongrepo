using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.network
{
    public class UserData : Singleton<UserData>
    {
        public long userIndex = 1000;
        public string nickName = "dkdkdk";
        public long coin = 10000000;
        public long buyInLeft = 10000;
    }

}

