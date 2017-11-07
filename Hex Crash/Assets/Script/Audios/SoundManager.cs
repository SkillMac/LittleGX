using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;
    public AudioClip[] m_DeleteVoice;
    public AudioClip[] m_Clips;
    public AudioClip[] m_Cheers;
    public GameObject m_GameOver;
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
    public void Lose(bool isActive){
        m_GameOver.SetActive(isActive);
    }
    public void ClickCoin() {
        PlaySound(m_Clips[3]);
    }
    public void HighScore()
    {
        PlaySound(m_Clips[4]);
    }
    public void Ding()
    {
        PlaySound(m_Clips[5]);
    }

    public float GetAudioClipLength()
    {
        return m_Clips[4].length;
    }

    public void ClearLines(int index)
    {
        if (index > m_DeleteVoice.Length) return;
        PlaySound(m_DeleteVoice[index]);
    }

    public void Cheers(int index)
    {
        if (index > m_Cheers.Length) return;
        PlaySound(m_Cheers[index]);
    }
}
