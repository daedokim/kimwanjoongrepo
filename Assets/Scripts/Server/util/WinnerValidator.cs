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

        public HandResult GetResult(int[] cards)
        {
            return handUtil.CheckHands(cards);
        }

        public long GetWinner(List<GamePlayer> playerList)
        {
            long userIndex = 0;

            List<GamePlayer> winnerList = null;
            int maxHand = -1;
            int currentHand = -1;

            for (int i = 0; i < playerList.Count; i++)
            {
                currentHand = (int)playerList[i].result.handType;

                if (maxHand < currentHand)
                {
                    maxHand = currentHand;
                    winnerList = new List<GamePlayer>();
                    winnerList.Add(playerList[i]);
                }
                else if (maxHand == currentHand)
                {
                    winnerList.Add(playerList[i]);
                }
            }

            if (winnerList != null)
            {
                if (winnerList.Count == 1)
                {
                    userIndex = winnerList[0].useridx;
                }
                else if (winnerList.Count > 1)
                {
                    userIndex = GetTiedWinner(winnerList);

                    if (userIndex == 0)
                        userIndex = GetKickWinner(winnerList);
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
