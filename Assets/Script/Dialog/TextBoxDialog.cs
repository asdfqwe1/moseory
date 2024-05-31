using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextBoxDialog : MonoBehaviour
{
    public DialogSystem dialogSystem;

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag=="Player"){
            StartCoroutine(this.startDialog(coll));
        }
    }
    IEnumerator startDialog(Collider2D coll){
        coll.gameObject.GetComponent<Player_C>().isDialoging=true;
        yield return new WaitUntil(()=>this.dialogSystem.UpdateDialog());
        this.dialogSystem.setIsFirst();
        coll.gameObject.GetComponent<Player_C>().isDialoging=false;

    }
}
