using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    public float disappearTime = 2f; // ������ ������� �ð�

    private bool isDisappearing = false; // ������ ������� ������ ����

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDisappearing)
        {
            // ������ ������ �� ������ ������� ��
            Invoke("Disappear", disappearTime);
            isDisappearing = true;
        }
    }

    void Disappear()
    {
        // ������ ��Ȱ��ȭ�Ͽ� ������� ��
        gameObject.SetActive(false);
    }
}
