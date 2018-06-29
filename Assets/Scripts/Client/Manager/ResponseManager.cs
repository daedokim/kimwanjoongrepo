using com.dug.common;
using com.dug.UI.Events;
using com.dug.UI.network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.Managers
{

    public class ResponseManager : Singleton<ResponseManager>
    {
        public static string ON_DATA_RECIEVE = "ResponseManager.ON_DATA_RECIEVE";
      

        private void Start()
        {
            StartCoroutine(CheckMessageQue());
        }

        IEnumerator CheckMessageQue()
        {
            //1초 주기로 탐색
            WaitForSeconds waitSec = new WaitForSeconds(1);
            PacketData data = null;

            while (true)
            {
                data = ResponseData.Instance.GetResponseData();

                if (data != null)
                {
                    GameEventHandler.Instance.Invoke(ON_DATA_RECIEVE, data);
                }
                yield return waitSec;
            }
        }
    }
}

