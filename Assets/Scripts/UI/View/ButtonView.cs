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
            raiseButton.enabled = checkButton.enabled = callButton.enabled = foldButton.enabled = allinButton.enabled = enable;
        }

        public void EnableCheckButton(bool enable)
        {
            checkButton.enabled = enable;
        }
    }

}
