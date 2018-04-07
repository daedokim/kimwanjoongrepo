using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;

namespace com.dug.UI.view
{
    public class ChipsView : MonoBehaviour, IView
    {
        [SerializeField] public GameObject chips10Prefab;
        [SerializeField] public Text chipsText;

        private ChipsPresenter presenter;

        private void Awake()
        {
            presenter = new ChipsPresenter(this);

            
        }

        public void SetChipsText(string text)
        {
            chipsText.text = text;
        }

        


    }

}
