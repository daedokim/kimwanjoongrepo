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
        //PokerHandUtil handUtil = new PokerHandUtil();

        //hands[(int)HandResult.HandType.ROYAL_STRAIGHT_FLUSH] = handUtil.CheckRoyalStraightFlush;
        //hands[(int)HandResult.HandType.STRAIGHT_FLUSH] = handUtil.CheckStraightFlush;
        //hands[(int)HandResult.HandType.POKER] = handUtil.CheckPoker;
        //hands[(int)HandResult.HandType.FULL_HOUSE] = handUtil.CheckFullHouse;
        //hands[(int)HandResult.HandType.FLUSH] = handUtil.CheckFlush;        
        //hands[(int)HandResult.HandType.STRAIT] = handUtil.CheckStraight;
        //hands[(int)HandResult.HandType.TRIPLE] = handUtil.CheckTriple;
        //hands[(int)HandResult.HandType.TWO_PAIR] = handUtil.CheckTwoPairs;
        //hands[(int)HandResult.HandType.ONE_PAIR] = handUtil.CheckOnePair;
        //hands[(int)HandResult.HandType.TITLE] = handUtil.CheckTitle;


        //this.ObserveEveryValueChanged(x => x.flag).Skip(1).Subscribe(x => {

        //    CheckHands();
        //});


        //int count = 0;

        //while(count < 1)
        //{
        //    CheckHands();

        //    if(result != null && result.handType == HandResult.HandType.ONE_PAIR)
        //    {
        //        break;
        //    }
        //    count++;
        //}

        string str = "{\"packetNum\":2,\"data\":{\"GamePlayers\":[{\"state\":1,\"chairIndex\":3,\"roomIndex\":1,\"round\":0,\"card1\":44,\"card2\":24,\"buyInLeft\":94000,\"orderNo\":0,\"stage\":3,\"betStatus\":1,\"betType\":4,\"lastBetType\":4,\"betCount\":6,\"lastBet\":1000,\"lastCall\":1000,\"lastRaise\":0,\"totalBet\":6000,\"stageBet\":0,\"lastActionDate\":\"2018-07-25T14:56:56.5508143+09:00\",\"noActionCount\":0,\"coin\":1000000,\"NickName\":\"NoName\",\"userIndex\":1,\"result\":{\"CardType\":4,\"HandType\":1,\"Hands\":[6,-1,-1,-1,-1,-1,-1],\"Kicks\":[-1,-1,1,3,9,10,11],\"MadeCards\":[19,6,-1,-1,-1,-1,-1]}},{\"state\":1,\"chairIndex\":4,\"roomIndex\":1,\"round\":0,\"card1\":4,\"card2\":15,\"buyInLeft\":96000,\"orderNo\":1,\"stage\":12,\"betStatus\":0,\"betType\":0,\"lastBetType\":0,\"betCount\":4,\"lastBet\":1000,\"lastCall\":0,\"lastRaise\":1000,\"totalBet\":4000,\"stageBet\":0,\"lastActionDate\":\"2018-07-25T14:56:55.1257016+09:00\",\"noActionCount\":0,\"coin\":1000000,\"NickName\":\"NoName\",\"userIndex\":2,\"result\":{\"CardType\":-1,\"HandType\":0,\"Hands\":[1,3,3,6,10,11,12],\"Kicks\":[-1,-1,-1,-1,-1,-1,-1],\"MadeCards\":[1,19,37,29,10,39,43]}}],\"Room\":{\"roomIndex\":1,\"state\":0,\"round\":0,\"card1\":9,\"card2\":37,\"card3\":46,\"card4\":4,\"card5\":7,\"lastBet\":0,\"lastRaise\":0,\"winnerUserIndex\":2,\"currentUserIndex\":0,\"dealerChairIndex\":4,\"ownerIndex\":3,\"totalBet\":0,\"stageBet\":0,\"lastBetType\":2,\"buyInMin\":1000,\"buyInMax\":100000,\"stage\":1,\"betFinished\":0,\"betCount\":2,\"currentOrderNo\":-1,\"minbetAmount\":1000,\"waitTimeout\":1}},\"error\":\"\"}";
        //com.dug.UI.DTO.Room room = com.dug.common.JsonConverter.FromJson<com.dug.UI.DTO.Room>(str);

        PacketData data = com.dug.common.JsonConverter.FromJson<PacketData>(str);

        object[] temp = (object[])data.data["GamePlayers"];
   
        List<com.dug.UI.DTO.GamePlayer> gamePlayers = com.dug.common.JsonConverter.GetObjectList<com.dug.UI.DTO.GamePlayer>((Dictionary<string, object>[])temp);

        //Debug.Log(temp);

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
