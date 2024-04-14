using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Player_C move;
    private Collision coll;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        move = GetComponentInParent<Player_C>();
        coll = GetComponentInParent<Collision>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("OnGround", coll.onGround);
        anim.SetBool("OnWall", coll.onWall);
        anim.SetBool("OnRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);
    }

    public void SetHorizontalMovement(float x,float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVel", yVel);
    }

    public void Trigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {
        if(move.wallGrab||move.wallSlide)
        {
            if (side == -1 && sr.flipX) return;
            if (side == 1 && !sr.flipX) return;
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }
}
