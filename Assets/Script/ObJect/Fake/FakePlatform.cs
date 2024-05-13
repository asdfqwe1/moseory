using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    [SerializeField] float speed, moveDistance; // ����� �̵� �ӵ�
    public bool moveRight = true; // ����� �̵� ����

    private Vector3 startPosition; // ����� ���� ��ġ
    private float movedDistance = 0f; // ����� �̵��� �Ÿ�

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ����� �̵��ϴ� �κ�
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
}
