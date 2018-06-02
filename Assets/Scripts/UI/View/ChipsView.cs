using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;
using com.dug.UI.component;
using com.dug.UI.model;
using System;

namespace com.dug.UI.view
{
    public class ChipsView : MonoBehaviour, IView
    {

        [SerializeField]
        public GameObject pokerChipsPrefab;
        private Transform pokerChipsParent = null;

        private static Vector2[] positions;
        private ChipsPresenter presenter;


        [HideInInspector]
        private UIPokerChip[] pokerChips = new UIPokerChip[RoomModel.MAX_GAME_PLAYER_COUNT];
        private UIPokerChip totalChip = null;


        private void Awake()
        {
            Setpositions();
            pokerChipsParent = GameObject.Find("/UI/backgroundCanvas/GamePlayerChips").transform;

            presenter = new ChipsPresenter(this);
        }

        private void Setpositions()
        {
            positions = new Vector2[RoomModel.MAX_GAME_PLAYER_COUNT];
            positions[0] = new Vector2(364, 261);
            positions[1] = new Vector2(522, 184);
            positions[2] = new Vector2(501, -45);
            positions[3] = new Vector2(479, -187);
            positions[4] = new Vector2(64, -187);
            positions[5] = new Vector2(-325, -177);
            positions[6] = new Vector2(-398, -31);
            positions[7] = new Vector2(-383, 152);
            positions[8] = new Vector2(-137, 312);
        }

        public void Clear()
        {
            if(pokerChips != null)
            {
                for(int i = 0;  i < pokerChips.Length; i++)
                {
                    pokerChips[i].Restore();
                }
            }

            if(totalChip != null)
            {
                totalChip.Restore();
            }
            
        }

        public void CreateGamePlayeChips()
        {
            GameObject pokerChip = null;
            Transform tf = null;
            UIPokerChip script = null;
            for (int i = 0; i < RoomModel.MAX_GAME_PLAYER_COUNT; i++)
            {
                pokerChip = Instantiate(pokerChipsPrefab, Vector3.zero, Quaternion.identity);
                tf = pokerChip.transform;

                tf.SetParent(pokerChipsParent.transform);
                tf.localPosition = positions[i];
                tf.localScale = new Vector2(1, 1);

                script = pokerChip.GetComponent<UIPokerChip>();

                pokerChip.SetActive(false);
                this.pokerChips[i] = script;
            }


            CreateTotalChip();
        }

        private void CreateTotalChip()
        {
            GameObject pokerChip = null;
            Transform tf = null;
            UIPokerChip script = null;

            pokerChip = Instantiate(pokerChipsPrefab, Vector3.zero, Quaternion.identity);
            tf = pokerChip.transform;

            tf.SetParent(pokerChipsParent.transform);
            tf.localPosition = new Vector3(95, -62, 0);
            tf.localScale = new Vector2(1, 1);
            script = pokerChip.GetComponent<UIPokerChip>();
            pokerChip.SetActive(false);

            totalChip = script;
        }

        public void ThrowChips(GamePlayerModel model)
        {
            if (model.lastBetType != GamePlayerModel.BetType.Fold
                && model.lastBetType != GamePlayerModel.BetType.Check)
            {
                pokerChips[model.chairIndex].SetAmount(model.lastBet);

                ThrowAni(model.chairIndex);
            }
        }

        private void ThrowAni(int chairIndex)
        {

        }

        public void CollectChips(long totalBet)
        {
            for (int i = 0; i < pokerChips.Length; i++)
            {
                if(pokerChips[i].gameObject.activeSelf == true)
                    pokerChips[i].Restore();
            }

            if (totalChip != null && totalBet > 0)
            {
                totalChip.SetAmount(totalBet);
            }
        }




    }

}
