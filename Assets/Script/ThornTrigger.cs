using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ThornTrigger : MonoBehaviour
{
    private TilemapCollider2D tilemapCollider2D;
    void Start() {
        tilemapCollider2D=GetComponent<TilemapCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag=="Player"){
            var Player=coll.gameObject.GetComponent<Player_C>();
            Player.KillSwitch();
        }
    }
}
