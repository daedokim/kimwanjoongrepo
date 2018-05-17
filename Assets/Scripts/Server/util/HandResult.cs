using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandResult
{
    public int cardType;
    public HandType handType = HandType.NONE;
    public int[] hands = {-1, -1, -1, -1, -1, -1, -1};
    public int[] kicks = { -1, -1, -1, -1, -1, -1, -1 };


    public enum HandType
    {
        NONE = -1, ROYAL_STRAIGHT_FLUSH = 0, STRAIGHT_FLUSH = 1, POKER = 2
    }
    
}
