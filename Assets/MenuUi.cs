using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUi : MonoBehaviour
{
    public GameObject menuUI; // 메뉴 UI 오브젝트

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        menuUI.SetActive(true);
    }

}
