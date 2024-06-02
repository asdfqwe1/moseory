using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dePos;
    Collider2D pcollider;
    AudioSource audioSource;

    [SerializeField] float Delay;

    private IEnumerator currentCoroutine;


    void Start()
    {
        dePos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        pcollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        SetStatic();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = Fall();
            StartCoroutine(currentCoroutine);
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(Delay);
        if (rb.bodyType == RigidbodyType2D.Static)
        {
            audioSource.Play();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void SetStatic()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

  
}

