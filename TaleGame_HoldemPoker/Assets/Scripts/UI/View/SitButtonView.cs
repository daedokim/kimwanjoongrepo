using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.presenter;
using com.dug.UI.model;
using System;

namespace com.dug.UI.view
{
    public class SitButtonView : MonoBehaviour, IView
    {
        private static Vector2[] positions;

        [SerializeField] GameObject sitButtonPrefab = null;
        private Transform sitButtonParent;

        private SitButtonPresenter presenter;

        private void Awake()
        {
            Setpositions();

            sitButtonParent = GameObject.Find("/UI/backgroundCanvas/SitButtons").transform;

            presenter = new SitButtonPresenter(this);
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
        
        public void CreateSitButtons()
        {
            GameObject gamePlayer = null;
            for (int i = 0; i < positions.Length; i++)
            {
                gamePlayer = Instantiate(sitButtonPrefab, Vector3.zero, Quaternion.identity);
                gamePlayer.transform.SetParent(sitButtonParent);
                gamePlayer.transform.localScale = new Vector3(2, 2, 1);
                gamePlayer.SetActive(false);

                gamePlayer.transform.localPosition = positions[i];
            }
        }

        public void OnUpateUI(GamePlayerModel model)
        {
            UIGamePlayer component = GetGamePlayersByChairIndex(model.chairIndex);
            component.UpdateGamePlayer(model);
        }

    }
}
 
