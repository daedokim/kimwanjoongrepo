using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.presenter;
using com.dug.UI.manager;
using com.dug.UI.model;
using System;

namespace com.dug.UI.view
{
    public class TableCardView : MonoBehaviour, IView
    {
        private TableCardPresenter presenter;
        private CardManager cardManager;

        private float[] XPOSITIONS = { -187, -63, 61, 181, 302};
        private float YPOSITION = 103;
        private float SCALE = 1.5f;

        private UICard[] cards = new UICard[5];
        private Transform tableParent;

        private void Awake()
        {
            tableParent = GameObject.Find("/UI/backgroundCanvas/TableCards").transform;
            presenter = new TableCardPresenter(this);
            cardManager = CardManager.Instance;
        }

        public void Flop(int cardIndex1, int cardIndex2, int cardIndex3)
        {
            UICard card = null;

            int[] cardIndice = { cardIndex1, cardIndex2, cardIndex3 };

            for(int i = 0; i < 3; i++)
            {
                card = cardManager.GetCards(cardIndice[i]);
                card.transform.SetParent(tableParent);
                card.gameObject.SetActive(true);
                card.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
                card.transform.localPosition = new Vector3(XPOSITIONS[i], YPOSITION);
                card.SetFace(true);
                cards[i] = card;
            }
        }

        public void Turn(int cardIndex)
        {
            UICard card = cardManager.GetCards(cardIndex);
            card.transform.SetParent(tableParent);
            card.gameObject.SetActive(true);
            card.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
            card.transform.localPosition = new Vector3(XPOSITIONS[3], YPOSITION);
            card.SetFace(true);
            cards[3] = card;
        }

        public void River(int cardIndex)
        {
            UICard card = cardManager.GetCards(cardIndex);
            card.transform.SetParent(tableParent);
            card.gameObject.SetActive(true);
            card.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
            card.transform.localPosition = new Vector3(XPOSITIONS[4], YPOSITION);
            card.SetFace(true);
            cards[4] = card;
        }
    }
}
