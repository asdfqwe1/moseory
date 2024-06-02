using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
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
    [SerializeField]
    private float typingSpeed=0.1f;
    [SerializeField]
    private bool isTypingEffect=false;
    [Space]
    [SerializeField]
    private bool isPlayMusic;
    [SerializeField]
    private SoundType soundType;
    [SerializeField]
    private bool isLoop;
    [SerializeField]
    private string musicName;

    private void Awake()
    {
        Setup();
    }

    private void Setup(){
        currentSpeakerIndex=0;
        currentDialogIndex=-1;
        for(int i=0;i<speakers.Length;++i){
            SetActiveObjects(speakers[i],false);
            try {speakers[i].CharacterRenderer.gameObject.SetActive(true);} catch (NullReferenceException ex) {Debug.Log(ex);}
        }
    }
    public bool UpdateDialog(){
        if(isFirst==true){
            Setup();
            if(isAutoStart) SetNextDialog();
            isFirst=false;
        }
        if(Input.GetMouseButtonDown(0)||Input.anyKeyDown){
            if(isTypingEffect==true){
                isTypingEffect=false;
                StopCoroutine("OnTypingText");
                speakers[currentSpeakerIndex].textDialog.text=dialogs[currentDialogIndex].dialog;
                try{speakers[currentSpeakerIndex].objectArrow.gameObject.SetActive(true);}catch(NullReferenceException ex){ Debug.Log(ex); }
                return false;
            }
            if(dialogs.Length>currentDialogIndex+1){
                SetNextDialog();
            }
            else{
                for(int i=0;i<speakers.Length;++i){
                    SetActiveObjects(speakers[i],false);
                    try{speakers[i].CharacterRenderer.gameObject.SetActive(false);}catch(NullReferenceException ex){Debug.Log(ex);}
                }
                if(isPlayMusic){
                    AudioManager.Instance.PlaySound(musicName,0,isLoop,soundType);
                    isPlayMusic=false;
                }
                return true;
            }
        }
        return false;
    }
    private void SetNextDialog(){
        SetActiveObjects(speakers[currentSpeakerIndex],false);
        currentDialogIndex++;
        
        currentSpeakerIndex=dialogs[currentDialogIndex].SpeakerIndex;
        SetActiveObjects(speakers[currentSpeakerIndex],true);
        try{
        speakers[currentSpeakerIndex].textName.text=dialogs[currentDialogIndex].name;
        speakers[currentSpeakerIndex].textDialog.text=dialogs[currentDialogIndex].dialog;
        }
        catch (NullReferenceException ex) {Debug.Log(ex);}
        StartCoroutine("OnTypingText");
        
        Debug.Log(dialogs[currentDialogIndex].name+": "+dialogs[currentDialogIndex].dialog);
    }
    private void SetActiveObjects(SpeakerInfo speaker, bool visible){
        
        try {speaker.imageDialog.gameObject.SetActive(visible);} catch (NullReferenceException ex) {Debug.Log(ex);}
        try {speaker.textName.gameObject.SetActive(visible);} catch (NullReferenceException ex) {Debug.Log(ex);}
        try {speaker.textDialog.gameObject.SetActive(visible);} catch (NullReferenceException ex) {Debug.Log(ex);}

        try {speaker.objectArrow.gameObject.SetActive(false);} catch (NullReferenceException ex) {Debug.Log(ex);}

        try{
        Color color=speaker.CharacterRenderer.color;
        color.a=visible==true?1:0f;
        speaker.CharacterRenderer.color=color;
        }
        catch (NullReferenceException ex) {Debug.Log(ex);}

        Debug.Log("setActive Obj: " + visible);
    }
    private IEnumerator OnTypingText(){
        int index=0;
        isTypingEffect=true;
        while(index<=dialogs[currentDialogIndex].dialog.Length){
            speakers[currentSpeakerIndex].textDialog.text=dialogs[currentDialogIndex].dialog.Substring(0,index);
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTypingEffect=false;
        try {speakers[currentSpeakerIndex].objectArrow.gameObject.SetActive(true);} catch (NullReferenceException ex) { Debug.Log(ex); }
    }
    public void setIsFirst(){
        isFirst=true;
    }
}

[System.Serializable]
public struct SpeakerInfo{
    public Image CharacterRenderer;
    public Image imageDialog;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialog;
    public Image objectArrow;
}
[System.Serializable]
public struct DialogInfo{
    public int SpeakerIndex;
    public string name;
    [TextArea(3,5)]
    public string dialog;
}