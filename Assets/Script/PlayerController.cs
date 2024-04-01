using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;

    // Start is called before the first frame update
    void Start()
    {
        movement2D = GetComponent<Movement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x=Input.GetAxisRaw("Horizontal");
        movement2D.Move(x);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement2D.Jump();
        }
    }
}
