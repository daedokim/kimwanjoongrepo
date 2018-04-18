using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;
using UniRx;
using System;

namespace com.dug.UI.view
{
    public class ButtonView : MonoBehaviour, IView
    {
        [SerializeField] Button raiseButton = null;
        [SerializeField] Button checkButton = null;
        [SerializeField] Button callButton = null;
        [SerializeField] Button foldButton = null;
        [SerializeField] Button allinButton = null;

        private ButtonPresenter presenter;

        private void Awake()
        {
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

        public IObservable<Unit> OnCheckButtonClicked
        {
            get { return checkButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnFoldButtonClicked
        {
            get { return foldButton.OnClickAsObservable(); }
        }

        public IObservable<Unit> OnAllinButtonClicked
        {
            get { return allinButton.OnClickAsObservable(); }
        }

        public void EnableAllButtons(bool enable)
        {
            raiseButton.interactable = checkButton.interactable = callButton.interactable = foldButton.interactable = allinButton.interactable = enable;
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
            if (callButton != null)
            {
                callButton.interactable = enable;
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

        public void EnableAllinButton(bool enable)
        {
            if (allinButton != null)
            {
                allinButton.interactable = enable;
            }
        }


    }

}
