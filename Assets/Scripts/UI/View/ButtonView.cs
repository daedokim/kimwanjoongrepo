using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;
using UniRx;
using com.dug.UI.util;
using System;

namespace com.dug.UI.view
{
    public class ButtonView : MonoBehaviour, IView
    {
        [SerializeField] Button raiseButton = null;
        [SerializeField] Button checkButton = null;
        [SerializeField] Button callButton = null;
        [SerializeField] Button call2Button = null;        
        [SerializeField] Button foldButton = null;
        [SerializeField] Button toLobbyButton = null;
        [SerializeField] Button standUpButton = null;
        [HideInInspector] Text call2Text = null;

        
        private ButtonPresenter presenter;

        private void Awake()
        {
            call2Text = call2Button.gameObject.transform.Find("Text").GetComponent<Text>();
            presenter = new ButtonPresenter(this);

            EnableAllButtons(false);
        }

        public IObservable<Unit> OnRaiseButtonClicked
        {
            get { return raiseButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnCallButtonClicked
        {
            get { return callButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnCall2ButtonClicked
        {
            get { return call2Button.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnCheckButtonClicked
        {
            get { return checkButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnFoldButtonClicked
        {
            get { return foldButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnToLobbyButtonClicked
        {
            get { return toLobbyButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnStandUpButtonClicked
        {
            get { return standUpButton.OnClickAsObservable(); }
        }

        public void EnableAllButtons(bool enable)
        {
            raiseButton.interactable = checkButton.interactable = callButton.interactable = call2Button.interactable = foldButton.interactable = enable;
        }

        public void EnableCheckButton(bool enable)
        {
            if(checkButton != null)
            {
                checkButton.interactable = enable;
            }
        }

        public void EnableCallButton(bool enable)
        {
            if (callButton != null && callButton.IsActive())
            {
                callButton.interactable = enable;
            }
        }

        public void EnableCall2Button(bool enable)
        {
            if (call2Button != null && call2Button.IsActive())
            {
                call2Button.interactable = enable;
            }
        }

        public void EnableRaiseButton(bool enable)
        {
            if (raiseButton != null)
            {
                raiseButton.interactable = enable;
            }
        }

        public void EnableFoldButton(bool enable)
        {
            if (foldButton != null)
            {
                foldButton.interactable = enable;
            }
        }

        public void SetRaiseBetAmount(long amount)
        { 
            if(presenter != null)
            {
                presenter.SetRaiseBetAmount(amount);
            }
        }

        public void SetCallButton(bool isCall2Enable, long lastRaise)
        {
            if(isCall2Enable == true)
            {
                callButton.gameObject.SetActive(false);
                call2Button.gameObject.SetActive(true);


                call2Text.text = "$" + GameUtil.MakePriceString(lastRaise);
            }
            else
            {
                callButton.gameObject.SetActive(true);
                call2Button.gameObject.SetActive(false);
            }
        }
    }

}
