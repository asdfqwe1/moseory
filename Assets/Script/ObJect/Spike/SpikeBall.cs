using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float moveDistance = 3f; 

    private Vector2 startPos; 
    private Vector2 endPos;
    private Vector2 nextPos; 

    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(startPos.x, startPos.y + moveDistance);
        nextPos = endPos; 
    }

    void Update()
    {
        Move(); 
    }

    void Move()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);

       
        if (Vector2.Distance(transform.position, nextPos) <= 0.1f)
        {
            nextPos = nextPos == startPos ? endPos : startPos;
        }
    }
}
