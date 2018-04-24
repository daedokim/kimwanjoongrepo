using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace com.dug.UI.component
{
    public class FlipCard : MonoBehaviour
    {
        private bool isFace = true;

        public bool IsFlipable { get; set; }

        private void Awake()
        {
            isFace = false;
        }

    }
}


