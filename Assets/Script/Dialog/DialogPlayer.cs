using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPlayer : MonoBehaviour
{
    [SerializeField]
    private DialogSystem[] dialogSystems;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        for(int i=0;i<dialogSystems.Length;++i){
            yield return new WaitUntil(()=> dialogSystems[i].UpdateDialog());
        }
    }
}
