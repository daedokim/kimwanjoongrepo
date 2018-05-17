using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

namespace com.dug.Server.Util
{
    public class PokerHandUtil
    {
        public HandResult CheckRoyalStraightFlush(int[] cards)
        {
            HandResult result = new HandResult();
            string cardStr = GetCardStr(cards);
            string[] hands = {
                  "0,9,10,11,12"
                , "13,22,23,24,25"
                , "26,35,36,37,38"
                , "39,48,49,50,51"
            };
            
            for (int i = 0; i < hands.Length; i++)
            {
                if (hands[i].IndexOf(cardStr) != -1)
                {
                    result.handType = HandResult.HandType.ROYAL_STRAIGHT_FLUSH;
                    result.cardType = i;
                    break;
                }   
            }
            
            return result;
        }

        public HandResult CheckStraightFlush(int[] cards)
        {
            HandResult result = new HandResult();
            string cardStr = GetCardStr(cards);

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

                ,"13,14,15,16,17"
                ,"14,15,16,17,18"
                ,"15,16,17,18,19"
                ,"16,17,18,19,20"
                ,"17,18,19,20,21"
                ,"18,19,20,21,22"
                ,"19,20,21,22,23"
                ,"20,21,22,23,24"
                ,"21,22,23,24,25"
                 
                ,"26,27,28,29,30"
                ,"27,28,29,30,31"
                ,"28,29,30,31,32"
                ,"29,30,31,32,33"
                ,"30,31,32,33,34"
                ,"31,32,33,34,35"
                ,"32,33,34,35,36"
                ,"33,34,35,36,37"
                ,"34,35,36,37,38"
                 
                ,"39,40,41,42,43"
                ,"40,41,42,43,44"
                ,"41,42,43,44,45"
                ,"42,43,44,45,46"
                ,"43,44,45,46,47"
                ,"44,45,46,47,48"
                ,"45,46,47,48,49"
                ,"46,47,48,49,50"
                ,"47,48,49,50,51"
            };

            for (int i = 0; i < hands.Length; i++)
            {
                if (hands[i].IndexOf(cardStr) != -1)
                {
                    result.handType = HandResult.HandType.STRAIGHT_FLUSH;
                    result.cardType = (i - (i % 9)) / 9 + 1;
                    result.hands[0] = 4 + (i % 9);
                    break;
                }

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

                for(int j = 0; j < cards.Length; j++)
                {
                    if(cards[i]%13 == cards[j]%13)
                    {
                        if (matchCount == 3)
                            matchIndex = cards[i] % 13;

                        matchCount++;
                    }
                }

                if(matchCount == 4)
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

                if(card != result.hands[0])
                {
                    result.kicks[kickCount++] = card;
                }
            }
            
            Array.Sort<int>(result.kicks);
            return result;
        }

        public HandResult CheckFullHouse(int[] cards)
        {
            int i = 0;
            int j = 0;
            bool hasTriple = false;
            int matchCount = 0;
            int matchIndex1 = 0;
            int matchIndex2 = 0;
            bool isFullHouse = false;

            for(i = 0; i < cards.Length; i++)
            {
                matchCount = 0;

                for(j = 0; j < cards.Length; j++)
                {
                    if (cards[i] % 13 == cards[j] % 13)
                    {
                        if (matchCount == 2)
                            matchIndex1 = cards[i] % 13;

                        matchCount++;
                    }
                }

                if(matchCount > 3)
                {
                    hasTriple = true;
                }
            }

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

                if (matchCount >=2)
                {
                    isFullHouse = true;
                }
            }


            return null;
        }

        public HandResult CheckFlush(int[] cards)
        {
            return null;
        }

        public HandResult CheckStraight(int[] cards)
        {
            return null;
        }

        public HandResult CheckTriple(int[] cards)
        {
            return null;
        }

        public HandResult CheckTwoPairs(int[] cards)
        {
            return null;
        }

        public HandResult CheckOnePair(int[] cards)
        {
            return null;
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


    }
}


//#include "header.h"
//#include "SevenPoker.h"

//int SevenPoker::getcardtype(int nCardIndex)
//{
//    if (nCardIndex >= 0 && nCardIndex <= 12) return 1;  //스페이드
//    if (nCardIndex >= 13 && nCardIndex <= 25) return 2; //다이아몬드
//    if (nCardIndex >= 26 && nCardIndex <= 38) return 3; //하트
//    if (nCardIndex >= 39 && nCardIndex <= 51) return 4; //클로버

//    return 0;
//}

//int SevenPoker::SortAllCards()
//{
//    // TODO: Add your implementation code here
//    if (m_bSorted) return 1;

//    int i = 0;
//    int j = 0;
//    int k = 0;

//    for (i = 0; i < 21; i++)
//    {
//        for (j = 0; j < 7; j++)
//        {
//            for (k = j + 1; k < 7; k++)
//            {
//                if (m_Cards[i][j] > m_Cards[i][k])
//                {
//                    int temp = m_Cards[i][k];
//                    m_Cards[i][k] = m_Cards[i][j];
//                    m_Cards[i][j] = temp;
//                }
//            }
//        }
//    }

//    // -1은 다시 뒤로 보냄(완전 개삽질!!)
//    for (i = 0; i < 21; i++)
//    {
//        int minuscount = 0;
//        for (j = 0; j < 7; j++)
//        {
//            if (m_Cards[i][j] == -1) minuscount++;
//        }
//        if (minuscount % 7 == 0) continue;

//        for (j = 0; j < 7 - minuscount; j++)
//        {
//            m_Cards[i][j] = m_Cards[i][minuscount + j];
//        }
//        for (j = 7 - minuscount; j < 7; j++)
//        {
//            m_Cards[i][j] = -1;
//        }
//    }

//    m_bSorted = true;

//    return 1;
//}

//int SevenPoker::InsertCard(int nPlayer, int nIndex, int nCardIndex)
//{
//    // TODO: Add your implementation code here
//    m_Cards[nPlayer][nIndex] = nCardIndex;

//    return 1;
//}

//int SevenPoker::GetCardIndex(int nPlayer, int nCard)
//{
//    int ret = -1;

//    for (int i = 0; i < 7; i++)
//    {
//        if (m_Cards[nPlayer][i] == nCard)
//        {
//            ret = i;
//            break;
//        }
//    }

//    return ret;
//}

//int SevenPoker::CheckRoyalStraightFlush(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here

//    char log[100];
//    sprintf(log, "nPlayer = %d", nPlayer);
//    //CMiscUtil::WriteLog(log);

//    int i = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    *nFlag = 0;
//    char possiblecases[4][50];
//	for (i=0; i<4; i++)
//	{

//        memset(possiblecases[i], 0, 50);
//	}

//    strcpy(possiblecases[0], ",9,,10,,11,,12,");

//    strcpy(possiblecases[1], ",22,,23,,24,,25,");

//    strcpy(possiblecases[2], ",35,,36,,37,,38,");

//    strcpy(possiblecases[3], ",48,,49,,50,,51,");

////CMiscUtil::WriteLog("1");


//char cardlist[50];

//    memset(cardlist, 0, 50);

//	//CMiscUtil::WriteLog("1.1");

//	for (i=0; i<7; i++)
//	{

//        sprintf(log, "i=%d", i);
////CMiscUtil::WriteLog(log);

//char temp[10];

//        memset(temp, 0, 10);

//        //CMiscUtil::WriteLog("1.2");
//        sprintf(temp, ",%d,", m_Cards[nPlayer][i]);
//        //CMiscUtil::WriteLog("1.3");
//        //CMiscUtil::WriteLog(cardlist);
//        //CMiscUtil::WriteLog(temp);
//        strcat(cardlist, temp);
//		//CMiscUtil::WriteLog("1.4");
//	}

//	//CMiscUtil::WriteLog("2");

//	for (i=0; i<4; i++)
//	{
//		char ace[10];

//        memset(ace, 0, 10);

//        sprintf(ace, ",%d,", i*13);
//		if (strstr(cardlist, possiblecases[i]) != NULL && strstr(cardlist, ace) != NULL) 
//		{
//			m_ResultDetail[nPlayer].type = i+1;

//            * nFlag = 1;
//		}
//	}

//	//CMiscUtil::WriteLog("3");

//	return 1;
//}

//int SevenPoker::GetCardAt(int nPlayer, int nIndex)
//{
//    return m_Cards[nPlayer][nIndex];
//}

//int SevenPoker::GetCardType(int nCardIndex, int* nType)
//{
//    // TODO: Add your implementation code here
//    *nType = 0;

//    if (nCardIndex >= 0 && nCardIndex <= 12) *nType = 1;    //스페이드
//    if (nCardIndex >= 13 && nCardIndex <= 25) *nType = 2;   //다이아몬드
//    if (nCardIndex >= 26 && nCardIndex <= 38) *nType = 3;   //하트
//    if (nCardIndex >= 39 && nCardIndex <= 51) *nType = 4;   //클로버

//    return 1;
//}

//int SevenPoker::GetCardOrder(int nCardIndex, int* nOrder)
//{
//    // TODO: Add your implementation code here
//    if (nCardIndex % 13 == 0)
//    {
//        *nOrder = 13;
//    }
//    else
//    {
//        *nOrder = nCardIndex % 13;
//    }

//    return 1;
//}

//int SevenPoker::Initialize()
//{
//    // TODO: Add your implementation code here
//    int i = 0;
//    int j = 0;

//    m_bSingleMode = false;
//    m_bSorted = false;

//    for (i = 0; i < 52; i++)
//    {
//        m_ShuffledCards[i] = -1;
//    }

//    for (i = 0; i < 21; i++)
//    {
//        for (j = 0; j < 7; j++)
//        {
//            m_Cards[i][j] = -1;
//        }
//        for (j = 0; j < 7; j++)
//        {
//            m_ResultDetail[i].ends[j] = -1;
//            m_ResultDetail[i].endstype[j] = -1;
//        }
//        for (j = 0; j < 7; j++)
//        {
//            m_ResultDetail[i].kick[j] = -1;
//        }
//        m_ResultDetail[i].type = -1;
//        m_ResultDetail[i].win_candidate = false;
//    }

//    for (i = 0; i < 52; i++)
//    {
//        m_CardInfo[i].index = i;
//        if (i >= 0 && i <= 12)
//        {
//            m_CardInfo[i].type = 1;
//            if (i == 0) m_CardInfo[i].order = 52;
//            else m_CardInfo[i].order = 39 + i;
//        }
//        else if (i >= 13 && i <= 25)
//        {
//            m_CardInfo[i].type = 2;
//            if (i == 0) m_CardInfo[i].order = 39;
//            else m_CardInfo[i].order = 26 + i % 13;
//        }
//        else if (i >= 26 && i <= 38)
//        {
//            m_CardInfo[i].type = 3;
//            if (i == 0) m_CardInfo[i].order = 26;
//            else m_CardInfo[i].order = 13 + i % 13;
//        }
//        else if (i >= 39 && i <= 51)
//        {
//            m_CardInfo[i].type = 4;
//            if (i == 0) m_CardInfo[i].order = 13;
//            else m_CardInfo[i].order = i % 13;
//        }
//    }

//    return 1;
//}

//int SevenPoker::CheckStraightFlush(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    int i = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }


//    *nFlag = 0;
//    char possiblecases[36][50];
//	for (i=0; i<36; i++)
//	{

//        memset(possiblecases[i], 0, 50);
//	}

//    strcpy(possiblecases[0], ",0,,1,,2,,3,,4,");

//    strcpy(possiblecases[1], ",1,,2,,3,,4,,5,");

//    strcpy(possiblecases[2], ",2,,3,,4,,5,,6,");

//    strcpy(possiblecases[3], ",3,,4,,5,,6,,7,");

//    strcpy(possiblecases[4], ",4,,5,,6,,7,,8,");

//    strcpy(possiblecases[5], ",5,,6,,7,,8,,9,");

//    strcpy(possiblecases[6], ",6,,7,,8,,9,,10,");

//    strcpy(possiblecases[7], ",7,,8,,9,,10,,11,");

//    strcpy(possiblecases[8], ",8,,9,,10,,11,,12,");


//    strcpy(possiblecases[9], ",13,,14,,15,,16,,17,");

//    strcpy(possiblecases[10], ",14,,15,,16,,17,,18,");

//    strcpy(possiblecases[11], ",15,,16,,17,,18,,19,");

//    strcpy(possiblecases[12], ",16,,17,,18,,19,,20,");

//    strcpy(possiblecases[13], ",17,,18,,19,,20,,21,");

//    strcpy(possiblecases[14], ",18,,19,,20,,21,,22,");

//    strcpy(possiblecases[15], ",19,,20,,21,,22,,23,");

//    strcpy(possiblecases[16], ",20,,21,,22,,23,,24,");

//    strcpy(possiblecases[17], ",21,,22,,23,,24,,25,");


//    strcpy(possiblecases[18], ",26,,27,,28,,29,,30,");

//    strcpy(possiblecases[19], ",27,,28,,29,,30,,31,");

//    strcpy(possiblecases[20], ",28,,29,,30,,31,,32,");

//    strcpy(possiblecases[21], ",29,,30,,31,,32,,33,");

//    strcpy(possiblecases[22], ",30,,31,,32,,33,,34,");

//    strcpy(possiblecases[23], ",31,,32,,33,,34,,35,");

//    strcpy(possiblecases[24], ",32,,33,,34,,35,,36,");

//    strcpy(possiblecases[25], ",33,,34,,35,,36,,37,");

//    strcpy(possiblecases[26], ",34,,35,,36,,37,,38,");


//    strcpy(possiblecases[27], ",39,,40,,41,,42,,43,");

//    strcpy(possiblecases[28], ",40,,41,,42,,43,,44,");

//    strcpy(possiblecases[29], ",41,,42,,43,,44,,45,");

//    strcpy(possiblecases[30], ",42,,43,,44,,45,,46,");

//    strcpy(possiblecases[31], ",43,,44,,45,,46,,47,");

//    strcpy(possiblecases[32], ",44,,45,,46,,47,,48,");

//    strcpy(possiblecases[33], ",45,,46,,47,,48,,49,");

//    strcpy(possiblecases[34], ",46,,47,,48,,49,,50,");

//    strcpy(possiblecases[35], ",47,,48,,49,,50,,51,");


//char cardlist[50];

//    memset(cardlist, 0, 50);

//	for (i=0; i<7; i++)
//	{
//		char temp[10];

//        memset(temp, 0, 10);

//        sprintf(temp, ",%d,", m_Cards[nPlayer][i]);

//        strcat(cardlist, temp);
//	}

//	for (i=0; i<36; i++)
//	{
//		if (strstr(cardlist, possiblecases[i]) != NULL) 
//		{
//			m_ResultDetail[nPlayer].ends[0] = 4 + (i%9);
//			m_ResultDetail[nPlayer].type = (i-(i%9))/9 + 1;

//            * nFlag = 1;
//		}
//	}

//	return 1;
//}

//int SevenPoker::CheckPoker(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 0;

//    int i = 0;
//    int j = 0;
//    int matchcount = 0;
//    int matchindex = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        matchcount = 0;
//        if (m_Cards[nPlayer][i] == -1) continue;
//        for (j = 0; j < 7; j++)
//        {
//            if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13)
//            {
//                if (matchcount == 3) matchindex = m_Cards[nPlayer][i] % 13;
//                matchcount++;
//            }
//        }
//        if (matchcount == 4)
//        {
//            if (matchindex == 0) matchindex = 13;
//            else m_ResultDetail[nPlayer].ends[0] = matchindex;
//            *nFlag = 1;
//        }
//    }

//    // 킥 보관
//    int kickcount = 0;

//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].kick[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        int sel_card = m_Cards[nPlayer][i] % 13;
//        if (sel_card == 0) sel_card = 13;

//        if (sel_card != m_ResultDetail[nPlayer].ends[0])
//        {
//            m_ResultDetail[nPlayer].kick[kickcount] = sel_card;
//            kickcount++;
//        }
//    }

//    SortKick(m_ResultDetail[nPlayer].kick);

//    return 1;
//}

//int SevenPoker::CheckFullHouse(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 0;

//    int i = 0;
//    int j = 0;
//    bool bNext = false;
//    int matchcount = 0;
//    int matchindex = -1;
//    int matchindex2 = -1;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        matchcount = 0;
//        if (m_Cards[nPlayer][i] == -1) continue;
//        for (j = 0; j < 7; j++)
//        {
//            if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13)
//            {
//                if (matchcount == 2) matchindex = m_Cards[nPlayer][i] % 13;
//                matchcount++;
//            }
//        }
//        if (matchcount == 3)
//        {
//            bNext = true;
//        }
//    }

//    bool bFullHouse = false;

//    if (bNext)  //일단 트리플이 있는 경우 페어 이상이 있나 찾음
//    {
//        for (i = 0; i < 7; i++)
//        {
//            matchcount = 0;
//            if (m_Cards[nPlayer][i] == -1) continue;
//            for (j = 0; j < 7; j++)
//            {
//                if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13 && m_Cards[nPlayer][i] % 13 != matchindex)
//                {
//                    if (matchcount == 1) matchindex2 = m_Cards[nPlayer][i] % 13;
//                    matchcount++;
//                }
//            }
//            if (matchcount >= 2)
//            {
//                bFullHouse = true;
//                break;
//            }
//        }
//    }

//    if (bFullHouse)
//    {
//        *nFlag = 1;

//        int end0 = -100, end1 = -100;

//        for (i = 0; i < 7; i++)
//        {
//            matchcount = 0;
//            matchindex = -1;

//            if (m_Cards[nPlayer][i] == -1) continue;
//            for (j = 0; j < 7; j++)
//            {
//                if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13)
//                {
//                    if (matchcount == 1)
//                    {
//                        matchindex = m_Cards[nPlayer][i] % 13;
//                        if (matchindex == 0) matchindex = 13;   // ace가 짱
//                    }
//                    matchcount++;
//                }
//            }

//            if (matchindex == -1) continue;

//            if (matchcount == 2)
//            {
//                // 기존 페어랑 비교해서 크면 페어 대체한다
//                if (matchindex >= end1)
//                {
//                    end1 = matchindex;
//                }
//            }
//            else if (matchcount == 3)
//            {
//                // 트리플과 비교하고 크면 트리플 대체하고 작으면 페어랑 비교해서 크면 페어 대체한다
//                if (matchindex >= end0)
//                {
//                    end0 = matchindex;
//                }
//                else
//                {
//                    if (matchindex >= end1) end1 = matchindex;
//                }

//            }
//        }

//        m_ResultDetail[nPlayer].ends[0] = end0;
//        m_ResultDetail[nPlayer].ends[1] = end1;
//    }

//    // no kick
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].kick[i] = -1;
//    }

//    return 1;
//}

//int SevenPoker::CheckFlush(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 0;

//    int i = 0;
//    int j = 0;
//    int k = 0;
//    int matchcount = 0;
//    int matchtype = -1;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        matchcount = 0;
//        if (m_Cards[nPlayer][i] == -1) continue;
//        for (j = 0; j < 7; j++)
//        {
//            if (getcardtype(m_Cards[nPlayer][i]) != 0 && getcardtype(m_Cards[nPlayer][i]) == getcardtype(m_Cards[nPlayer][j]))
//            {
//                if (matchcount == 4) matchtype = getcardtype(m_Cards[nPlayer][i]);
//                matchcount++;
//            }
//        }
//        if (matchcount >= 5)
//        {
//            m_ResultDetail[nPlayer].type = matchtype;
//            int count = 0;
//            for (j = 0; j < 7; j++)
//            {
//                if (getcardtype(m_Cards[nPlayer][j]) == matchtype)
//                {
//                    m_ResultDetail[nPlayer].ends[count] = m_Cards[nPlayer][j] % 13;
//                    if (m_ResultDetail[nPlayer].ends[count] == 0) m_ResultDetail[nPlayer].ends[count] = 13;
//                    count++;
//                }
//            }

//            //내림차순으로 소트
//            for (j = 0; j < 7; j++)
//            {
//                for (k = j + 1; k < 7; k++)
//                {
//                    if (m_ResultDetail[nPlayer].ends[j] < m_ResultDetail[nPlayer].ends[k])
//                    {
//                        int temp = m_ResultDetail[nPlayer].ends[k];
//                        m_ResultDetail[nPlayer].ends[k] = m_ResultDetail[nPlayer].ends[j];
//                        m_ResultDetail[nPlayer].ends[j] = temp;
//                    }
//                }
//            }

//            *nFlag = 1;
//        }
//    }

//    // no kick
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].kick[i] = -1;
//    }

//    return 1;
//}

//int SevenPoker::getcardbyorder(int nPlayer, int nOrder)
//{
//    int i = 0, j = 0;
//    long order[7];

//    for (i = 0; i < 7; i++)
//    {
//        long value = -1;
//        if (m_Cards[nPlayer][i] % 13 == 0) value = 13;
//        else value = m_Cards[nPlayer][i] % 13;
//        value = value * 4 - (getcardtype(m_Cards[nPlayer][i]) - 1);
//        order[i] = value;
//    }

//    //소트
//    for (i = 0; i < 7; i++)
//    {
//        for (j = i + 1; j < 7; j++)
//        {
//            if (order[i] < order[j])
//            {
//                long temp = order[j];
//                order[j] = order[i];
//                order[i] = temp;
//            }
//        }
//    }

//    long ret = order[nOrder];
//    int cardtype = ret % 4;
//    if (cardtype == 0) cardtype = 1;
//    else if (cardtype == 3) cardtype = 2;
//    else if (cardtype == 2) cardtype = 3;
//    else if (cardtype == 1) cardtype = 4;

//    ret = (ret + cardtype - 1) / 4 + 13 * (cardtype - 1);

//    return (int)ret;

//}

//int SevenPoker::CheckStraight(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    int i = 0;
//    int j = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    *nFlag = 0;
//    char possiblecases[10][50];
//	for (i=0; i<10; i++)
//	{

//        memset(possiblecases[i], 0, 50);
//	}

//    strcpy(possiblecases[0], ",0,,1,,2,,3,,4,");

//    strcpy(possiblecases[1], ",1,,2,,3,,4,,5,");

//    strcpy(possiblecases[2], ",2,,3,,4,,5,,6,");

//    strcpy(possiblecases[3], ",3,,4,,5,,6,,7,");

//    strcpy(possiblecases[4], ",4,,5,,6,,7,,8,");

//    strcpy(possiblecases[5], ",5,,6,,7,,8,,9,");

//    strcpy(possiblecases[6], ",6,,7,,8,,9,,10,");

//    strcpy(possiblecases[7], ",7,,8,,9,,10,,11,");

//    strcpy(possiblecases[8], ",8,,9,,10,,11,,12,");

//    strcpy(possiblecases[9], ",9,,10,,11,,12,");

//char cardlist[50];

//    memset(cardlist, 0, 50);

//int saved_card[7];	// 임시 복사
//	for (i=0; i<7; i++)
//	{
//		saved_card[i] = m_Cards[nPlayer][i];
//	}

//	// 무늬와 상관없이 재정렬(오름차순)
//	for (i=0; i<7; i++)
//	{
//		for (j=i+1; j<7; j++)
//		{
//			if (saved_card[i]%13 > saved_card[j]%13)
//			{
//				int temp = saved_card[i];
//saved_card[i] = saved_card[j];
//				saved_card[j] = temp;
//			}
//		}
//	}

//	for (i=0; i<7; i++)
//	{
//		char temp[10];

//        memset(temp, 0, 10);

//        sprintf(temp, ",%d,", saved_card[i]%13);
//		if (strstr(cardlist, temp) == NULL) strcat(cardlist, temp);	// 중복제거
//	}

//	for (i=9; i>=0; i--)
//	{
//		if (i == 9)
//		{
//			if (strstr(cardlist, possiblecases[i]) != NULL && strstr(cardlist, ",0,") != NULL) 
//			{
//				m_ResultDetail[nPlayer].ends[0] = 13;

//                * nFlag = 1;
//				break;
//			}
//		}
//		else
//		{
//			if (strstr(cardlist, possiblecases[i]) != NULL) 
//			{
//				m_ResultDetail[nPlayer].ends[0] = 13-(9-i);

//                * nFlag = 1;
//				break;
//			}
//		}
		

//	}

//	// no kick
//	for (i=0; i<7; i++)
//	{
//		m_ResultDetail[nPlayer].kick[i] = -1;
//	}


//	return 1;
//}

//int SevenPoker::CheckTriple(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 0;

//    int i = 0;
//    int j = 0;
//    int matchcount = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        matchcount = 0;
//        if (m_Cards[nPlayer][i] == -1) continue;
//        for (j = 0; j < 7; j++)
//        {
//            if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13)
//            {
//                if (matchcount == 2)
//                {
//                    m_ResultDetail[nPlayer].ends[0] = m_Cards[nPlayer][i] % 13;
//                    if (m_ResultDetail[nPlayer].ends[0] == 0) m_ResultDetail[nPlayer].ends[0] = 13;
//                }
//                matchcount++;
//            }
//        }
//        if (matchcount == 3) *nFlag = 1;
//    }

//    // 킥 보관
//    int kickcount = 0;

//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].kick[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        int sel_card = m_Cards[nPlayer][i] % 13;
//        if (sel_card == 0) sel_card = 13;

//        if (sel_card != m_ResultDetail[nPlayer].ends[0])
//        {
//            m_ResultDetail[nPlayer].kick[kickcount] = sel_card;
//            kickcount++;
//        }
//    }

//    SortKick(m_ResultDetail[nPlayer].kick);


//    return 1;
//}

//int SevenPoker::CheckTwoPairs(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 0;

//    int i = 0;
//    int j = 0;
//    int k = 0;
//    bool bNext = false;
//    int matchcount = 0;
//    int matchindex = -1;
//    int endcount = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        matchcount = 0;
//        if (m_Cards[nPlayer][i] == -1) continue;
//        for (j = 0; j < 7; j++)
//        {
//            if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13)
//            {
//                if (matchcount == 1)
//                {
//                    matchindex = m_Cards[nPlayer][i] % 13;
//                    if (matchindex == 0) matchindex = 13;

//                    bool bFound = false;
//                    for (k = 0; k < endcount; k++) // 기존 존재여부 체크
//                    {
//                        if (m_ResultDetail[nPlayer].ends[k] == matchindex) bFound = true;
//                    }

//                    if (!bFound)
//                    {
//                        m_ResultDetail[nPlayer].ends[endcount] = matchindex;
//                        matchcount++;
//                    }
//                }
//                else
//                {
//                    matchcount++;
//                }
//            }
//        }
//        if (matchcount == 2) // 페어 발견
//        {
//            bNext = true;
//            matchcount = 0;
//            endcount++;
//        }
//    }

//    // 페어가 두개 이상이면 투페어 처리
//    if (endcount >= 2) *nFlag = 1;

//    // 높은 페어부터 정렬
//    for (i = 0; i < 7; i++)
//    {
//        for (j = i + 1; j < 7; j++)
//        {
//            if (m_ResultDetail[nPlayer].ends[i] < m_ResultDetail[nPlayer].ends[j])
//            {
//                int temp = m_ResultDetail[nPlayer].ends[i];
//                m_ResultDetail[nPlayer].ends[i] = m_ResultDetail[nPlayer].ends[j];
//                m_ResultDetail[nPlayer].ends[j] = temp;
//            }
//        }
//    }


//    /*
//        if (bNext)	//일단 페어가 있는 경우 다음 페어를 찾음
//        {
//            for (i=0; i<7; i++)
//            {
//                matchcount = 0;
//                if (m_Cards[nPlayer][i] == -1) continue;
//                for (j=0; j<7; j++)
//                {
//                    if (m_Cards[nPlayer][i]%13 == m_Cards[nPlayer][j]%13 && m_Cards[nPlayer][i]%13 != matchindex) 
//                    {
//                        if (matchcount == 1)
//                        {
//                            m_ResultDetail[nPlayer].ends[1] = m_Cards[nPlayer][i]%13;
//                            if (m_ResultDetail[nPlayer].ends[1] == 0) m_ResultDetail[nPlayer].ends[1] = 13;

//                            if (m_ResultDetail[nPlayer].ends[1] > m_ResultDetail[nPlayer].ends[0])
//                            {
//                                int temp = m_ResultDetail[nPlayer].ends[1];
//                                m_ResultDetail[nPlayer].ends[1] = m_ResultDetail[nPlayer].ends[0];
//                                m_ResultDetail[nPlayer].ends[0] = temp;
//                            }						

//                            for (k=0; k<7; k++)
//                            {
//                                int index = getcardbyorder(nPlayer, k);
//                                int temp = index%13;
//                                if (temp == 0) temp = 13;
//                                if (temp == m_ResultDetail[nPlayer].ends[0])
//                                {
//                                    m_ResultDetail[nPlayer].type = getcardtype(index);
//                                    break;
//                                }
//                            }
//                        }
//                        matchcount++;
//                    }
//                }
//                if (matchcount == 2) *nFlag = 1;
//            }
//        }
//    */

//    // 킥 보관
//    int kickcount = 0;

//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].kick[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        int sel_card = m_Cards[nPlayer][i] % 13;
//        if (sel_card == 0) sel_card = 13;

//        if (sel_card != m_ResultDetail[nPlayer].ends[0] && sel_card != m_ResultDetail[nPlayer].ends[1])
//        {
//            m_ResultDetail[nPlayer].kick[kickcount] = sel_card;
//            kickcount++;
//        }
//    }

//    SortKick(m_ResultDetail[nPlayer].kick);

//    return 1;
//}

//int SevenPoker::CheckOnePair(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 0;

//    int i = 0;
//    int j = 0;
//    int k = 0;
//    int matchcount = 0;
//    int kickcount = 0;

//    // 초기화
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].ends[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        matchcount = 0;
//        if (m_Cards[nPlayer][i] == -1) continue;
//        for (j = 0; j < 7; j++)
//        {
//            if (m_Cards[nPlayer][i] % 13 == m_Cards[nPlayer][j] % 13)
//            {
//                if (matchcount == 1)
//                {
//                    m_ResultDetail[nPlayer].ends[0] = m_Cards[nPlayer][i] % 13;
//                    if (m_ResultDetail[nPlayer].ends[0] == 0) m_ResultDetail[nPlayer].ends[0] = 13;
//                    for (k = 0; k < 7; k++)
//                    {
//                        int index = getcardbyorder(nPlayer, k);
//                        int temp = index % 13;
//                        if (temp == 0) temp = 13;
//                        if (temp == m_ResultDetail[nPlayer].ends[0])
//                        {
//                            m_ResultDetail[nPlayer].type = getcardtype(index);
//                            break;
//                        }
//                    }
//                }
//                matchcount++;
//            }
//        }
//        if (matchcount == 2)
//        {
//            *nFlag = 1;
//        }
//    }

//    // 킥 보관
//    for (i = 0; i < 7; i++)
//    {
//        m_ResultDetail[nPlayer].kick[i] = -1;
//    }

//    for (i = 0; i < 7; i++)
//    {
//        int sel_card = m_Cards[nPlayer][i] % 13;
//        if (sel_card == 0) sel_card = 13;

//        if (sel_card != m_ResultDetail[nPlayer].ends[0])
//        {
//            m_ResultDetail[nPlayer].kick[kickcount] = sel_card;
//            kickcount++;
//        }
//    }

//    SortKick(m_ResultDetail[nPlayer].kick);

//    return 1;
//}

//void SevenPoker::SortKick(int* array)
//{
//    int i, j, temp;

//    for (i = 0; i < 7; i++)
//    {
//        for (j = i + 1; j < 7; j++)
//        {
//            if (array[i] < array[j])
//            {
//                temp = array[i];
//                array[i] = array[j];
//                array[j] = temp;
//            }
//        }
//    }
//}

//void SevenPoker::SortCard(int* array)
//{
//    int i, j, temp;

//    for (i = 0; i < 7; i++)
//    {
//        for (j = i + 1; j < 7; j++)
//        {
//            if (array[i] > array[j])
//            {
//                temp = array[i];
//                array[i] = array[j];
//                array[j] = temp;
//            }
//        }
//    }
//}


//int SevenPoker::GetWinnerOfGame()
//{
//    // TODO: Add your implementation code here
//    int i = 0;

//    int result[21] = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
//    //int specialcode[21] = {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
//    int maxresult = -1;

//    for (i = 0; i < 21; i++)
//    {
//        if (m_Cards[i][0] == -1) continue;

//        int flag = 0;
//        CheckRoyalStraightFlush(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 9;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckStraightFlush(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 8;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckPoker(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 7;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckFullHouse(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 6;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckFlush(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 5;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckStraight(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 4;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckTriple(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 3;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckTwoPairs(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 2;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//        CheckOnePair(i, &flag);
//        if (flag == 1)
//        {
//            result[i] = 1;
//            if (result[i] > maxresult) maxresult = result[i];
//            break;
//        }
//    }

//    int matchcount = 0;
//    int winner = -1;
//    for (i = 0; i < 21; i++)
//    {
//        if (result[i] == -1) continue;
//        if (result[i] == maxresult)
//        {
//            winner = i;
//            matchcount++;
//        }
//    }

//    //비겼을 경우
//    if (matchcount > 1)
//    {
//    }
//    else
//    {
//    }

//    return 1;
//}

//int SevenPoker::GetWinner(bool bSelfCheck)
//{
//    // TODO: Add your implementation code here

//    m_bSingleMode = bSelfCheck;

//    char log[100];

//    //CMiscUtil::WriteLog("1");

//    int i = 0;
//    int j = 0;

//    SortAllCards();

//    //CMiscUtil::WriteLog("2");


//    int result[21] = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
//    //int specialcode[21] = {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
//    int maxresult = -1;

//    for (i = 0; i < 21; i++)
//    {
//        //CMiscUtil::WriteLog("2.5");

//        if (m_Cards[i][0] == -1) continue;

//        //CMiscUtil::WriteLog("2.6");

//        int flag = 0;
//        CheckRoyalStraightFlush(i, &flag);
//        //CMiscUtil::WriteLog("2.7");
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckRoyalStraightFlush = 1");
//            result[i] = 9;
//            //CMiscUtil::WriteLog("2.8");
//            if (result[i] > maxresult) maxresult = result[i];
//            //CMiscUtil::WriteLog("2.9");
//            continue;
//        }

//        //CMiscUtil::WriteLog("3");

//        CheckStraightFlush(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckStraightFlush = 1");
//            result[i] = 8;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("4");
//        CheckPoker(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckPoker = 1");
//            result[i] = 7;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("5");
//        CheckFullHouse(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckFullHouse = 1");
//            result[i] = 6;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("6");
//        CheckFlush(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckFlush = 1");
//            result[i] = 5;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("7");
//        CheckStraight(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckStraight = 1");
//            result[i] = 4;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("8");
//        CheckTriple(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckTriple = 1");
//            result[i] = 3;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("9");
//        CheckTwoPairs(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckTwoPairs = 1");
//            result[i] = 2;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("10");
//        CheckOnePair(i, &flag);
//        //CMiscUtil::WriteLog("10.5");
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckOnePair = 1");
//            result[i] = 1;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }

//        //CMiscUtil::WriteLog("11");
//        CheckTitle(i, &flag);
//        if (flag == 1)
//        {
//            //CMiscUtil::WriteDebugLog("CheckTitle = 1");
//            result[i] = 0;
//            if (result[i] > maxresult) maxresult = result[i];
//            continue;
//        }
//    }

//    //CMiscUtil::WriteLog("12");
//    int matchcount = 0;
//    int winner = -1;
//    for (i = 0; i < 21; i++)
//    {
//        if (bSelfCheck)
//        {
//            sprintf(log, "Fuck : maxresult = %d, result[%d] = %d", maxresult, i, result[i]);
//            //CMiscUtil::WriteLog(log);
//        }

//        if (result[i] == -1) continue;
//        if (result[i] == maxresult)
//        {
//            winner = i;
//            matchcount++;
//        }
//    }

//    //CMiscUtil::WriteLog("13");

//    if (bSelfCheck)
//    {
//        sprintf(log, "Fuck : maxresult = %d, winner1 = %d", maxresult, winner);
//        //CMiscUtil::WriteLog(log);
//    }

//    //족보가 비겼을 경우 카드 비교
//    if (matchcount > 1)
//    {
//        winner = decidetiebreak(maxresult);
//    }

//    if (bSelfCheck)
//    {
//        sprintf(log, "Fuck : maxresult = %d, winner2 = %d", maxresult, winner);
//        //CMiscUtil::WriteLog(log);

//        if (maxresult == 2 && winner == -1)
//        {
//            // 이 때의 카드값 모두 덤프떠서 확인할 것
//            for (i = 0; i < 21; i++)
//            {
//                for (j = 0; j < 7; j++)
//                {
//                    sprintf(log, "Fuck : m_Cards[%d][%d] = %d", i, j, m_Cards[i][j]);
//                    //CMiscUtil::WriteLog(log);
//                }
//            }
//        }
//    }

//    return winner;
//}

//bool SevenPoker::CheckTiedWinner(int playeridx)
//{
//    return m_ResultDetail[playeridx].win_candidate;
//}

//int SevenPoker::GetKickWinner(int resultcode, int* maxvalue)
//{
//    int i = 0;
//    int j = 0;
//    int k = 0;
//    int winner = -1;
//    int candidate_count = 0;
//    int kickcard_cnt = 0;
//    for (i = 0; i < 21; i++)
//    {
//        m_ResultDetail[i].win_candidate = false;
//    }

//    switch (resultcode)
//    {
//        case 1:
//            {
//                kickcard_cnt = 3;
//                for (i = 0; i < 21; i++)
//                {
//                    int flag = -1;
//                    CheckOnePair(i, &flag);
//                    if (flag == 1 && m_ResultDetail[i].ends[0] == maxvalue[0]) m_ResultDetail[i].win_candidate = true;
//                }
//                break;
//            }
//        case 2:
//            {
//                kickcard_cnt = 1;
//                for (i = 0; i < 21; i++)
//                {
//                    int flag = -1;
//                    CheckTwoPairs(i, &flag);
//                    if (flag == 1 && m_ResultDetail[i].ends[0] == maxvalue[0] && m_ResultDetail[i].ends[1] == maxvalue[1]) m_ResultDetail[i].win_candidate = true;
//                }
//                break;
//            }
//        case 3:
//            {
//                kickcard_cnt = 2;
//                for (i = 0; i < 21; i++)
//                {
//                    int flag = -1;
//                    CheckTriple(i, &flag);
//                    if (flag == 1 && m_ResultDetail[i].ends[0] == maxvalue[0]) m_ResultDetail[i].win_candidate = true;
//                }
//                break;
//            }
//        case 7:
//            {
//                kickcard_cnt = 1;
//                for (i = 0; i < 21; i++)
//                {
//                    int flag = -1;
//                    CheckPoker(i, &flag);
//                    if (flag == 1 && m_ResultDetail[i].ends[0] == maxvalue[0]) m_ResultDetail[i].win_candidate = true;
//                }
//                break;
//            }
//    }

//    if (resultcode == 2)
//    {
//        //if (m_bSingleMode) CMiscUtil::WriteLog("Fuck Two : --------------------------");
//        for (i = 0; i < 21; i++)
//        {
//            if (!m_ResultDetail[i].win_candidate) continue;

//            if (m_bSingleMode)
//            {
//                char log[100];
//                sprintf(log, "Fuck : Two Pair Kick = %d", m_ResultDetail[i].kick[0]);
//                //CMiscUtil::WriteLog(log);
//            }
//        }
//    }

//    for (i = 0; i < kickcard_cnt; i++)
//    {
//        for (j = 0; j < 21; j++)
//        {
//            // 이미 탈락된 플레이어 배제
//            if (!m_ResultDetail[j].win_candidate) continue;
//            for (k = j + 1; k < 21; k++)
//            {
//                // 이미 탈락된 플레이어 배제
//                if (!m_ResultDetail[k].win_candidate) continue;
//                // 상대방의 카드보다 값이 작으면 탈락

//                if (m_ResultDetail[j].kick[i] < m_ResultDetail[k].kick[i])
//                {
//                    m_ResultDetail[j].win_candidate = false;
//                }
//                else if (m_ResultDetail[j].kick[i] > m_ResultDetail[k].kick[i])
//                {
//                    m_ResultDetail[k].win_candidate = false;
//                }
//            }
//        }
//    }

//    // 승자 판별
//    for (i = 0; i < 21; i++)
//    {
//        if (m_ResultDetail[i].win_candidate)
//        {
//            winner = i;
//            candidate_count++;
//            if (m_bSingleMode && resultcode == 2) break;    // 싱글 모드의 투페어는 비길 수 없음(쓰리페어 떄문에 예외처리)
//        }
//    }

//    // 여러명이면 비긴 것임
//    if (candidate_count > 1) winner = -1;

//    return winner;
//}

//int SevenPoker::decidetiebreak(int resultcode)
//{
//    int i = 0;
//    int j = 0;
//    int winner = -1;
//    char log[100];

//    switch (resultcode)
//    {
//        case 0: //타이틀
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckTitle(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 5; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 5; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckTitle(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 비긴 것으로 처리
//                if (winnercount > 1)
//                {
//                    winner = -1;
//                }

//                break;
//            }
//        case 1: //원페어
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckOnePair(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 1; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 1; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckOnePair(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 킥 비교
//                if (winnercount > 1)
//                {
//                    winner = GetKickWinner(resultcode, maxvalue);
//                }
//                break;
//            }
//        case 2: //투페어
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (j = 0; j < 2; j++)
//                {
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckTwoPairs(i, &flag);
//                        if (flag != 1) continue;

//                        m_ResultDetail[i].win_candidate = true;

//                        if (j == 0 || (j == 1 && m_ResultDetail[i].ends[0] == maxvalue[0]))
//                        {
//                            if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                            {
//                                maxvalue[j] = m_ResultDetail[i].ends[j];
//                            }
//                        }

//                        if (m_ResultDetail[i].type < maxtype)
//                        {
//                            maxtype = m_ResultDetail[i].type;
//                        }
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 2; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckTwoPairs(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }


//                //그래도 분간이 안 되면 킥 비교
//                if (winnercount > 1)
//                {
//                    winner = GetKickWinner(resultcode, maxvalue);
//                }

//                break;
//            }
//        case 3: //트리플
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckTriple(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 1; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 1; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckTriple(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 킥 비교
//                if (winnercount > 1)
//                {
//                    winner = GetKickWinner(resultcode, maxvalue);
//                }
//                break;
//            }
//        case 4: //스트레이트
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckStraight(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 1; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 1; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckStraight(i, &flag);
//                        if (flag != 1) continue;

//                        sprintf(log, "Fuck : m_ResultDetail[%d].ends[%d] = %d", i, j, m_ResultDetail[i].ends[j]);
//                        //CMiscUtil::WriteLog(log);
//                        sprintf(log, "Fuck : maxvalue[%d] = %d", j, maxvalue[j]);
//                        //CMiscUtil::WriteLog(log);

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                            if (m_bSingleMode) break;   // 싱글모드에서는 승리로 처리
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                sprintf(log, "winnercount = %d", winnercount);
//                //CMiscUtil::WriteLog(log);

//                //그래도 분간이 안 되면 비긴 것임
//                if (winnercount > 1)
//                {
//                    winner = -1;
//                }
//                break;
//            }
//        case 5: //플러시
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckFlush(i, &flag);

//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 5; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 5; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckFlush(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 비긴 것임
//                if (winnercount > 1)
//                {
//                    winner = -1;
//                }
//                break;
//            }
//        case 6: //풀하우스
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (j = 0; j < 2; j++)
//                {
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckFullHouse(i, &flag);
//                        if (flag != 1) continue;

//                        m_ResultDetail[i].win_candidate = true;

//                        if (j == 0 || (j == 1 && m_ResultDetail[i].ends[0] == maxvalue[0]))
//                        {
//                            if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                            {
//                                maxvalue[j] = m_ResultDetail[i].ends[j];
//                            }
//                        }
//                        if (m_ResultDetail[i].type < maxtype)
//                        {
//                            maxtype = m_ResultDetail[i].type;
//                        }
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 2; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckFullHouse(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 비긴 것임
//                if (winnercount > 1)
//                {
//                    winner = -1;
//                }
//                break;
//            }
//        case 7: //포커
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckPoker(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 1; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 1; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckPoker(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 킥 비교
//                if (winnercount > 1)
//                {
//                    winner = GetKickWinner(resultcode, maxvalue);
//                }
//                break;
//            }
//        case 8: //스트레이트 플러쉬
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckStraightFlush(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 1; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 1; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckStraightFlush(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 비긴 것임
//                if (winnercount > 1)
//                {
//                    winner = -1;
//                }

//                break;
//            }
//        case 9: //로얄 스트레이트 플러쉬
//            {
//                int maxvalue[5] = { -1, -1, -1, -1, -1 };
//                int maxtype = 10;

//                //높은 값들을 넣어둔다
//                for (i = 0; i < 21; i++)
//                {
//                    if (m_Cards[i][0] == -1) continue;
//                    int flag = -1;
//                    CheckRoyalStraightFlush(i, &flag);
//                    if (flag != 1) continue;

//                    m_ResultDetail[i].win_candidate = true;

//                    for (j = 0; j < 1; j++)
//                    {
//                        if (m_ResultDetail[i].ends[j] > maxvalue[j])
//                        {
//                            maxvalue[j] = m_ResultDetail[i].ends[j];
//                        }
//                    }
//                    if (m_ResultDetail[i].type < maxtype)
//                    {
//                        maxtype = m_ResultDetail[i].type;
//                    }
//                }

//                //높은 값을 내림차순으로 비교하여 유일하게 가지고 있는 사람이 승자
//                int winnercount = 0;
//                for (j = 0; j < 1; j++)
//                {
//                    winnercount = 0;
//                    for (i = 0; i < 21; i++)
//                    {
//                        if (m_Cards[i][0] == -1) continue;
//                        int flag = -1;
//                        CheckRoyalStraightFlush(i, &flag);
//                        if (flag != 1) continue;

//                        if (m_ResultDetail[i].ends[j] == maxvalue[j])
//                        {
//                            winner = i;
//                            winnercount++;
//                        }
//                    }
//                    if (winnercount == 1) break;
//                }

//                //그래도 분간이 안 되면 비긴 것임
//                if (winnercount > 1)
//                {
//                    winner = -1;
//                }
//                break;
//            }
//    }

//    return winner;
//}

//int SevenPoker::CheckTitle(int nPlayer, int* nFlag)
//{
//    // TODO: Add your implementation code here
//    *nFlag = 1;

//    int i = 0;
//    for (i = 0; i < 7; i++)
//    {
//        if (m_Cards[nPlayer][i] == -1) continue;
//        m_ResultDetail[nPlayer].ends[i] = (getcardbyorder(nPlayer, i) % 13);
//        if (m_ResultDetail[nPlayer].ends[i] == 0) m_ResultDetail[nPlayer].ends[i] = 13;
//    }

//    m_ResultDetail[nPlayer].type = getcardtype(getcardbyorder(nPlayer, 0));

//    return 1;
//}

//int SevenPoker::GetValidCards(int nPlayer)
//{
//    return 10000000;
//}

//int SevenPoker::GetSequenceType(int nPlayer, int* nType)
//{
//    // TODO: Add your implementation code here
//    if (m_Cards[nPlayer][0] == -1)
//    {
//        *nType = -1;
//        return 1;
//    }

//    int flag = 0;
//    CheckRoyalStraightFlush(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 9;
//        return 1;
//    }
//    CheckStraightFlush(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 8;
//        return 1;
//    }
//    CheckPoker(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 7;
//        return 1;
//    }
//    CheckFullHouse(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 6;
//        return 1;
//    }
//    CheckFlush(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 5;
//        return 1;
//    }
//    CheckStraight(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 4;
//        return 1;
//    }
//    CheckTriple(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 3;
//        return 1;
//    }
//    CheckTwoPairs(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 2;
//        return 1;
//    }
//    CheckOnePair(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 1;
//        return 1;
//    }
//    CheckTitle(nPlayer, &flag);
//    if (flag == 1)
//    {
//        *nType = 0;
//        return 1;
//    }

//    return 1;
//}

//int SevenPoker::GenerateCards()
//{
//    // TODO: Add your implementation code here
//    srand((unsigned)time(NULL));
//    int i = 0;
//    int j = 0;

//    for (i = 0; i < 52; i++)
//    {
//        while (true)
//        {
//            int index = rand() % 52;
//            bool bExist = false;
//            for (j = 0; j < 52; j++)
//            {
//                if (index == m_ShuffledCards[j]) bExist = true;
//            }
//            if (!bExist)
//            {
//                m_ShuffledCards[i] = index;
//                break;
//            }
//        }
//    }

//    return 1;
//}

//int SevenPoker::GetShuffledCards(int nIndex, int* nCardIndex)
//{
//    // TODO: Add your implementation code here
//    *nCardIndex = m_ShuffledCards[nIndex];
//    return 1;
//}

//int SevenPoker::GenerateCardsBySeed(int nSeed)
//{
//    // TODO: Add your implementation code here
//    int i = 0;
//    int j = 0;

//    for (i = 0; i < 52; i++)
//    {
//        while (true)
//        {
//            int index = rand() % 52;
//            bool bExist = false;
//            for (j = 0; j < 52; j++)
//            {
//                if (index == m_ShuffledCards[j]) bExist = true;
//            }
//            if (!bExist)
//            {
//                m_ShuffledCards[i] = index;
//                break;
//            }
//        }
//    }

//    return 1;
//}
