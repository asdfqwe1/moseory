using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jump = 25.0f;
    private Rigidbody2D rigid2D;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float x)
    {
        rigid2D.velocity=new Vector2 (x*speed,rigid2D.velocity.y);
    }

    public void Jump( )
    {
        rigid2D.AddForce(Vector2.up*jump,ForceMode2D.Impulse);
    }
}
