using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb;

    [Header("Movement")]
    [Tooltip("�̵� �ӵ�")]
    public float speed = 10f;
    [Tooltip("���� ����")]
    public float jumpForce = 2f;
    [Tooltip("���� �پ��� �� �����̵� �ӵ�")]
    public float slideSpeed = 5f;
    [Tooltip("������ ������ �� ����(���� �� ������ Ȯ�� ����)")]
    public float wallJumpLerp = 10f;
    [Tooltip("�뽬 �ӵ�(ũ��)")]
    public float dashSpeed = 20f;
    [Space]
    [Header("Control Timing")]
    public float dashTime = .3f;
    [Space]
    [Header("Boolean")]
    private bool wallGrab;
    private bool wallSlide;
    private bool wallJumped;
    private bool canMove = true;
    private bool isDashing;
    [Space]
    private bool hasDashed;
    private bool groundTouch;
    [Header("Scale")]
    public float gravityScale = 3f;
    public float wallJumpToY;
    public float wallJumpToX;
    [Space]
    [HideInInspector]
    public int side = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();

        rb.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x,y);
        Walk(dir);
        //wallWalk
        wallGrab = coll.onWall&&Input.GetButton("Fire2");
        
        if (wallGrab&&!isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f) rb.velocity = new Vector2(rb.velocity.x, 0);
            float speedModifier = y > 0 ? .5f : 1f;
            rb.velocity=new Vector2(rb.velocity.x, y*(speed*speedModifier));
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x!=0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide=false;
        }
        //jump
        if (Input.GetButtonDown("Fire3"))
        {
            if (coll.onGround)
            {
                Jump(Vector2.up, false);
            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
        }
        //dash
        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if(xRaw!=0||yRaw!=0) Dash(xRaw,yRaw);
        }
        //���� ��Ҵ���?
        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }
        if(!coll.onGround&&groundTouch)
        {
            groundTouch = false;
        }

        if (wallGrab || wallSlide || !canMove) return;

        if (x > 0)
        {
            side = 1;
        }
        if (x < 0)
        {
            side = -1;
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove) return;
        if (wallGrab) return;

        if(!wallJumped)
        {
            rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Dash(float x, float y)
    {
        hasDashed = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait(dashTime));
    }

    private void Jump(Vector2 dir, bool wall)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || (side == -1 && coll.onLeftWall))
        {
            side *= -1;
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 WallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / wallJumpToY + WallDir / wallJumpToX), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if (coll.wallSide != side)
        {

        }
        bool pushingWall = false;
        if((rb.velocity.x>0&&coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push=pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }
    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    IEnumerator DashWait(float time)
    {
        StartCoroutine(GroundDash());

        rb.gravityScale = 0;
        GetComponent<Betterjump>().enabled = false;
        wallJumped = true;
        isDashing = true;
        yield return new WaitForSeconds(time);
        rb.gravityScale = gravityScale;
        GetComponent<Betterjump>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }
    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if(coll.onGround) { hasDashed = false; }
    }
}
