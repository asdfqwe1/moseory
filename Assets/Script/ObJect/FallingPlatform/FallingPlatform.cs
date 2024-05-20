using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallingplatform : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dePos;
    Collider2D pcollider;

    [SerializeField] float Delay, RespawnTime;

    void Start()
    {
        dePos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        pcollider = GetComponent<Collider2D>(); // �ݶ��̴� ������Ʈ ��������
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
        pcollider.enabled = false; // �浹 ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(RespawnTime);
        Reset();
    }

    private void Reset()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = dePos;
        pcollider.enabled = true; // �浹 ���� Ȱ��ȭ
    }
}                           

