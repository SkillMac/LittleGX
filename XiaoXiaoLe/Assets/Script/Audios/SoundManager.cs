using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;
    public AudioClip[] m_Clips;
    private AudioSource m_Audio;

    void Awake(){
        Instance = this;
        m_Audio = transform.GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip){
        m_Audio.PlayOneShot(clip);
    }

    public void SoundOn(){
        m_Audio.mute = false;
    }
    public void SoundOff(){
        m_Audio.mute = true;
    }
    public void ClickPrefabs(){
        PlaySound(m_Clips[0]);
    }
    public void MatchPrefabs(){
        PlaySound(m_Clips[1]);
    }
    public void DMatchPrefabs(){
        PlaySound(m_Clips[2]);
    }
    public void ClearLines(){
        PlaySound(m_Clips[3]);
    }
    public void Lose(){
        PlaySound(m_Clips[4]);
    }
}
