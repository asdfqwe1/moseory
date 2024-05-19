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
        rb.gravityScale = 0; // 처음에는 중력을 0으로 설정
        initialXPosition = transform.position.x; // 초기 x 위치 저장
    }

    private void Update()
    {
        // "Player" 태그를 가진 오브젝트를 찾아서 플레이어로 설정
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return; // 플레이어를 찾지 못했으면 업데이트를 종료
            }
        }

        // 플레이어와 가시 오브젝트의 x 위치가 지정된 오차 범위 내에 있는지 확인
        if (Mathf.Abs(initialXPosition - player.transform.position.x) <= xTolerance)
        {
            // 플레이어와 가시 오브젝트 사이의 수직 거리 계산
            float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);

            // 플레이어가 특정 수직 거리 이내로 들어오면 중력을 활성화
            if (distanceY <= dropDistanceY)
            {
                rb.gravityScale = 10; // 중력을 활성화하여 떨어지게 함
            }
        }
    }
}
