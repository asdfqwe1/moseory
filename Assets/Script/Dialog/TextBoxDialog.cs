using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextBoxDialog : MonoBehaviour
{
    public DialogSystem dialogSystem;
    private bool isPlay;
    public bool isAlways=true;
    public bool isLast=false;
    public float waitTime=1f;

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag=="Player"){
            if(!isAlways&&isPlay) return;
            StartCoroutine(this.startDialog(coll));
        }
    }
    IEnumerator startDialog(Collider2D coll){
        coll.gameObject.GetComponent<Player_C>().isDialoging=true;
        if(dialogSystem){
            yield return new WaitUntil(()=>this.dialogSystem.UpdateDialog());
            this.dialogSystem.setIsFirst();
            coll.gameObject.GetComponent<Player_C>().isDialoging=false;}
        this.isPlay=true;
        if(isLast) {
            GameManager.Instance.fadeManager.SetTrigger("Fade");
            yield return new WaitForSeconds(waitTime);
            GameManager.Instance.NextScene();}
    }
}
