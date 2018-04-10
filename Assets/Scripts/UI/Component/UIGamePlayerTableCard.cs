using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.dug.UI.component
{
    public class UIGamePlayerTableCard : MonoBehaviour
    {
        public int chairIndex = -1;
        private Sprite imageSource;
        [SerializeField]
        public Image image;


        public int ChairIndex
        {
            set
            {
                this.chairIndex = value;
            }
        }
        
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
    }

}
