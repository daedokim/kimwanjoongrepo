using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.view;
using com.dug.UI.manager;

namespace com.dug.UI.presenter
{
    public class GamePlayerCardPresenter : IPresenter
    {
        private GamePlayerCardView view;
        private GameManager manager;

        public GamePlayerCardPresenter(GamePlayerCardView view)
        {
            this.view = view;
            manager = GameManager.Instance;

            this.view.CreateTableCards();
        }


    }
}


