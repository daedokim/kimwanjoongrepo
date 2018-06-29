using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.Managers;
namespace com.dug.UI.popups
{
    public class PopupBase : MonoBehaviour
    {
        [SerializeField]
        public GameObject blockLayer;

        public virtual void InitValue(System.Object[] lists)
        {
            
        }


        public void ClosePopup(System.Object[] lists)
        {
            PopupManager.Instance.ClosePopup(this);
        }
    }
}

