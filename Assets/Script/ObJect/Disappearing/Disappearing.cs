using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    Animator mAnimator;
    public float disappearTime = 2f; // 발판이 사라지는 시간

    private bool isDisappearing = false; // 발판이 사라지는 중인지 여부

    void Start()
    {
         mAnimator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDisappearing)
        {
            // 발판이 밟혔을 때 발판을 사라지게 함
            Invoke("Disappear", disappearTime);
            isDisappearing = true;
            mAnimator.SetTrigger("hit");
        }
    }

    void Disappear()
    {
        // 발판을 비활성화하여 사라지게 함
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
