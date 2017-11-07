using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMusic : MonoBehaviour {
    public AudioClip gameOverMenu;
    private AudioSource m_Audio;

    void Awake()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        StartCoroutine(PlayMenu(m_Audio.clip.length-1));
    }

    private IEnumerator PlayMenu(float length)
    {
        yield return new WaitForSeconds(length);
        m_Audio.PlayOneShot(gameOverMenu);
    }
}
