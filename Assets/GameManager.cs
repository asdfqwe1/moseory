using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public StageManager stageManager;
    public FadeManager fadeManager;

    public static GameManager Instance{
        get{
            if(!_instance){
                _instance=FindObjectOfType(typeof(GameManager))as GameManager;

                if(_instance==null){
                    Debug.Log("No Singleton OBJ");
                }
            }
            return _instance;
        }
    }

    private void Awake(){
        if(_instance==null){
            _instance=this;
        }
        else if(_instance!=this){
            Destroy(gameObject);
        }

        if(!stageManager)stageManager=GameObject.FindObjectOfType<StageManager>();
        if(!fadeManager)fadeManager=GameManager.FindObjectOfType<FadeManager>();
    }

    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}