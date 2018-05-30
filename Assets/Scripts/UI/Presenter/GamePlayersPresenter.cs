using System;
using com.dug.UI.dto;
using com.dug.UI.events;
using com.dug.UI.manager;
using com.dug.UI.model;
using com.dug.UI.view;
using UnityEngine;
using System.Collections.Generic;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class GamePlayersPresenter : IPresenter
    {
        private GamePlayersView view;
        private GameManager manager;

        private GamePlayerModel model = new GamePlayerModel();
        private GameEvent gameEvent = GameEvent.Instance;

        public GamePlayersPresenter(GamePlayersView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.view.CreateGamePlayers();

            gameEvent.AddGamePlayerEvent(OnUpdateGamePlayer);
            gameEvent.AddHandoutCompleteEvent(OnHandOutComplete);
            gameEvent.AddClearEvent(OnClearAll);
        }

        private void OnHandOutComplete(int charIndex)
        {
            this.view.ShowOwnCards(charIndex);
        }

        private void OnUpdateGamePlayer(GamePlayerModel model)
        {   
            this.view.OnUpateUI(model);
        }

        private void OnClearAll()
        {
            this.view.Clear();
        }

    }
}

