using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    Animator mAnimator;
    public float disappearTime = 2f; // ������ ������� �ð�

    public bool isDisappearing = false; // ������ ������� ������ ����

    void Start()
    {
         mAnimator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDisappearing)
        {
            // ������ ������ �� ������ ������� ��
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
