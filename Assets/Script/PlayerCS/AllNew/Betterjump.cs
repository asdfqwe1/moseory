using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betterjump : MonoBehaviour
{
    private Rigidbody2D rb;
    [Tooltip("떨어지는 속도 조절")]
    public float fallMultiplier = 2.5f;
    [Tooltip("체공 시간 조절\n숫자가 높을 수록 빨리 떨어짐\n점프 지점에서 고점까지 이동하는 동안 동작")]
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
