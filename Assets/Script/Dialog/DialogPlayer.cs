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
        yield return new WaitUntil(() => dialogSystems[0].UpdateDialog());

        yield return new WaitUntil(() => dialogSystems[1].UpdateDialog());
    }
}
