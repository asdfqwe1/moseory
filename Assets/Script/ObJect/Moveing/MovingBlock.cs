using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // 발판의 이동 방향 (x: 1 y:0 오른쪽 / x:-1 y:0 왼쪽 / x:0 y:1 위 / x:0 y:-1 아래)
    public float speed = 2f; // 발판의 이동 속도
    public float distanceToMove = 5f; // 발판이 이동할 거리

    private Vector2 initialPosition; // 발판의 초기 위치
    private Vector2 destination; // 발판의 목적지
    public bool moving = false; // 발판이 움직이고 있는지 여부

    void Start()
    {
        initialPosition = transform.position;
        destination = initialPosition + direction.normalized * distanceToMove;
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
