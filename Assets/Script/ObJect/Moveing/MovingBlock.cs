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
    private bool moving = false; // 발판이 움직이고 있는지 여부

    void Start()
    {
        initialPosition = transform.position;

        // 이동 방향 설정
        destination = initialPosition + direction.normalized * distanceToMove;
    }

    void Update()
    {
        // 발판이 움직이고 있을 때
        if (moving)
        {
            // 이동 속도 및 방향 설정
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, destination, step);

            // 목적지에 도달하면 멈추기
            if (Vector2.Distance(transform.position, destination) < 0.001f)
            {
                moving = false;
            }
        }
    }

    // 발판과 충돌이 시작될 때 호출되는 함수
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어를 발판의 자식으로 설정
            collision.transform.SetParent(transform, true);
            moving = true;
        }
    }

    // 발판과 충돌이 끝날 때 호출되는 함수
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 부모를 초기화하여 발판과의 부모-자식 관계 해제
            collision.transform.SetParent(null, true);
        }
    }
}
