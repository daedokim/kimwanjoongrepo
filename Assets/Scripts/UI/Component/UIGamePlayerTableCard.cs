using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.events;

namespace com.dug.UI.component
{
    public class UIGamePlayerTableCard : MonoBehaviour
    {
        public int chairIndex = -1;
        private Sprite imageSource;
        [SerializeField]
        public Image image;

        
        public Sprite ImageSource
        {
            get { return this.imageSource; }
            set
            {
                this.imageSource = value;
                Render();
            }
        }

        private void Render()
        {
            image.sprite = imageSource;
        }

        public void Handout()
        {
            this.gameObject.SetActive(true);

            GameEvent.Instance.InvokeHandoutCompleteEvent(chairIndex);
        }
    }

}
