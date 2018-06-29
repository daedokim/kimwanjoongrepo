using com.dug.UI.presenter;
using com.dug.UI.util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.dug.UI.view
{
    public class TopPanelView : MonoBehaviour, IView
    {
        TopPanelPresenter presenter;

        [SerializeField]
        public Text coinText;

        private void Awake()
        {
            presenter = new TopPanelPresenter(this);
            Debug.Log("eeee");
        }

        private void Start()
        {
            Debug.Log("ccc");
        }

        public void SetCoin(long coin)
        {
            coinText.text = GameUtil.MakePriceString(coin);
        }
    }
}