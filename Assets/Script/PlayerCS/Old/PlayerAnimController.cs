using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private Movement2D movement2D;
    [SerializeField]
    private Dash_Skill Dash;
    // Start is called before the first frame update
    void Start()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
