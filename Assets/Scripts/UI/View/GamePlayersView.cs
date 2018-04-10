using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.model;
using com.dug.UI.dto;
using com.dug.UI.component;
using com.dug.UI.presenter;
using System;

namespace com.dug.UI.view
{
    public class GamePlayersView : MonoBehaviour, IView
    {
        [SerializeField]
        public GameObject gamePlayerPrefab;
        private Transform gamePlayersParent = null;
        private static Vector2[] positions;

        public UIGamePlayer[] gamePlayers = new UIGamePlayer[RoomModel.MAX_GAME_PLAYER_COUNT];

        private GamePlayersPresenter presenter = null;
        
        private void Awake()
        {
            Setpositions();
            gamePlayersParent = GameObject.Find("/UI/backgroundCanvas/GamePlayers").transform;
            presenter = new GamePlayersPresenter(this);
        }

        private void Setpositions()
        {
            positions = new Vector2[RoomModel.MAX_GAME_PLAYER_COUNT];
            positions[0] = new Vector2(499, 567);
            positions[1] = new Vector2(799, 304);
            positions[2] = new Vector2(799, -204);
            positions[3] = new Vector2(501, -522);
            positions[4] = new Vector2(64, -522);
            positions[5] = new Vector2(-384.5f, -522);
            positions[6] = new Vector2(-724, -198);
            positions[7] = new Vector2(-724, 319);
            positions[8] = new Vector2(-401, 567);
        }


        public void CreateGamePlayers()
        {
            GameObject gamePlayer = null;
            Transform tf = null;
            UIGamePlayer script = null;
            for (int i = 0; i < RoomModel.MAX_GAME_PLAYER_COUNT; i++)
            {
                gamePlayer = Instantiate(gamePlayerPrefab, Vector3.zero, Quaternion.identity);
                tf = gamePlayer.transform;
                
                tf.SetParent(gamePlayersParent.transform);
                tf.localPosition = positions[i];
                tf.localScale = new Vector2(2, 2);

                script = gamePlayer.GetComponent<UIGamePlayer>();
                script.ChairIndex = i;

                gamePlayer.SetActive(false);
                this.gamePlayers[i] = script;
            }
        }

        public UIGamePlayer GetGamePlayersByChairIndex(int chairIndex)
        {
            UIGamePlayer gamePlayer = null;

            if(chairIndex >= 0 && chairIndex <= this.gamePlayers.Length - 1)
            {
                gamePlayer = this.gamePlayers[chairIndex];
            }
            return gamePlayer;
        }

        public void OnUpateUI(GamePlayerModel model)
        {
            UIGamePlayer component = GetGamePlayersByChairIndex(model.chairIndex);

            if(component != null)
            {
                component.UpdateGamePlayer(model);
            }
        }


    }
}

    

