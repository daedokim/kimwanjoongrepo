using com.dug.common;
using com.dug.UI.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.dug.UI.Networks
{
    public class ResponseData
    {
        private static ResponseData instance;
        Queue<string> messages = new Queue<string>();

        public static ResponseData Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ResponseData();
                }

                return instance;
            }
        }

        public void PushResponseData(string jsonStr)
        {
            messages.Enqueue(jsonStr);

            //Debug.Log(jsonStr);
        }

        public PacketData GetResponseData()
        {
            PacketData data = null;            
            string jsonStr = "";

            if (messages.Count > 0)
            {
                jsonStr = messages.Dequeue();

                //Debug.Log(jsonStr);
                data = JsonConverter.FromJson<PacketData>(jsonStr);
            }
            return data;
        }

        

    }
}
