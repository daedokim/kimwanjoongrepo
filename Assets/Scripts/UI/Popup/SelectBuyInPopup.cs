using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.popup.component;
using UniRx;

namespace com.dug.UI.popup
{
    public class SelectBuyInPopup : PopupBase
    {
        [SerializeField]
        private GameObject gaugeObj;

        [SerializeField]
        private Text gaugeText;

        private GaugeHandler gauge;

        private void Awake()
        {
            gauge = gaugeObj.GetComponent<GaugeHandler>();

            gauge.ObserveEveryValueChanged(x => x.gaugeRate).Subscribe(x => {

                gaugeText.text = x + "";


            });

        }


        

    }

}
