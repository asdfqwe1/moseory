using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private Animator fade;
    void Start(){
        fade=this.GetComponent<Animator>();
    }

    public void SetTrigger(string tg){
        fade.SetTrigger(tg);
    }
    public void SetAnimSpeed(string tg, float f){
        fade.SetFloat(tg,(60f/(f*60f)));
    }
}
