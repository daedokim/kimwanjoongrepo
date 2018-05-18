using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HandResult
{
    public CardType cardType = CardType.NONE;
    public HandType handType = HandType.NONE;
    public int[] hands = {-1, -1, -1, -1, -1, -1, -1};
    public int[] kicks = { -1, -1, -1, -1, -1, -1, -1 };


    public enum CardType
    {
        NONE = -1, SPADE = 1, DIAMOND = 2, HEART = 3, CLOVER = 4
    }


    public enum HandType
    {
        NONE = -1, ROYAL_STRAIGHT_FLUSH = 0, STRAIGHT_FLUSH = 1, POKER = 2, FULL_HOUSE = 3, FLUSH = 4, STRAIT = 5,
        TRIPLE = 6, TWO_PAIR = 7, ONE_PAIR = 8, TITLE = 9
    }
    
}
