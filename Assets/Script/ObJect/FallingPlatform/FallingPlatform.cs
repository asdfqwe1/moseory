using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallingplatform : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dePos;
    Collider2D collider;

    [SerializeField] float Delay, RespawnTime;

    void Start()
    {
        dePos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>(); // 콜라이더 컴포넌트 가져오기
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(Delay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = false; // 충돌 판정 비활성화
        yield return new WaitForSeconds(RespawnTime);
        Reset();
    }

    private void Reset()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = dePos;
        collider.enabled = true; // 충돌 판정 활성화
    }
}                           

