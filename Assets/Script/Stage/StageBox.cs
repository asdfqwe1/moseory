using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBox : MonoBehaviour
{
    public DialogSystem dialog;
    public bool isFade;
    public float waitTime;
    [SerializeField]
    private int platform_Num;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private bool productEnter = false;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void setNum(int n)
    {
        platform_Num = n;
    }
    public int getNum()
    {
        return platform_Num;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var sm = FindObjectOfType<StageManager>();
            sm.SavePointUpdate(this.platform_Num);
            Debug.Log(platform_Num + "Check");
        }
        if(collision.gameObject.tag == "Player" && !productEnter && dialog)
        {
            productEnter = true;
            StartCoroutine(this.StartDialog(collision));
        }
    }

    private IEnumerator StartDialog(Collider2D collision)
    {     
        collision.gameObject.GetComponent<Player_C>().isDialoging=true;
        yield return new WaitUntil(() => this.dialog.UpdateDialog());
        collision.gameObject.GetComponent<Player_C>().isDialoging=false;
        yield return new WaitForSeconds(waitTime);
        if(isFade) GameManager.Instance.fadeManager.SetTrigger("Fade");
    }
}
