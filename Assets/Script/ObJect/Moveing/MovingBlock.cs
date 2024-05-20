using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // ������ �̵� ���� (x: 1 y:0 ������ / x:-1 y:0 ���� / x:0 y:1 �� / x:0 y:-1 �Ʒ�)
    public float speed = 2f; // ������ �̵� �ӵ�
    public float distanceToMove = 5f; // ������ �̵��� �Ÿ�

    private Vector2 initialPosition; // ������ �ʱ� ��ġ
    private Vector2 destination; // ������ ������
    private bool moving = false; // ������ �����̰� �ִ��� ����

    void Start()
    {
        initialPosition = transform.position;

        // �̵� ���� ����
        destination = initialPosition + direction.normalized * distanceToMove;
    }

    void Update()
    {
        // ������ �����̰� ���� ��
        if (moving)
        {
            // �̵� �ӵ� �� ���� ����
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, destination, step);

            // �������� �����ϸ� ���߱�
            if (Vector2.Distance(transform.position, destination) < 0.001f)
            {
                moving = false;
            }
        }
    }

    // ���ǰ� �浹�� ���۵� �� ȣ��Ǵ� �Լ�
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ ������ �ڽ����� ����
            collision.transform.SetParent(transform, true);
            moving = true;
        }
    }

    // ���ǰ� �浹�� ���� �� ȣ��Ǵ� �Լ�
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� �θ� �ʱ�ȭ�Ͽ� ���ǰ��� �θ�-�ڽ� ���� ����
            collision.transform.SetParent(null, true);
        }
    }
}
