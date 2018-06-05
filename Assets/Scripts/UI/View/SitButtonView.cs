using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.presenter;
using com.dug.UI.model;
using com.dug.UI.component;
using com.dug.UI.dto ;
using System;

namespace com.dug.UI.view
{
    public class SitButtonView : MonoBehaviour, IView
    {
        private static Vector2[] positions;

        private UISitButton[] sitButtons = new UISitButton[RoomModel.MAX_GAME_PLAYER_COUNT];

        [SerializeField] GameObject sitButtonPrefab = null;
        private Transform sitButtonParent;

        private SitButtonPresenter presenter = null;

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
            GameObject sitButton = null;
            for (int i = 0; i < positions.Length; i++)
            {
                sitButton = Instantiate(sitButtonPrefab, Vector3.zero, Quaternion.identity);
                sitButton.transform.SetParent(sitButtonParent);
                sitButton.transform.localScale = new Vector3(2, 2, 1);
                sitButton.transform.localPosition = positions[i];
                sitButtons[i] = sitButton.GetComponent<UISitButton>();
                sitButtons[i].chairIndex = i;
            }
        }

        public void OnUpateUI(List<int> chairIndice)
        {
            for(int i = 0; i < sitButtons.Length; i++)
            {
                if (chairIndice.IndexOf(sitButtons[i].chairIndex) != -1)
                {
                    sitButtons[i].Enable = false;
                }
                else
                {
                    sitButtons[i].Enable = true;
                }
            }
        }

        public UISitButton GetSitButtonByChairIndex(int chairIndex)
        {
            UISitButton sitButton = null;

            if (chairIndex > 0 && chairIndex <= this.sitButtons.Length - 1)
            {
                sitButton = this.sitButtons[chairIndex];
            }
            return sitButton;
        }

    }
}
 
