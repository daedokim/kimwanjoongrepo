using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.presenter;
using com.dug.UI.Models;
using com.dug.UI.component;
using DG.Tweening;
using com.dug.UI.Events;

namespace com.dug.UI.view
{
    public class GamePlayerCardView : MonoBehaviour, IView
    {

        [SerializeField]
        public GameObject cardPrefab = null;
        private Transform tableCardParent = null;
        private static Vector2[] positions = null;

        private Vector2 dealerPosition;

        [SerializeField]
        public Sprite[] cardResources;

        public UIGamePlayerTableCard[] tableCards = new UIGamePlayerTableCard[RoomModel.MAX_GAME_PLAYER_COUNT];

        private GamePlayerCardPresenter presenter;

        private void Awake()
        {
            dealerPosition = new Vector2(47, 443);
            Setpositions();
            tableCardParent = GameObject.Find("/UI/backgroundCanvas/GamePlayerTableCards").transform;
            presenter = new GamePlayerCardPresenter(this);
            
        }

        private void Setpositions()
        {
            positions = new Vector2[RoomModel.MAX_GAME_PLAYER_COUNT];
            positions[0] = new Vector2(381, 354);
            positions[1] = new Vector2(610, 165);
            positions[2] = new Vector2(598, -50);
            positions[3] = new Vector2(490, -291);
            positions[4] = new Vector2(64, -291);
            positions[5] = new Vector2(-342, -273);
            positions[6] = new Vector2(-488, -73);
            positions[7] = new Vector2(-536, 173);
            positions[8] = new Vector2(-241, 338);
        }

        public void CreateTableCards()
        {
            GameObject tableCard = null;
            Transform tf = null;
            UIGamePlayerTableCard script = null;
            for (int i = 0; i < RoomModel.MAX_GAME_PLAYER_COUNT; i++)
            {
                tableCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
                tf = tableCard.transform;

                tf.SetParent(tableCardParent.transform);
                tf.localPosition = dealerPosition;
                tf.localScale = new Vector2(0, 0);

                script = tableCard.GetComponent<UIGamePlayerTableCard>();
                script.chairIndex = i;
                script.ImageSource = cardResources[i];

                tableCard.SetActive(false);
                this.tableCards[i] = script;
            }
        }

        public void ReturnCard(int chairIndex)
        {
            tableCards[chairIndex].transform.DOLocalMove(dealerPosition, 0.2f).SetEase(Ease.InOutQuad);
            tableCards[chairIndex].transform.DOScale(new Vector3(0, 0), 0.2f).OnComplete(() => OnClearComplete(tableCards[chairIndex]));
        }

        public void Clear(float duration, float delay = 0f)
        {
            for (int i = 0; i < tableCards.Length; i++)
            {
                tableCards[i].transform.DOLocalMove(dealerPosition, duration).SetDelay(delay).SetEase(Ease.InOutQuad);
                tableCards[i].transform.DOScale(new Vector3(0, 0), duration).SetDelay(delay).OnComplete(()=>OnClearComplete(tableCards[i]));
            }
        }

        private void OnClearComplete(UIGamePlayerTableCard card)
        {
            card.gameObject.SetActive(false);
        }

        public void HandOut(int chairIndex, float duration, float delay)
        {
            for (int i = 0; i < tableCards.Length; i++)
            {
                if(chairIndex == tableCards[i].chairIndex)
                {
                    tableCards[i].gameObject.SetActive(true);

                    tableCards[i].transform.DOLocalMove(positions[i], duration).SetDelay(delay).SetEase(Ease.InOutQuad);
                    tableCards[i].transform.DOScale(new Vector3(1, 1), duration).SetDelay(delay).OnComplete(() => OnHandoutComplete(chairIndex));
                }
            }
        }

        private void OnHandoutComplete(int chairIndex)
        {
            GameEvent.Instance.InvokeHandoutCompleteEvent(chairIndex);
        }
    }

}

