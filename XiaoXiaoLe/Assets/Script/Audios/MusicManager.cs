using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance;
    public AudioClip m_Clicps;
    private AudioSource m_Audio;

    void Awake(){
        Instance = this;
        m_Audio = transform.GetComponent<AudioSource>();
    }
	
    public void MusicOn(){
        m_Audio.mute = false;
    }
    public void MusicOff(){
        m_Audio.mute = true;
    }
}
