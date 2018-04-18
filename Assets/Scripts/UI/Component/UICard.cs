using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UICard : MonoBehaviour
{
    public int index;

    [SerializeField]
    public Image face;
    [SerializeField]
    public Image back;

    public void Draw(Sprite sprite, Sprite cardBack, int index)
    {
        face.sprite = sprite;
        back.sprite = cardBack;

        this.index = index;
    }

    internal void SetFace(bool isFace)
    {
        face.gameObject.SetActive(isFace);
        back.gameObject.SetActive(!isFace);
    }
}
