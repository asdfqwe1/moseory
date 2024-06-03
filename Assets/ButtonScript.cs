using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Vector2 pos;
    public Vector2 plus;

    public void interact()
    {
        GameObject.FindObjectOfType<Player_C>().transform.position = pos+plus;
    }
}
