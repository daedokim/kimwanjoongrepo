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

        SetAlpha(1);

        this.index = index;
    }

    public void SetFace(bool isFace)
    {
        face.gameObject.SetActive(isFace);
        back.gameObject.SetActive(!isFace);
    }

    public void SetAlpha(float alpha)
    {
        face.color = new Color(face.color.r, face.color.g, face.color.b, alpha);
        back.color = new Color(back.color.r, back.color.g, back.color.b, alpha);
    }
}
