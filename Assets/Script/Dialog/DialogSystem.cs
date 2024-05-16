using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private SpeakerInfo[] speakers;
    [SerializeField]
    private DialogInfo[] dialogs;
    [SerializeField]
    private bool isAutoStart;
    [SerializeField]
    private bool isFirst;
    private int currentDialogIndex=-1;
    private int currentSpeakerIndex=0;

    private void Awake()
    {
        Setup();
    }

    private void Setup(){
        for(int i=0;i<speakers.Length;++i){/*
            SetActiveObjects(speakers[i],false);
            speakers[i].CharacterRenderer.gameObject.SetActive(true);*/
        }
    }
    public bool UpdateDialog(){
        if(isFirst==true){
            Setup();
            if(isAutoStart) SetNextDialog();
            isFirst=false;
        }
        if(Input.GetMouseButtonDown(0)||Input.anyKeyDown){
            if(dialogs.Length>currentDialogIndex+1){
                SetNextDialog();
            }
            else{
                for(int i=0;i<speakers.Length;++i){/*
                    SetActiveObjects(speakers[i],false);
                    speakers[i].CharacterRenderer.gameObject.SetActive(false);*/
                }
            }
        }
        return false;
    }
    private void SetNextDialog(){
        //SetActiveObjects(speakers[currentSpeakerIndex],false);
        currentDialogIndex++;
        /*
        currentSpeakerIndex=dialogs[currentDialogIndex].SpeakerIndex;
        SetActiveObjects(speakers[currentSpeakerIndex],true);
        speakers[currentSpeakerIndex].textName.text=dialogs[currentDialogIndex].name;
        speakers[currentSpeakerIndex].textDialog.text=dialogs[currentDialogIndex].dialog;
        */
        Debug.Log(dialogs[currentDialogIndex].name+": "+dialogs[currentDialogIndex].dialog);
    }
    private void SetActiveObjects(SpeakerInfo speaker, bool visible){
        /*
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialog.gameObject.SetActive(visible);

        speaker.objectArrow.SetActive(false);

        Color color=speaker.CharacterRenderer.color;
        color.a=visible==true?1:0.2f;
        speaker.CharacterRenderer.color=color;
        */
    }
}

[System.Serializable]
public struct SpeakerInfo{
    public SpriteRenderer CharacterRenderer;
    public Image imageDialog;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialog;
    public GameObject objectArrow;
}
[System.Serializable]
public struct DialogInfo{
    public int SpeakerIndex;
    public string name;
    [TextArea(3,5)]
    public string dialog;
}