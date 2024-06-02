using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDrop : MonoBehaviour
{
    public float dropDistanceY = 5f; // 플레이어가 가시에 접근할 수직 거리
    public float xTolerance = 1f; // x 위치 허용 오차 범위
    private float initialXPosition; // 초기 x 위치
    private GameObject player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        initialXPosition = transform.position.x; // 초기 x 위치 저장
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
