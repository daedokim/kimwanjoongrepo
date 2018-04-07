﻿using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;

namespace com.dug.UI.view
{
    public class StatusView : MonoBehaviour, IView
    {
        [SerializeField] Text statusText = null;

        private StatusPresenter presenter;

        private void Awake()
        {
            presenter = new StatusPresenter(this);
        }

        public void SetStatusText(string text)
        {
            statusText.text = text;
        }
    }
}
 
