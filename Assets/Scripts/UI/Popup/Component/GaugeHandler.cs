using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace com.dug.UI.popup.component
{
    public class GaugeHandler : MonoBehaviour
     , IPointerDownHandler 
     , IPointerUpHandler
     , IDragHandler
     , IPointerExitHandler
    {
        public float gaugeRate = 0f;

        [SerializeField]
        public Image gauge;

        private float gaugeWidth;

        private RectTransform rt;
        private RectTransform gaugeRt;
        private Camera myCamera;

        void OnMouseDown()
        {
            Vector3 oPosition = rt.localPosition;

            Vector2 pos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, myCamera, out pos);

            if(pos.x > gaugeWidth)
            {
                pos.x = gaugeWidth;
            }

            gaugeRate = pos.x / gaugeWidth;
            gaugeRt.sizeDelta = new Vector2(pos.x, gaugeRt.sizeDelta.y);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopDragg();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnMouseDown();
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnMouseDown();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopDragg();
        }

        private void StopDragg()
        {
        }

        void Awake()
        {
            rt = GetComponent<RectTransform>();
            myCamera = GetComponent<Camera>();
            gaugeRt = gauge.GetComponent<RectTransform>();
            gaugeWidth = rt.sizeDelta.x;
        }
    }

}
