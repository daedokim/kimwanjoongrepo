using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.Networks
{
    public class CallbackData
    {
        public int packetNum;
        public Func<PacketData> callback;

        public CallbackData(int packetNum, Func<PacketData> callback)
        {
            this.packetNum = packetNum;
            this.callback = callback;
        }
      
    }

}
