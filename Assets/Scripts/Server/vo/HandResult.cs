using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.dug.Server.vo
{
    [Serializable]
    public class HandResult
    {
        public CardType cardType = CardType.NONE;
        public HandType handType = HandType.NONE;
        public int[] hands = { -1, -1, -1, -1, -1, -1, -1 };
        public int[] kicks = { -1, -1, -1, -1, -1, -1, -1 };
        public int[] madeCards = { -1, -1, -1, -1, -1, -1, -1 };


        public enum CardType
        {
            NONE = -1, SPADE = 1, DIAMOND = 2, HEART = 3, CLOVER = 4
        }


        public enum HandType
        {
            NONE = -1, ROYAL_STRAIGHT_FLUSH = 9, STRAIGHT_FLUSH = 8, POKER = 7, FULL_HOUSE = 6, FLUSH = 5, STRAIT = 4,
            TRIPLE = 3, TWO_PAIR = 2, ONE_PAIR = 1, TITLE = 0
        }

        public void AddMadeCard(int card)
        {
            bool isFound = false;
            int i = 0;

            for (i = 0; i < madeCards.Length; i++)
            {
                if (card == madeCards[i])
                {
                    isFound = true;
                    break;
                }
            }

            if(isFound == false)
            {
                for (i = 0; i < madeCards.Length; i++)
                {
                    if (madeCards[i] == -1)
                    {
                        madeCards[i] = card;
                        break;
                    }
                }
            }
        }
    }
}

