using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType{
    BGM,EFFECT,PLAYER
}

public class AudioManager : SingleTon<AudioManager>
{
    [SerializeField]
    private AudioMixer mAudioMixer;

    private float mCurrentBGMVolume, mCurrentEffectVolume;
    private List<TemporarySoundPlayer> mInstantiateSouds;

    private Dictionary<string, AudioClip> mClipsDictionary;
    [SerializeField]
    private AudioClip[] mPreloadClips;

    void Start(){
        mClipsDictionary=new Dictionary<string, AudioClip>();
        foreach(AudioClip clip in mPreloadClips){
            mClipsDictionary.Add(clip.name,clip);
        }
        mInstantiateSouds=new List<TemporarySoundPlayer>();
    }
    private AudioClip GetClip(string clipName){
        AudioClip clip=mClipsDictionary[clipName];
        if(clip==null)Debug.LogError(clipName+" is not having");
        return clip;
    }
    private void AddToList(TemporarySoundPlayer soundPlayer){
        mInstantiateSouds.Add(soundPlayer);
    }
    public void StopLoopSound(string clipName){
        foreach(TemporarySoundPlayer audioPlayer in mInstantiateSouds){
            if(audioPlayer.ClipName==clipName){
                mInstantiateSouds.Remove(audioPlayer);
                Destroy(audioPlayer.gameObject);
                return;
            }
        }
    }
    public bool FindPlayingSound(string clipName){
        foreach(TemporarySoundPlayer audioPlayer in mInstantiateSouds){
            if(audioPlayer.ClipName==clipName){
                Debug.Log("playing that sound!");
                return true;
            }
        }
        return false;
    }
    public void PlaySound(string clipName, float delay=0f,bool isLoop=false,SoundType type=SoundType.EFFECT){
        GameObject obj=new GameObject("TemporarySoundPlayer 2D");
        TemporarySoundPlayer soundPlayer=obj.AddComponent<TemporarySoundPlayer>();

        if(isLoop) AddToList(soundPlayer);

        soundPlayer.InitSound(GetClip(clipName));
        soundPlayer.Play(mAudioMixer.FindMatchingGroups(type.ToString())[0],delay,isLoop);
    }
    public static string Range(int from,int includedTo,bool isStartZero=false){
        if(includedTo>100&&isStartZero) Debug.LogError("not surport 3line");
        int value=UnityEngine.Random.Range(from,includedTo+1);
        return value<10&&isStartZero?'0'+value.ToString():value.ToString();
    }
}
