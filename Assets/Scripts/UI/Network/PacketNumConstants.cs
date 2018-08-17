using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.Networks
{
    public class PacketNumConstants
    {
        
        public enum PacketNum
        {
            LOGIN = 1
            , GET_ROOM = 2
            , SIT = 3
            , ADD_ROOM = 4
            , STAND_UP = 5
            , SET_PLAYER_BET = 6

        }
    }

}
