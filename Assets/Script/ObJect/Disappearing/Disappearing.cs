using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    Animator mAnimator;
    AudioSource audioSource;
    public float disappearTime = 2f;

    public bool isDisappearing = false; 

    void Start()
    {
         mAnimator = GetComponent<Animator>();
         audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDisappearing)
        {
            if(isDisappearing == false)
            {
                audioSource.Play();
            }
            Invoke("Disappear", disappearTime);
            isDisappearing = true;
            mAnimator.SetTrigger("hit");
        }
    }

    void Disappear()
    {  
        gameObject.SetActive(false);
    }
}
