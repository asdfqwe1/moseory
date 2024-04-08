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
    private Dash_Skill dash;

    private bool isFacingRight=true;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        dash= GetComponent<Dash_Skill>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, new Vector3(rigid2D.velocity.x/speed,0,0),Color.red);

        Flip();
    }

    public void Move(Vector2 inputMove)
    {
        if (dash.isDashing()) return;
        rigid2D.velocity=new Vector2 (inputMove.x*speed,rigid2D.velocity.y);
    }

    public void Jump( )
    {
        rigid2D.AddForce(Vector2.up*jump,ForceMode2D.Impulse);
    }

    private void Flip()
    {
        if (isFacingRight&&rigid2D.velocity.x < 0f || !isFacingRight&&rigid2D.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1.0f;
            transform.localScale = localScale;
        }
    }
}
