using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CheatUI : MonoBehaviour
{
    [SerializeField]
    private List<Vector2Int> list;
    [SerializeField]
    private GameObject Cheat_UI;
    private bool toggle=false;
    public GameObject contentOBJ;
    public GameObject buttonPre;
    private void Start()
    {
        if (!GameManager.Instance.stageManager) return;
        list = GameManager.Instance.stageManager.stageSavePoint;

        foreach (var item in list)
        {
            var obj = Instantiate(buttonPre, contentOBJ.transform);
            obj.GetComponent<ButtonScript>().pos = item;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = item.ToString();
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (toggle)
            {
                toggle = false;
                Cheat_UI.SetActive(toggle);
            }
            else if(!toggle)
            {
                toggle = true;
                Cheat_UI.SetActive(toggle);
            }
        }
    }
}
