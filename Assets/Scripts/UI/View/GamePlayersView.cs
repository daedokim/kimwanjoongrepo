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

        public UIGamePlayer[] gamePlayerList = new UIGamePlayer[RoomModel.MAX_GAME_PLAYER_COUNT];

        private GamePlayersPresenter presenter = null;
        
        private void Awake()
        {
            gamePlayersParent = GameObject.Find("/UI/Canvas/GamePlayers").transform;
            presenter = new GamePlayersPresenter(this);
        }

        public void CreateGamePlayers()
        {
            GameObject gamePlayer = null;
            UIGamePlayer script = null;
            for (int i = 0; i < RoomModel.MAX_GAME_PLAYER_COUNT; i++)
            {
                gamePlayer = Instantiate(gamePlayerPrefab, Vector3.zero, Quaternion.identity);
                gamePlayer.transform.SetParent(gamePlayersParent.transform);
                gamePlayer.transform.localPosition = new Vector3(-196 + ((i % 5) * 70), 4 + ((i/5) * -60), 0);

                script = gamePlayer.GetComponent<UIGamePlayer>();
                script.ChairIndex = i;
                this.gamePlayerList[i] = script;
            }
        }

        public UIGamePlayer GetGamePlayersByChairIndex(int chairIndex)
        {
            if(chairIndex <= 0 || chairIndex > this.gamePlayerList.Length - 1)
            {
                throw new System.Exception("인덱스를 벗어 난다.");
            }
            return this.gamePlayerList[chairIndex];
        }

        public void OnUpateUI(GamePlayerModel model)
        {
            UIGamePlayer component = GetGamePlayersByChairIndex(model.chairIndex);
            component.UpdateGamePlayer(model);
        }


    }
}

    

