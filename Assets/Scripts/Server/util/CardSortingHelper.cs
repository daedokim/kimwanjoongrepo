

namespace com.dug.Server.Util
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CardSortingHelper
    {

        const int CARD_COUNT = 52;
        private List<int> cards = new List<int>();


        public CardSortingHelper()
        {
            for (int i = 0; i < CARD_COUNT; i++)
            {
                cards.Add(i);
            }

            Shuffle();
        }

        public void Shuffle()
        {
            System.Random random = new System.Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = random.Next(i, cards.Count - 1);
                int temporary = cards[i];
                cards[i] = cards[j];
                cards[j] = temporary;
            }
        }

        public int Pop()
        {
            if (HasNext() == false)
                throw new System.Exception("Has Not Card Exception");

            int cardNum = cards[0];
            cards.RemoveAt(0);

            return cardNum;
        }

        public bool HasNext()
        {
            return cards.Count > 0;
        }
    }

}


