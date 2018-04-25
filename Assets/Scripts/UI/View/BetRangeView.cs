using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;
using UniRx;
using System;

namespace com.dug.UI.view
{
    public class BetRangeView : MonoBehaviour, IView
    {
        private BetRangePresenter presenter;
       
        [SerializeField]
        public Button maxButton;
        [SerializeField]
        public Button minButton;
        [SerializeField]
        public Button plusButton;
        [SerializeField]
        public Button minusButton;
        [SerializeField]
        public Scrollbar scrollBar;
        [SerializeField]
        public Text rangeText;

        private void Awake()
        {
            presenter = new BetRangePresenter(this);
        }
     
        public IObservable<Unit> OnMaxButtonClicked
        {
            get { return maxButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnMinButtonClicked
        {
            get { return minButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnPlusButtonClicked
        {
            get { return plusButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnMinusButtonClicked
        {
            get { return minusButton.OnClickAsObservable(); }
        }

        public IObservable<float> OnScrollBarChanged
        {
            get { return scrollBar.OnValueChangedAsObservable(); }
        }

        public void SetRangeText(string str)
        {
            rangeText.text = str;
        }

        public float GetScrollRate()
        {
            return scrollBar.value;
        }

        public void EnableScrollBar(bool enable)
        {
            scrollBar.interactable = enable;
        }
    }
}

