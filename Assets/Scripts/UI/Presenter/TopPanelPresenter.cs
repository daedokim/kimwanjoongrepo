using com.dug.UI.Events;
using com.dug.UI.Managers;
using com.dug.UI.network;
using com.dug.UI.view;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class TopPanelPresenter : IPresenter
    {
        private TopPanelView view;
        private GameManager manager;

        public TopPanelPresenter(TopPanelView view)
        {
            this.view = view;

            manager = GameManager.Instance;

            GameEventHandler.Instance.AddHandler(this, UserManager.USER_INFO_UPDATE, OnUpdateUserInfo);
        }

        private void OnUpdateUserInfo(object obj)
        {
            this.view.SetCoin(UserData.Instance.coin);
        }
    }
}
