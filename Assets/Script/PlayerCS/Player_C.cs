using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player_C : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb;
    private PlayerAnimation anim;
    public DashCrystal dashCrystal;

    [Header("Movement")]
    [Tooltip("이동 속도")]
    public float speed = 10f;
    [Tooltip("점프 강도")]
    public float jumpForce = 2f;
    [Tooltip("벽에 붙었을 때 슬라이딩 속도")]
    public float slideSpeed = 5f;
    [Tooltip("벽에서 점프할 때 강도(조작 시 오류날 확률 있음)")]
    public float wallJumpLerp = 10f;
    [Tooltip("대쉬 속도(크기)")]
    public float dashSpeed = 20f;
    [Space]
    [Header("Control Timing")]
    public float dashTime = .3f;
    public float defaultStamina = 3f;
    [Space]
    [Header("Boolean")]
    public bool wallGrab;
    public bool wallSlide;
    public bool wallJumped;
    public bool canMove = true;
    public bool isDashing;
    [Space]
    private bool hasDashed;
    private bool groundTouch;
    private float stamina;
    [Header("Scale")]
    public float gravityScale = 3f;
    public float wallJumpToY;
    public float wallJumpToX;
    [Space]
    [HideInInspector]
    public int side = 1;
    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        anim=GetComponentInChildren<PlayerAnimation>();

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

        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        /*
        //wallWalk
        if(coll.onWall&&Input.GetButton("Fire2")&&canMove)
        {
            if (side != coll.wallSide) anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
        }

        if(Input.GetButton("Fire2")||!coll.onWall||!canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }
        
        if(coll.onGround&&!isDashing)
        {
            wallJumped = false;
            GetComponent<Betterjump>().enabled = true;
        }

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
            anim.Trigger("Jump");

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
        //땅에 닿았는지?
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
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }
        */
        if (coll.onWall && Input.GetButton("Fire2") && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire2") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<Betterjump>().enabled = true;
        }

        if (wallGrab && !isDashing && stamina > 0)
        {
            stamina -= Time.deltaTime;

            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Fire3"))
        {
            anim.Trigger("Jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        wallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if (x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
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

        anim.Trigger("Dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait(dashTime));
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || (side == -1 && coll.onLeftWall))
        {
            side *= -1;
            anim.Flip(side);
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
            anim.Flip(side*-1);
        }
        if(!canMove) { return; }

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
        stamina = defaultStamina;
        jumpParticle.Play();
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

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<Betterjump>().enabled = false;
        wallJumped = true;
        isDashing = true;
        canMove = false;

        yield return new WaitForSeconds(time);

        dashParticle.Stop();
        rb.gravityScale = gravityScale;
        GetComponent<Betterjump>().enabled = true;
        wallJumped = false;
        isDashing = false;
        canMove = true;
    }
    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if(coll.onGround) { hasDashed = false; }
    }
    void wallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }
    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("crystal"))
        {
            hasDashed = false;
            isDashing = false;
            stamina = defaultStamina;

            Vector3 respawnPosition = collision.transform.position;
            float respawnDelay = 3f;

            FindObjectOfType<DashCrystal>().RespawnCrystal(respawnPosition, respawnDelay);

            Destroy(collision.gameObject);
        }
    }

}
