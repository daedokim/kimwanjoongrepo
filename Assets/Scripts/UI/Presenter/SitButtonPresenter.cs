using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.view;
using com.dug.UI.manager;
using com.dug.UI.events;
using com.dug.UI.model;
using System;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class SitButtonPresenter : IPresenter
    {
        private SitButtonView view;
        private GameManager manager;

        public SitButtonPresenter(SitButtonView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.view.CreateSitButtons();
            GameEvent.Instance.AddGamePlayerEvent(OnUpdateGamePlayer);
        }

        private void OnUpdateGamePlayer(GamePlayerModel model)
        {
            this.view.OnUpateUI(model);
        }



    }
}
