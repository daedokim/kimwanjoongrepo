using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.Server.vo;
using System;

namespace com.dug.Server.Util
{
    public class WinnerValidator : Singleton<WinnerValidator>
    {
        private PokerHandUtil handUtil;
        

        public WinnerValidator()
        {
            handUtil = new PokerHandUtil();
        }

        public long GetWinner(List<GamePlayer> playerList, int[] tableCards)
        {
            long userIndex = 0;

            List<GamePlayer> resultList = null;
            int maxHand = -1;
            int currentHand = -1;

            for (int i = 0; i < playerList.Count; i++)
            {
                int[] cards = new int[7];

                cards[0] = playerList[i].card1;
                cards[1] = playerList[i].card2;
                cards[2] = tableCards[0];
                cards[3] = tableCards[1];
                cards[4] = tableCards[2];
                cards[5] = tableCards[3];
                cards[6] = tableCards[4];

                playerList[i].result = handUtil.CheckHands(cards);

                print(playerList[i].result.handType);

                currentHand = (int)playerList[i].result.handType;

                if (maxHand < currentHand)
                {
                    maxHand = currentHand;
                    resultList = new List<GamePlayer>();
                    resultList.Add(playerList[i]);
                }
                else if (maxHand == currentHand)
                {
                    resultList.Add(playerList[i]);
                }
            }

            if (resultList != null)
            {
                if (resultList.Count == 1)
                {
                    userIndex = resultList[0].useridx;
                }
                else if (resultList.Count > 1)
                {
                    userIndex = GetTiedWinner(resultList);

                    if (userIndex == 0)
                        userIndex = GetKickWinner(resultList);
                }
            }
            
            return userIndex;
        }

        private long GetTiedWinner(List<GamePlayer> resultList)
        {
            long userIndex = 0;
            int maxValue = 0;

            for (int i = 0; i < resultList.Count; i++)
            {
                for (int j = 0; j < resultList[i].result.hands.Length; j++)
                {
                    if (resultList[i].result.hands[j] > maxValue)
                    {
                        maxValue = resultList[i].result.hands[j];
                        userIndex = resultList[i].useridx;
                    }
                }
            }
           
            return userIndex;
        }

        private long GetKickWinner(List<GamePlayer> resultList)
        {
            long userIndex = 0;
            int maxValue = 0;

            for (int i = 0; i < resultList.Count; i++)
            {
                for (int j = 0; j < resultList[i].result.kicks.Length; j++)
                {
                    if (resultList[i].result.kicks[j] > maxValue)
                    {
                        maxValue = resultList[i].result.kicks[j];
                        userIndex = resultList[i].useridx;
                    }
                }
            }
            return userIndex;
        }


    }

}
