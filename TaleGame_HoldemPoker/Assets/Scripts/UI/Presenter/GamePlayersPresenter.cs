using com.dug.UI.dto;
using com.dug.UI.events;
using com.dug.UI.manager;
using com.dug.UI.model;
using com.dug.UI.view;
using UnityEngine;

namespace com.dug.UI.presenter
{
    [DisallowMultipleComponent]
    public class GamePlayersPresenter : IPresenter
    {
        private GamePlayersView view;
        private GameManager manager;

        private GamePlayerModel model = new GamePlayerModel();

        public GamePlayersPresenter(GamePlayersView view)
        {
            this.view = view;
            this.manager = GameManager.Instance;

            this.view.CreateGamePlayers();

            GameEvent.Instance.AddGamePlayerEvent(OnUpdateGamePlayer);
        }

        private void OnUpdateGamePlayer(GamePlayerModel model)
        {   
            this.view.OnUpateUI(model);
        }
    }
}

