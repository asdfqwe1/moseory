using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash_Skill : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private TrailRenderer tr;

    public float dashSpeed=15.0f;
    public float dashTime=0.1f;
    public float dashCoolDown = 1f;
    private bool dashed = false;
    private bool dashing = false;
    // Start is called before the first frame update
    void Start()
    {
        if(rigid2D == null)
        {
            rigid2D = GetComponent<Rigidbody2D>();
        }
        if(tr == null) tr=GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Dash()
    {
        if (!dashed)
        {
            dashed = true;
            dashing = true;
            tr.emitting = true;
            Debug.Log("Dash!");

            float originGravity = rigid2D.gravityScale;
            rigid2D.gravityScale = 0f;
            rigid2D.velocity=new Vector2(transform.localScale.x*dashSpeed,0f);
            yield return new WaitForSeconds(dashTime);
            dashing = false;
            tr.emitting = false;
            rigid2D.gravityScale = originGravity;
            yield return new WaitForSeconds(dashCoolDown);
            dashed = false;
        }
        yield return null;
    }
    public bool isDashing() { return dashing; }
}
