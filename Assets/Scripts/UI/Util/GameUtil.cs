using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.dug.UI.util
{
    public class GameUtil
    {
        // 1000000 식으로 숫자 변환
        public static string MakePriceString(double number)
        {
            string str = number.ToString();
            string newStr = "";
            int cnt = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                cnt++;
                if (cnt == 3 && i > 0)
                {
                    newStr = "," + str.Substring(i, 3) + newStr;
                    cnt = 0;
                }
                else if (i == 0)
                {
                    newStr = str.Substring(i, cnt) + newStr;
                }

            }
            return newStr;
        }


        public static string MakeShortPriceFractionString(double num)
        {
            double fractionCheckNum = 0;

            if (num < 10000)
            {
                return MakePriceString(num);
            }
            else if (num < 10000000)
            {
                fractionCheckNum = num / 1000;
                if (fractionCheckNum % 1 > 0)
                {
                    return MakePriceString(num);
                }
                else
                {
                    num = System.Math.Floor(num / 1000);
                    return MakePriceString(num) + "K";
                }
            }
            else if (num < 10000000000)
            {
                fractionCheckNum = num / 1000000;
                if (fractionCheckNum % 1 > 0)
                {
                    num = num / 1000;
                    return MakePriceString(num) + "K";
                }
                else
                {
                    num = System.Math.Floor(num / 1000000);
                    return MakePriceString(num) + "M";
                }
            }
            else
            {
                fractionCheckNum = num / 1000000000;
                if (fractionCheckNum % 1 > 0)
                {
                    num = System.Math.Floor(num / 1000000);
                    return MakePriceString(num) + "M";
                }
                else
                {
                    num = System.Math.Floor(num / 1000000000);
                    return MakePriceString(num) + "B";
                }
            }
        }
    }

}
