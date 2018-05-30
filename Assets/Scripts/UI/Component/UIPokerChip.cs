using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.util;
using com.dug.UI.manager;

namespace com.dug.UI.component
{
    public class UIPokerChip : MonoBehaviour
    {
        private ChipsManager chipsManager = null;
        [SerializeField]
        public Text amountText;

        private void Awake()
        {
            chipsManager = ChipsManager.Instance;
        }

        public void SetAmount(long amount)
        {
            this.gameObject.SetActive(true);
            RemoveChildren();
            Draw(amount);
        }

        private void Draw(long amount)
        {
            int amountLength = 0;
            int count = 0;
            double diff = 0;
            GameObject chip = null;

            amountText.text = "$" + GameUtil.MakePriceString(amount);

            while (amount > 0 && count < 7)
            {
                amountLength = amount.ToString().Length;

                // amount값이 앞자리수 5보다 크다면 
                if(amount > Math.Pow(10, (amountLength)) / 2)
                {
                    diff = Math.Pow(10, (amountLength)) / 2;
                }
                else
                {
                    diff = Math.Pow(10, (amountLength -1));
                }

                chip = chipsManager.GetChip(amount, false);
                chip.transform.SetParent(this.transform);
                chip.transform.localPosition = new Vector3(0, 10 * count, 0);
                chip.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                amount -= (long) diff;
                count++;
            }
        }

        public void RemoveChildren()
        {
            int count = transform.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
               if(transform.GetChild(i) != amountText.transform)
                {
                    chipsManager.ReStore(transform.GetChild(i));
                }
            }

            
        }

        public void Restore()
        {
            amountText.text = "";
            RemoveChildren();
        }
    }
}


