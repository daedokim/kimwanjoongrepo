using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.dug.UI.component
{
    public class UITimeline : MonoBehaviour
    {
        private Animator animator;
        private int chairIndex;
        private bool isPlaying = false;

        private void Awake()
        {
            this.animator = GetComponent<Animator>();
            this.animator.speed = 0;
            isPlaying = false;
        }

        public void Draw(RuntimeAnimatorController controller, int chairIndex)
        {
            this.animator.runtimeAnimatorController = controller;
            this.chairIndex = chairIndex;

            StopCountDown();

            AnimationEvent evt = new AnimationEvent();
            evt.objectReferenceParameter = this;
            evt.time = 15;
            evt.functionName = "AnimationComplete";
            this.animator.runtimeAnimatorController.animationClips[0].AddEvent(evt);
        }

        public void AnimationComplete()
        {
            Debug.Log("eee");

        }
        public void StartCountDown()
        {
            if (this.animator == null)
                return;

            if (isPlaying == true)
            {
                StopCountDown(true);
            }

            transform.gameObject.SetActive(true);
            this.animator.Rebind();
            this.animator.speed = 1;

            isPlaying = true;
        }

        public void StopCountDown(bool semiStop = false)
        {
            if (this.animator == null)
                return;

            if (semiStop == false)
            {
                transform.gameObject.SetActive(false);
            }
            
            this.animator.speed = 0;
            isPlaying = false;
        }

    }
}

