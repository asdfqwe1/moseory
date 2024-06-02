using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class Player_C : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb;
    private PlayerAnimation anim;
    public DashCrystal dashCrystal;
    public PrefabObjectManager prefabObjectManager;
    private AudioSource audioSource;

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
    public float defaultStamina = 3f;
    public float reviveWaitTime = 2f;
    [Space]
    [Header("Boolean")]
    public bool wallGrab;
    public bool wallSlide;
    public bool wallJumped;
    public bool canMove = true;
    public bool isDashing;
    public bool isDialoging=false;
    [Space]
    private bool hasDashed;
    private bool groundTouch;
    private float stamina;
    private bool isDead;
    private bool isSlide;
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
        audioSource = GetComponent<AudioSource>();

        rb.gravityScale = gravityScale;
        GameManager.Instance.fadeManager.SetAnimSpeed("DieSpeed",reviveWaitTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.onHit)
        {
            KillSwitch();
        }
        if (!wallSlide) {AudioManager.Instance.StopLoopSound("SlideL"); isSlide=false;}

        float x = isDialoging == false ? Input.GetAxis("Horizontal") : 0f;
        float y = isDialoging == false ? Input.GetAxis("Vertical") : 0f;
        float xRaw = isDialoging == false ? Input.GetAxisRaw("Horizontal") : 0f;
        float yRaw = isDialoging == false ? Input.GetAxisRaw("Vertical") : 0f;
        Vector2 dir = new Vector2(x,y);
        Walk(dir);

        anim.SetHorizontalMovement(x, y, rb.velocity.y);

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
            float push=coll.onRightWall==true ? 0.05f : -0.05f;
            stamina -= Time.deltaTime;

            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x+push, y * (speed * speedModifier));
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

        if (!coll.onWall || coll.onGround){
            wallSlide = false;
        }

        if (Input.GetButtonDown("Fire3")&&!isDialoging)
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
        AudioManager.Instance.PlaySound("Dash");
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
        AudioManager.Instance.PlaySound("Jump");
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
        //if(!AudioManager.Instance.FindPlayingSound("WallSlide")) AudioManager.Instance.PlaySound("WallSlide");
        if(!isSlide){
            AudioManager.Instance.PlaySound("SlideL",0,true,SoundType.PLAYER);
            isSlide=true;
        }
    }
    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        stamina = defaultStamina;
        jumpParticle.Play();
        AudioManager.Instance.PlaySound("Jump");
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
        int crystalLayer = LayerMask.NameToLayer("Crystal");
      
        if (collision.gameObject.layer == crystalLayer)
        {
            hasDashed = false;
            isDashing = false;
            stamina = defaultStamina;

            audioSource.Play();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            KillSwitch();
        }


    }

    private void Die(){
        isDead = true;
        anim.Trigger("isDead");
        GameManager.Instance.fadeManager.SetTrigger("Die");
    }

    private void Revive(){
        this.transform.position=GameManager.Instance.stageManager.GetNowSave();
        this.gameObject.transform.SetParent(null);
        try{RespawnPlayer();}catch(NullReferenceException ex){var e=ex;}
        isDead = false;
    }

    public void KillSwitch(){
        Debug.Log("Get playerKillSwitch! IsDead status = "+isDead);
        if (isDead) return;
        StartCoroutine(this.DieAndRevive(reviveWaitTime));
    }

    public void RespawnPlayer()
    {
        prefabObjectManager.ResetToInitialState();
    }

    IEnumerator DieAndRevive(float waitTime)
    {
        Die();
        yield return new WaitForSeconds(waitTime/2);
        Revive();
    }

}
