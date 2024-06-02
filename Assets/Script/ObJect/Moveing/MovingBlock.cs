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
    public bool moving = false; // ������ �����̰� �ִ��� ����
    private AudioSource audioSource;

    void Start()
    {
        initialPosition = transform.position;
        destination = initialPosition + direction.normalized * distanceToMove;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, destination, step);

            if (Vector2.Distance(transform.position, destination) < 0.001f)
            {
                moving = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (moving == false)
            {
                audioSource.Play();
            }
            collision.transform.SetParent(transform, true);
            moving = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null, true);
        }
    }
}
