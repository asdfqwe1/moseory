using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;
    private Dash_Skill dash;

    // Start is called before the first frame update
    void Start()
    {
        movement2D = GetComponent<Movement2D>();
        dash = GetComponent<Dash_Skill>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement2D.Move(inputMove);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement2D.Jump();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(dash.Dash(inputMove));
        }
        if(Input.GetKeyDown (KeyCode.X))
        {

        }
    }
}
