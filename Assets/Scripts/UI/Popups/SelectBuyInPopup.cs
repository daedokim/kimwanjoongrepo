using com.dug.UI.popups.component;
using com.dug.UI.DTO;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.Managers;
using com.dug.UI.util;
using com.dug.UI.DAO;
using com.dug.UI.Networks;
using System;
using com.dug.UI.Events;

namespace com.dug.UI.popups
{
    public class SelectBuyInPopup : PopupBase
    {
        [SerializeField]
        private GameObject gaugeObj = null;

        [SerializeField]
        private Text gaugeText = null;

        [SerializeField]
        private Text minimumText = null;

        [SerializeField]
        private Text maximumText = null;

        [SerializeField]
        private Text myChipsText = null;

        [SerializeField]
        private Button okButton = null;

        [SerializeField]
        private Button cancelButton = null;

        [SerializeField]
        private Toggle autoRefill = null;

        [SerializeField]
        private Button closeButton = null;


        private GaugeHandler gauge;

        private long minBuyIn = 0;
        private long maxBuyIn = 0;
        private int chairIndex = -1;
        private long selectedBuyIn = 0;

        RoomDAO dao = new RoomDAO();

        public static readonly string SIT_CHAIR_COMPLETE = "SelectBuyInPopup.SIT_CHAIR_COMPLETE";


        private void Awake()
        {
            gauge = gaugeObj.GetComponent<GaugeHandler>();
        }

        public override void InitValue(System.Object[] lists)
        {
            minBuyIn = Convert.ToInt64(lists[0]);
            maxBuyIn = Convert.ToInt64(lists[1]);
            chairIndex = Convert.ToInt32(lists[2]);

            selectedBuyIn = maxBuyIn;

            maximumText.text = GameUtil.MakePriceString(maxBuyIn);
            minimumText.text = GameUtil.MakePriceString(minBuyIn);
            
            gaugeText.text = GameUtil.MakePriceString(maxBuyIn);

            myChipsText.text = GameUtil.MakePriceString(com.dug.UI.Networks.UserData.Instance.coin);

            gauge.ObserveEveryValueChanged(x => x.gaugeRate).Skip(1).Subscribe(x => {
                selectedBuyIn = (long)(minBuyIn + (maxBuyIn - minBuyIn) * (Math.Floor(x * 10) / 10));
                gaugeText.text = GameUtil.MakePriceString(selectedBuyIn);
            });

            object thiz = this;

            okButton.onClick.AsObservable().Subscribe(_ => {

                okButton.interactable = false;
                cancelButton.interactable = false;

                GameEventHandler.Instance.AddHandler(thiz, SIT_CHAIR_COMPLETE, OnSitChairComplete);
                GameManager.Instance.SitChair(UserData.Instance.userIndex, chairIndex, selectedBuyIn);                

            });

            cancelButton.onClick.AsObservable().Subscribe(_ => {
                ClosePopup(null);
            });

            closeButton.onClick.AsObservable().Subscribe(_ => {
                ClosePopup(null);
            });

        }

        public void OnSitChairComplete(object obj)
        {
            GameEventHandler.Instance.RemoveHandler(this, SIT_CHAIR_COMPLETE, OnSitChairComplete);

            CRUDResult result = (CRUDResult)obj;

            if (result.resultType == CRUDResult.ResultType.FAILED)
            {
                cancelButton.interactable = true;
            }
            else if (result.resultType == CRUDResult.ResultType.SUCCESS)
            {
                UserData.Instance.isAutoRefill = autoRefill.isOn;
                ClosePopup(null);
            }

        }
    }

}
