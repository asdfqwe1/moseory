using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTrigger : MonoBehaviour
{
    // 두 개의 블록을 참조하기 위한 변수
    public GameObject block1;
    public GameObject block2;

    // 블록들에 대한 움직임 스크립트
    private FakePlatform block1Movement;
    private FakePlatform block2Movement;

    void Start()
    {
        // 블록들의 움직임 스크립트를 참조
        block1Movement = block1.GetComponent<FakePlatform>();
        block2Movement = block2.GetComponent<FakePlatform>();

        // 초기에는 블록들의 움직임 스크립트를 비활성화
        block1Movement.enabled = false;
        block2Movement.enabled = false;

        // 두 번째 블록의 움직임 방향을 반대로 설정
        block1Movement.moveRight = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거에 다른 Collider가 진입했을 때
        if (other.CompareTag("Player"))
        {
            // 블록들의 움직임 스크립트를 활성화하여 움직임 시작
            block1Movement.enabled = true;
            block2Movement.enabled = true;
        }
    }

}
