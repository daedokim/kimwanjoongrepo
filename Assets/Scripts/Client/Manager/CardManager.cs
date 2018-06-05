using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.util;
using com.dug.UI.component;

namespace com.dug.UI.manager
{
    public class CardManager : Singleton<CardManager>
    {
        public static int CARD_TOTALCOUNT = 52;
        [SerializeField]
        public Sprite[] faces;
        [SerializeField]
        public Sprite back;

        public GameObject cardPrefab;

        private UICard[] cards = new UICard[CARD_TOTALCOUNT];
        public GameObjectPool<GameObject> objectPool;

        public void Awake()
        {
            CreateCards();
        }

        public void CreateCards()
        {
            GameObject cardInstance = null;
            UICard script = null;
            for (int i = 0; i < CARD_TOTALCOUNT; i++)
            {
                cardInstance = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
                script = cardInstance.GetComponent<UICard>();
                script.Draw(faces[i], back, i);
                script.transform.SetParent(this.transform);
                cardInstance.SetActive(false);

                cards[i] = script;
            }
        }

        public UICard GetCards(int index)
        {
            if (index < 0 || index > 52 || cards[index] == null)
                throw new System.Exception("인덱스를 넘어감");

            UICard card = cards[index];

            card.transform.localPosition = new Vector3(0, 0, 0);
            card.transform.localScale = new Vector3(1, 1, 1);
            card.transform.SetParent(this.transform);
            card.gameObject.SetActive(false);
            card.SetAlpha(1);

            return cards[index];
        }

    }
}

