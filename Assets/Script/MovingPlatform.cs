using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Pfspeed;
    public int StartPoint;
    public Transform[] Points;

    private int i;

    void Start()
    {
        transform.position = Points[StartPoint].position;
    }


    void Update()
    {
            transform.position = Vector2.MoveTowards(transform.position, Points[i].position, Pfspeed * Time.deltaTime);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
        if (Vector2.Distance(transform.position, Points[i].position) < 0.1f)
        {
            i++;
            if (i == Points.Length)
            {
                i = 0;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
