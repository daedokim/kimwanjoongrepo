using System;
using System.Collections;
using System.Collections.Generic;
using com.dug.UI.model;
using com.dug.UI.manager;
using UnityEngine;
using UniRx;
using UnityEngine.UI;


namespace com.dug.UI.component
{
    public class UISitButton : MonoBehaviour
    {
        public int chairIndex = -1;

        GamePlayerModel model = new GamePlayerModel();

        public GamePlayerModel Model { get { return model; } }

        public void Sit(GamePlayerModel model)
        {
            if (model.status == GamePlayerModel.GamePlayerState.Stand)
            { 
                this.gameObject.SetActive(true);
            }
        }

        void Awake()
        {
            this.gameObject.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => {
                GameManager.Instance.SitChair(model.userIndex, chairIndex);
            });
        }

        public void ResetButton()
        {
            this.gameObject.SetActive(true);
        }
    }

}
