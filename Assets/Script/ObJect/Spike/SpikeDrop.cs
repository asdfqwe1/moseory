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
        rb.gravityScale = 0; // ó������ �߷��� 0���� ����
        initialXPosition = transform.position.x; // �ʱ� x ��ġ ����
    }

    private void Update()
    {
        // "Player" �±׸� ���� ������Ʈ�� ã�Ƽ� �÷��̾�� ����
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return; // �÷��̾ ã�� �������� ������Ʈ�� ����
            }
        }

        // �÷��̾�� ���� ������Ʈ�� x ��ġ�� ������ ���� ���� ���� �ִ��� Ȯ��
        if (Mathf.Abs(initialXPosition - player.transform.position.x) <= xTolerance)
        {
            // �÷��̾�� ���� ������Ʈ ������ ���� �Ÿ� ���
            float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);

            // �÷��̾ Ư�� ���� �Ÿ� �̳��� ������ �߷��� Ȱ��ȭ
            if (distanceY <= dropDistanceY)
            {
                rb.gravityScale = 10; // �߷��� Ȱ��ȭ�Ͽ� �������� ��
            }
        }
    }
}
