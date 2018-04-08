using System;
using System.Collections;
using System.Collections.Generic;
using com.dug.UI.model;
using com.dug.UI.manager;
using com.dug.UI.network;
using UnityEngine;
using UniRx;
using UnityEngine.UI;


namespace com.dug.UI.component
{
    public class UISitButton : MonoBehaviour
    {
        public int chairIndex = -1;
        private bool enable;

        public bool Enable { set { this.enable = value; }}

        void Awake()
        {
            this.gameObject.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => {
                GameManager.Instance.SitChair(chairIndex);
                this.enable = false;
            });
        }

        private void Start()
        {
            this.ObserveEveryValueChanged(x => x.enable).Where(x=>x).Subscribe(x => {
                this.gameObject.SetActive(enable);
            });
        }


        public void ResetButton()
        {
        }
    }

}
