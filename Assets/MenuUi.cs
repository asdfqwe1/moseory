using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUi : MonoBehaviour
{
    public GameObject menuUI; // �޴� UI ������Ʈ

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
