using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;
using com.dug.UI.manager;
using com.dug.UI.model;

namespace com.dug.UI.view
{
    public class BetRangeView : MonoBehaviour, IView
    {
        private BetRangePresenter presenter;
        private GameManager manager;
        private GamePlayerModel model;

        [SerializeField]
        public Button maxButton;
        [SerializeField]
        public Button minButton;
        [SerializeField]
        public Button plusButton;
        [SerializeField]
        public Button minusButton;
        [SerializeField]
        public GameObject scroll;

        private void Awake()
        {
            presenter = new BetRangePresenter(this);
            manager = GameManager.Instance;
        }

    }
}

