using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDrop : MonoBehaviour
{
    public float dropDistanceY = 5f; // �÷��̾ ���ÿ� ������ ���� �Ÿ�
    public float xTolerance = 1f; // x ��ġ ��� ���� ����
    private float initialXPosition; // �ʱ� x ��ġ
    private GameObject player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        initialXPosition = transform.position.x; // �ʱ� x ��ġ ����
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return;
            }
        }

        if (Mathf.Abs(initialXPosition - player.transform.position.x) <= xTolerance)
        {
            float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);

            if (distanceY <= dropDistanceY)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
