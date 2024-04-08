using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betterjump : MonoBehaviour
{
    private Rigidbody2D rb;
    [Tooltip("�������� �ӵ� ����")]
    public float fallMultiplier = 2.5f;
    [Tooltip("ü�� �ð� ����\n���ڰ� ���� ���� ���� ������\n���� �������� �������� �̵��ϴ� ���� ����")]
    public float lowJumpMultiplier = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Fire3"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
