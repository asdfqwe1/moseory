using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private float width, height;
    private Vector2 midPoint;
    [SerializeField]
    private TextMeshProUGUI text;
    [Space]
    public RectTransform t_Mountain;
    private float t_Mountain_Max;
    public RectTransform t_Trees;
    private float t_Trees_Max;
    public RectTransform t_Player;
    private float t_Player_Max;
    public RectTransform t_Logo;
    private Vector2[] positions = new Vector2[4];
    void Awake(){
        width=Screen.width;
        height=Screen.height;
        midPoint=new Vector2(width/2,height/2);

        t_Mountain.sizeDelta=new Vector2(width,height);
        t_Trees.sizeDelta=new Vector2(width,height);
        t_Player.sizeDelta=new Vector2(width,height);
        t_Logo.sizeDelta=new Vector2(width,height);
        t_Logo.position=new Vector2(width*0.4f,height*t_Logo.pivot.y);

        positions[0]=t_Mountain.position;
        positions[1]=t_Trees.position;
        positions[2]=t_Player.position;
        positions[3]=t_Logo.position;

        t_Mountain_Max=(height*(t_Mountain.localScale.x-1f))/2f;
        t_Trees_Max=(height*(t_Trees.localScale.x-1f))/2f;
        t_Player_Max=(height*(t_Player.localScale.x-1f))/2f;
    }
    void Start(){
        Debug.Log(width+"*"+height);
        Debug.Log(t_Mountain_Max+" "+t_Trees_Max+" "+t_Player_Max);
        //Mouse Lock
        Cursor.lockState=CursorLockMode.Confined;
        //AudioManager.Instance.PlaySound("Title Sound",0,true,SoundType.BGM);
    }

    void Update(){
        Vector2 mouse = ((Vector2)Input.mousePosition-midPoint);
        Vector2 mouse_Per = new Vector2(((-mouse.x)/(width/2)),((-mouse.y)/(height/2)));
        //float mouseY_Per=((-mouse.y)/(height/2));
        //float mouseX_per=((-mouse.x)/(width/2));
        MoveIMG(mouse, mouse_Per);
    }

    void MoveIMG(Vector2 mouse,Vector2 p){
        text.text=(mouse.x +" : "+ mouse.y+"\n"+p+"%");
        t_Mountain.position=Vector2.Lerp(t_Mountain.position,new Vector2(positions[0].x+(t_Mountain_Max*p.x),positions[0].y+(t_Mountain_Max*p.y)),Time.deltaTime);
        t_Trees.position=Vector2.Lerp(t_Trees.position,new Vector2(positions[0].x+(t_Trees_Max*p.x),positions[0].y+(t_Trees_Max*p.y)),Time.deltaTime);
        t_Player.position=Vector2.Lerp(t_Player.position,new Vector2(positions[0].x+(t_Player_Max*p.x),positions[0].y+(t_Player_Max*p.y)),Time.deltaTime);
    }

    public void GameStart(){
        AudioManager.Instance.PlaySound("Game Start");
        GameManager.Instance.NextScene(2);
    }
}
