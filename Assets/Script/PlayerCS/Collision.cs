using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask oneWayLayer;
    public LayerMask thornLayer;
    [Header("Player Collision")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    [Space]
    [Header("Hit Collision")]
    [SerializeField]
    private CapsuleCollider2D capsuleColl2D;
    [Space]
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;
    [Space]
    public bool onHit;

    private void Start()
    {
        capsuleColl2D = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if(Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer)||Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, oneWayLayer)) onGround=true;
        else onGround=false;
        //onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall= Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;

        onHit = Physics2D.OverlapCapsule((Vector2)transform.position+capsuleColl2D.offset,capsuleColl2D.size,capsuleColl2D.direction,0f,thornLayer);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
