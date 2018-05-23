using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

namespace com.dug.Server.Util
{
    public class PokerHandUtil
    {
        delegate HandResult handDelegate(int[] cards);
        private handDelegate[] hands = new handDelegate[10];

        public PokerHandUtil()
        {

            hands[(int)HandResult.HandType.ROYAL_STRAIGHT_FLUSH] = CheckRoyalStraightFlush;
            hands[(int)HandResult.HandType.STRAIGHT_FLUSH] = CheckStraightFlush;
            hands[(int)HandResult.HandType.POKER] = CheckPoker;
            hands[(int)HandResult.HandType.FULL_HOUSE] = CheckFullHouse;
            hands[(int)HandResult.HandType.FLUSH] = CheckFlush;
            hands[(int)HandResult.HandType.STRAIT] = CheckStraight;
            hands[(int)HandResult.HandType.TRIPLE] = CheckTriple;
            hands[(int)HandResult.HandType.TWO_PAIR] = CheckTwoPairs;
            hands[(int)HandResult.HandType.ONE_PAIR] = CheckOnePair;
            hands[(int)HandResult.HandType.TITLE] = CheckTitle;
        }
        
        public HandResult CheckRoyalStraightFlush(int[] cards)
        {
            HandResult result = new HandResult();
            int[,] hands = {
                  {0,9,10,11,12}
                , {13,22,23,24,25}
                , {26,35,36,37,38}
                , {39,48,49,50,51}
            };

            int handsCount = 4;
            int cardsCount = 5;
            int matchCount = 0;

            int compare1 = -1;
            int compare2 = -1;

            for (int i = 0; i < handsCount; i++)
            {
                for(int k = 0; k < cardsCount; k++)
                {
                    for (int j = 0; j < cards.Length; j++)
                    {
                        compare1 = hands[i, k];
                        compare2 = cards[j];
                        
                        if (compare1 == compare2)
                        {
                            matchCount++;
                        }
                    }

                }
                if (matchCount == cardsCount)
                {
                    result.handType = HandResult.HandType.ROYAL_STRAIGHT_FLUSH;
                    result.cardType = (HandResult.CardType)i + 1;

                    break;
                }
                matchCount = 0;
            }

            return result;
        }

        public HandResult CheckStraightFlush(int[] cards)
        {
            HandResult result = new HandResult();
            string cardStr = GetCardStr(cards);

            int[,] hands = {
                 {0,1,2,3,4}
                ,{1,2,3,4,5}
                ,{2,3,4,5,6}
                ,{3,4,5,6,7}
                ,{4,5,6,7,8}
                ,{5,6,7,8,9}
                ,{6,7,8,9,10}
                ,{7,8,9,10,11}
                ,{8,9,10,11,12}

                ,{13,14,15,16,17}
                ,{14,15,16,17,18}
                ,{15,16,17,18,19}
                ,{16,17,18,19,20}
                ,{17,18,19,20,21}
                ,{18,19,20,21,22}
                ,{19,20,21,22,23}
                ,{20,21,22,23,24}
                ,{21,22,23,24,25}

                ,{26,27,28,29,30}
                ,{27,28,29,30,31}
                ,{28,29,30,31,32}
                ,{29,30,31,32,33}
                ,{30,31,32,33,34}
                ,{31,32,33,34,35}
                ,{32,33,34,35,36}
                ,{33,34,35,36,37}
                ,{34,35,36,37,38}

                ,{39,40,41,42,43}
                ,{40,41,42,43,44}
                ,{41,42,43,44,45}
                ,{42,43,44,45,46}
                ,{43,44,45,46,47}
                ,{44,45,46,47,48}
                ,{45,46,47,48,49}
                ,{46,47,48,49,50}
                ,{47,48,49,50,51}
            };

            int handsCount = 36;
            int cardsCount = 5;
            int matchCount = 0;

            int compare1 = -1;
            int compare2 = -1;


            for (int i = 0; i < handsCount; i++)
            {
                for (int k = 0; k < cardsCount; k++)
                {
                    for (int j = 0; j < cards.Length; j++)
                    {
                        compare1 = hands[i, k];
                        compare2 = cards[j];

                        if (compare1 == compare2)
                        {
                            matchCount++;
                        }
                    }                   
                }

                if (matchCount == cardsCount)
                {
                    result.handType = HandResult.HandType.STRAIGHT_FLUSH;
                    result.cardType = (HandResult.CardType)((i - (i % 9)) / 9 + 1);
                    result.hands[0] = 4 + (i % 9);

                    break;
                }

                matchCount = 0;
            }

            return result;
        }

        public HandResult CheckPoker(int[] cards)
        {
            HandResult result = new HandResult();
            int matchCount = 0;
            int matchIndex = 0;
            int i = 0;

            for (i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for (int j = 0; j < cards.Length; j++)
                {
                    if (cards[i] % 13 == cards[j] % 13)
                    {
                        if (matchCount == 3)
                            matchIndex = cards[i] % 13;

                        matchCount++;
                    }
                }

                if (matchCount == 4)
                {
                    result.handType = HandResult.HandType.POKER;
                    if (matchIndex == 0) matchIndex = 13;
                    else result.hands[0] = matchIndex;
                }
            }

            int kickCount = 0;
            int card = -1;

            for (i = 0; i < cards.Length; i++)
            {
                card = cards[i] % 13;
                if (card == 0) card = 13;

                if (card != result.hands[0])
                {
                    result.kicks[kickCount++] = card;
                }
            }

            Array.Sort<int>(result.kicks);
            return result;
        }

        public HandResult CheckFullHouse(int[] cards)
        {
            HandResult result = new HandResult();
            int i = 0;
            int j = 0;
            bool hasTriple = false;
            int matchCount = 0;
            int matchIndex1 = 0;
            int matchIndex2 = 0;
            bool isFullHouse = false;

            for (i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for (j = 0; j < cards.Length; j++)
                {
                    if (cards[i] % 13 == cards[j] % 13)
                    {
                        if (matchCount == 2)
                            matchIndex1 = cards[i] % 13;

                        matchCount++;
                    }
                }

                if (matchCount >= 3)
                {
                    hasTriple = true;
                }
            }

            if (hasTriple == true)
            {
                for (i = 0; i < cards.Length; i++)
                {
                    matchCount = 0;

                    for (j = 0; j < cards.Length; j++)
                    {
                        if (cards[i] % 13 == cards[j] % 13 && cards[i] % 13 != matchIndex1)
                        {
                            if (matchCount == 1)
                                matchIndex2 = cards[i] % 13;

                            matchCount++;
                        }
                    }

                    if (matchCount >= 2)
                    {
                        isFullHouse = true;
                        result.handType = HandResult.HandType.FULL_HOUSE;
                    }
                }
            }


            if (isFullHouse == true)
            {
                int hand1 = -100;
                int hand2 = -100;

                for (i = 0; i < cards.Length; i++)
                {
                    matchCount = 0;
                    matchIndex1 = -1;

                    for (j = 0; j < cards.Length; j++)
                    {
                        if (cards[i] % 13 == cards[j] % 13)
                        {
                            if (matchCount == 1)
                            {
                                matchIndex1 = cards[i] % 13;
                                if (matchIndex1 == 0) matchIndex1 = 13;
                            }
                            matchCount++;
                        }

                        if (matchIndex1 == -1) continue;

                        if (matchCount == 2)
                        {
                            // 페어일떄는 가장큰 숫자로 대체 
                            if (matchIndex1 > hand2)
                                hand2 = matchIndex1;
                        }
                        else if (matchCount == 3)
                        {
                            // 트리플일때는 트리플 값을 세팅
                            if (matchIndex1 > hand1)
                            {
                                hand1 = matchIndex1;
                            }
                            else
                            {
                                if (matchIndex1 > hand2)
                                    hand2 = matchIndex1;
                            }
                        }
                    }

                }

                result.hands[0] = hand1;
                result.hands[1] = hand2;
            }

            return result;
        }

        public HandResult CheckFlush(int[] cards)
        {
            HandResult result = new HandResult();

            int i = 0;
            int j = 0;
            int count = 0;
            int matchCount = 0;
            HandResult.CardType matchType = HandResult.CardType.NONE;

            for (i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for (j = 0; j < cards.Length; j++)
                {
                    if (GetCardType(cards[i]) == GetCardType(cards[j]))
                    {
                        if (matchCount == 4)
                            matchType = GetCardType(cards[i]);

                        matchCount++;
                    }
                }

                if (matchCount >= 5)
                {
                    result.handType = HandResult.HandType.FLUSH;
                    result.cardType = matchType;
                    count = 0;

                    for (j = 0; j < cards.Length; j++)
                    {
                        if (GetCardType(cards[j]) == matchType)
                        {
                            result.hands[count] = cards[j] % 13;
                            if (result.hands[count] == 0) result.hands[count] = 13;

                            count++;
                        }
                    }

                    Array.Sort<int>(result.hands);
                    Array.Reverse(result.hands);
                }
            }

            return result;
        }

        public HandResult CheckStraight(int[] cards)
        {
            HandResult result = new HandResult();
            string cardStr = GetCardStr(cards);
            int i = 0;
            int j = 0;
            string[] hands = {
                 "0,1,2,3,4"
                ,"1,2,3,4,5"
                ,"2,3,4,5,6"
                ,"3,4,5,6,7"
                ,"4,5,6,7,8"
                ,"5,6,7,8,9"
                ,"6,7,8,9,10"
                ,"7,8,9,10,11"
                ,"8,9,10,11,12"
                ,"0,9,10,11,12"
            };

            for (i = 0; i < hands.Length; i++)
            {
                if (cardStr.IndexOf(hands[i]) != -1)
                {
                    result.handType = HandResult.HandType.STRAIT;
                    if (i == 9)
                    {
                        result.hands[0] = 13;
                    }
                    else
                    {
                        result.hands[0] = 13 - (9 - i);
                    }
                }
            }

            return result;
        }

        public HandResult CheckTriple(int[] cards)
        {
            HandResult result = new HandResult();
            int i = 0;
            int j = 0;
            int matchCount = 0;
            int matchIndex = -1;
            int kickCount = 0;

            for (i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for (j = 0; j < cards.Length; j++)
                {
                    if (cards[i] % 13 == cards[j] % 13)
                    {
                        if (matchCount == 2)
                        {
                            matchIndex = cards[i] % 13;
                            if (matchIndex == 0) matchIndex = 13;
                        }
                        matchCount++;
                    }
                }

                if (matchCount == 3)
                {
                    result.handType = HandResult.HandType.TRIPLE;
                }
            }

            for (i = 0; i < cards.Length; i++)
            {
                int card = cards[i] % 13;
                if (card == 0) card = 13;

                if (card != matchCount)
                {
                    result.kicks[kickCount++] = card;
                }
            }

            Array.Sort(result.kicks);

            return result;
        }

        public HandResult CheckTwoPairs(int[] cards)
        {
            HandResult result = new HandResult();
            int i = 0;
            int j = 0;
            int k = 0;
            int kickCount = 0;
            bool exist = false;
            int matchCount = 0;
            int matchIndex = -1;
            int endCount = 0;

            for (i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for (j = 0; j < cards.Length; j++)
                {
                    if (cards[i] % 13 == cards[j] % 13)
                    {
                        if (matchCount == 1)
                        {

                            matchIndex = cards[i] % 13;
                            if (matchIndex == 0) matchIndex = 13;

                            exist = false;

                            for (k = 0; k < endCount; k++)
                            {
                                if (result.hands[k] == matchIndex)
                                    exist = true;
                            }

                            if (exist == false)
                            {
                                result.hands[endCount] = matchIndex;
                                matchCount++;
                            }
                        }
                        else
                        {
                            matchCount++;
                        }
                    }

                }

                if (matchCount == 2)
                {
                    matchCount = 0;
                    endCount++;
                }
            }

            if (endCount >= 2)
            {
                result.handType = HandResult.HandType.TWO_PAIR;

                Array.Sort<int>(result.hands);
                Array.Reverse(result.hands);
            }

            for (i = 0; i < cards.Length; i++)
            {
                int card = cards[i] % 13;
                if (card == 0) card = 13;

                if (card != result.hands[0] && card != result.hands[1])
                {
                    result.kicks[kickCount++] = card;
                }
            }

            return result;
        }

        public HandResult CheckOnePair(int[] cards)
        {
            HandResult result = new HandResult();
            int i = 0;
            int j = 0;
            int k = 0;
            int kickCount = 0;
            int matchCount = 0;

            for (i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for (j = 0; j < cards.Length; j++)
                {
                    if (cards[i] % 13 == cards[j] % 13)
                    {
                        if (matchCount == 1)
                        {
                            result.hands[0] = cards[i] % 13;
                            if (result.hands[0] == 0) result.hands[0] = 13;

                            for (k = 0; k < cards.Length; k++)
                            {
                                int index = GetCardOrder(cards, k);
                                int temp = index % 13;
                                if (temp == 0) temp = 13;
                                if (temp == result.hands[0])
                                {
                                    result.cardType = GetCardType(index);
                                    break;
                                }
                            }
                        }
                        matchCount++;
                    }
                }

                if (matchCount == 2)
                {
                    result.handType = HandResult.HandType.ONE_PAIR;
                }
            }

            for (i = 0; i < cards.Length; i++)
            {
                int card = cards[i] % 13;
                if (card == 0) card = 13;

                if (card != result.hands[0])
                {
                    result.kicks[kickCount++] = card;
                }
            }
            Array.Sort(result.kicks);
            return result;
        }

        public HandResult CheckTitle(int[] cards)
        {
            HandResult result = new HandResult();
            result.handType = HandResult.HandType.TITLE;

            for (int i = 0; i < cards.Length; i++)
            {
                result.hands[i] = GetCardOrder(cards, i) % 13;
                if (result.hands[i] == 0) result.hands[i] = 13;
            }

            return result;
        }


        public static string GetCardStr(int[] cards)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < cards.Length; i++)
            {
                if (i > 0)
                    builder.Append(",");

                builder.Append(cards[i]);
            }
            return builder.ToString();
        }

        public static HandResult.CardType GetCardType(int cardIndex)
        {
            HandResult.CardType type = HandResult.CardType.NONE;

            if (cardIndex >= 0 && cardIndex <= 12) type = HandResult.CardType.SPADE;  //스페이드
            if (cardIndex >= 13 && cardIndex <= 25) type = HandResult.CardType.DIAMOND; //다이아몬드
            if (cardIndex >= 26 && cardIndex <= 38) type = HandResult.CardType.HEART; //하트
            if (cardIndex >= 39 && cardIndex <= 51) type = HandResult.CardType.CLOVER;	//클로버

            return type;
        }

        public static int GetCardOrder(int[] cards, int orderNo)
        {
            int i = 0;
            int j = 0;
            int value = 0;
            int[] order = new int[cards.Length];

            for (i = 0; i < cards.Length; i++)
            {
                if (cards[i] % 13 == 0) value = 13;
                else value = cards[i] % 13;

                value = value * 4 - ((int)GetCardType(cards[i]) - 1);

                order[i] = value;
            }

            Array.Sort(order);

            int ret = order[orderNo];
            int cardType = ret % 4;
            if (cardType == 0) cardType = 1;
            else if (cardType == 1) cardType = 2;
            else if (cardType == 2) cardType = 3;
            else if (cardType == 3) cardType = 4;

            ret = (ret + cardType - 1) / 4 + 13 * (cardType - 1);

            return ret;
        }

        public HandResult CheckHands(int[] cards)
        {
            HandResult result = null;

            Array.Sort(cards);

            for (int i = 0; i < hands.Length; i++)
            {
                result = hands[i](cards);

                if (result.handType != HandResult.HandType.NONE)
                {
                    break;
                }
            }

            return result;
        }

    }
}
