using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

using com.dug.Server.Util;
using com.dug.Server.vo;
using System;

public class Test : MonoBehaviour {

    [SerializeField]
    public HandResult result;

    delegate HandResult handDelegate(int[] cards);

    [SerializeField]
    public int[] cards = new int[] { 0, 9, 10, 11, 12, 6,7};

    [SerializeField]
    public int[] cardsType = new int[] { 0, 9, 10, 11, 12, 6, 7 };


    [SerializeField]
    public bool flag = false;
    

    private handDelegate[] hands = new handDelegate[10];

    private void Awake()
    {
        PokerHandUtil handUtil = new PokerHandUtil();

        hands[(int)HandResult.HandType.ROYAL_STRAIGHT_FLUSH] = handUtil.CheckRoyalStraightFlush;
        hands[(int)HandResult.HandType.STRAIGHT_FLUSH] = handUtil.CheckStraightFlush;
        hands[(int)HandResult.HandType.POKER] = handUtil.CheckPoker;
        hands[(int)HandResult.HandType.FULL_HOUSE] = handUtil.CheckFullHouse;
        hands[(int)HandResult.HandType.FLUSH] = handUtil.CheckFlush;        
        hands[(int)HandResult.HandType.STRAIT] = handUtil.CheckStraight;
        hands[(int)HandResult.HandType.TRIPLE] = handUtil.CheckTriple;
        hands[(int)HandResult.HandType.TWO_PAIR] = handUtil.CheckTwoPairs;
        hands[(int)HandResult.HandType.ONE_PAIR] = handUtil.CheckOnePair;
        hands[(int)HandResult.HandType.TITLE] = handUtil.CheckTitle;


        this.ObserveEveryValueChanged(x => x.flag).Skip(1).Subscribe(x => {

            CheckHands();
        });


        int count = 0;

        while(count < 1)
        {
            CheckHands();

            if(result != null && result.handType == HandResult.HandType.ONE_PAIR)
            {
                break;
            }
            count++;
        }

    }

    private void CheckHands()
    {
        result.handType = HandResult.HandType.NONE;
        SetCards();

        cards[0] = 41;
        cards[1] = 44;
        cards[2] = 10;
        cards[3] = 29;
        cards[4] = 3;
        cards[5] = 17;
        cards[6] = 35;

        Array.Sort(cards);

        for (int i = hands.Length -1 ; i >= 0; i--)
        {
            result = hands[i](cards);

            if (result.handType != HandResult.HandType.NONE)
            {
                print(result.handType);
                break;
            }
        }
    }

    private void SetCards()
    {
        cards.Initialize();

        CardSortingHelper helper = new CardSortingHelper();
        helper.Initialize();

        for (int j = 0; j < 7; j++)
        {
            cards[j] = helper.Pop();
        }

        Array.Sort(cards);

        for(int i = 0; i < cards.Length; i++)
        {
            cardsType[i] = cards[i] % 13;
        }
    }
}
