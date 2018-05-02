using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour {

    [SerializeField]
    private string nowPlaying;

    [SerializeField]
    private List<Sprite> spriteList;

    private float time = 0f;
    private float delayTime = 0f;
    private int frame = 0;
    private bool loop = false;



}
