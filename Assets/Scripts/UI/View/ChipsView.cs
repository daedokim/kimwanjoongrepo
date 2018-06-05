using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;
using com.dug.UI.component;
using com.dug.UI.model;
using System;
using DG.Tweening;

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
        private List<GameObject> throwAniChips = new List<GameObject>();


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
                    pokerChips[i].gameObject.SetActive(false);
                }
            }

            if(totalChip != null)
            {
                totalChip.Restore();
                totalChip.gameObject.SetActive(false);
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
                tf.localPosition = Vector2.zero;
                tf.localScale = new Vector2(1f, 1f);

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
            tf.localScale = new Vector2(1f, 1f);
            script = pokerChip.GetComponent<UIPokerChip>();
            pokerChip.SetActive(false);

            totalChip = script;
        }

        public void ThrowChips(int chairIndex, long amount)
        {
            pokerChips[chairIndex].SetAmount(amount);
            ThrowAni(chairIndex, amount);
        }

        private void ThrowAni(int chairIndex, long amount)
        {
            Vector2 gamePos = GamePlayersView.positions[chairIndex];
            Vector2 dest = positions[chairIndex];

            int i = 0;
            
            for(i = 0; i < 3; i++)
            {
                GameObject go = ChipsManager.Instance.GetChip(10);
                go.name = "throwChip" + i;
                go.transform.SetParent(pokerChipsParent.transform);
                go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                go.transform.localPosition = new Vector2(gamePos.x + (2 * i), gamePos.y + (2 * i));
                go.transform.DOLocalMove(dest, 0.2f).SetDelay(0.1f * i).OnComplete(()=> { OnThrowComplete(go); });
                
            }
            DOVirtual.DelayedCall(0.4f, () => { OnThrowAniComplete(chairIndex, dest, amount); });
        }

        private void OnThrowAniComplete(int chairIndex, Vector2 dest, long amount)
        {
            UIPokerChip chip = pokerChips[chairIndex];
            chip.gameObject.SetActive(true);
            chip.transform.localPosition = dest;
            chip.transform.localScale = new Vector2(1, 1);
            chip.SetAmount(amount);   
        }

        private void OnThrowComplete(GameObject go)
        {
            ChipsManager.Instance.ReStore(go.transform); 
        }

        public void CollectChips(long totalBet)
        {
            for (int i = 0; i < pokerChips.Length; i++)
            {
                if(pokerChips[i].gameObject.activeSelf == true)
                {
                    UIPokerChip uIPokerChip = pokerChips[i];
                    uIPokerChip.transform.DOLocalMove(totalChip.transform.localPosition, 0.2f).OnComplete(()=> OnCollectAniComplete(uIPokerChip));
                }
            }

            DOVirtual.DelayedCall(0.1f, () => {

                if (totalChip != null && totalBet > 0)
                {
                    totalChip.SetAmount(totalBet);
                    totalChip.gameObject.SetActive(true);
                }
            });
        }

        private void OnCollectAniComplete(UIPokerChip uIPokerChip)
        {
            uIPokerChip.Restore();
            uIPokerChip.gameObject.SetActive(false);
        }
    }

}
