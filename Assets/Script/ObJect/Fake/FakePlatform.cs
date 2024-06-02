using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    [SerializeField] float speed, moveDistance; // 블록의 이동 속도
    public bool moveRight = true; // 블록의 이동 방향

    public Vector3 startPosition; // 블록의 시작 위치
    private float movedDistance = 0f; // 블록이 이동한 거리

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

        if (movedDistance < moveDistance)
        {
            float movement = speed * Time.deltaTime;
            if (movedDistance + movement > moveDistance)
            {
                movement = moveDistance - movedDistance;
            }
            if (moveRight)
            {
                transform.Translate(Vector2.right * movement);
            }
            else
            {
                transform.Translate(Vector2.left * movement);
            }
            movedDistance += movement;
        }

    }

    public void ResetPlatform()
    {
        transform.position = startPosition;
        movedDistance = 0f;
    }
}
