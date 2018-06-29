using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.popups;
using System;

namespace com.dug.UI.Managers
{
    public class PopupManager : Singleton<PopupManager>
    {
        public List<PopupBase> popups = new List<PopupBase>();
        public GameObject[] popupPrefabs;
        public static string POPUP_CANVAS_PATH = "/UI/popupCanvas";
        public RectTransform parentCanvas;


        public void Awake()
        {
            parentCanvas = GameObject.Find("/UI/popupCanvas").GetComponent<RectTransform>();
        }

        public enum PopupTypes
        {
            SELECT_BUYIN_POPUP = 0
        }

        public PopupBase OpenPopup(PopupTypes type, params System.Object[] lists)
        {
            GameObject popupPrefab = GetPopupPrefab(type);
            PopupBase popup = null;

            if (popupPrefab != null)
            {
                GameObject popupObject = Instantiate(popupPrefab, Vector3.zero, Quaternion.identity);
                popupObject.SetActive(true);
                popupObject.transform.SetParent(parentCanvas);

                RectTransform rt = popupObject.GetComponent<RectTransform>();
                rt.sizeDelta = parentCanvas.rect.size;
                rt.anchoredPosition = new Vector2(-1f, 1f);

                popup = popupObject.GetComponent<PopupBase>();
                popup.InitValue(lists);
                popup.gameObject.SetActive(true);
            }
            return popup;
        }

        private GameObject GetPopupPrefab(PopupTypes type)
        {
            GameObject popup = null;
            if (popupPrefabs != null && popupPrefabs.Length >= (int)type)
            {
                popup = popupPrefabs[(int)type];
            }
            return popup;
        }

        public void ClosePopup(PopupBase popup)
        {
            GameObject.Destroy(popup.gameObject);
        }
    }
}

