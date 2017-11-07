using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window_Over : MonoBehaviour {
    public Text m_Current;
    public Text m_Hight;
    public Button m_Share;
    public Button m_RePlay;
    private int number, tempnum;
    private bool isBegin = false;
    private float m_Speed;
    private float audioTimer;

    void OnEnable()
    {
        Init();
    }
  
    void Awake()
    {
        audioTimer = SoundManager.Instance.GetAudioClipLength();
        m_Share.onClick.AddListener(ClickShare);
        m_RePlay.onClick.AddListener(ClickPlay);
    }

    void Update()
    {
        if(isBegin)
        {
            tempnum += (int)(m_Speed * Time.deltaTime);
            m_Current.text = HummerString.FormatNum(tempnum);
        }
    }

    private void ClickShare() {
        //待处理
    }

	private void ClickPlay()
    {
        SoundManager.Instance.Lose(false);
        gameObject.SetActive(false);
        GameMgr.instance.ReStartGame();
        MusicManager.Instance.PlayMusic();
    }

	private void AddScores() {
        StartCoroutine(AddScore());
    }

	private IEnumerator AddScore() {
        SoundManager.Instance.HighScore();
        yield return new WaitForSeconds(audioTimer);
        isBegin = false;
        SoundManager.Instance.Ding();
        m_Current.text = HummerString.FormatNum(number);
    }

    private void Init()
    {
        tempnum = 0;
        int hightScore;
        int tempScore = PlayerPrefs.GetInt("CurrentScore");
        int tempHightScore = PlayerPrefs.GetInt("Highscore");
        number = tempScore;
        if (tempScore > tempHightScore)
        {
            hightScore = tempScore;
            m_Hight.text = HummerString.FormatNum(hightScore);
        }
        else
        {
            hightScore = tempHightScore;
            m_Hight.text = HummerString.FormatNum(hightScore);
        }
        PlayerPrefs.SetInt("Highscore", hightScore);
        m_Speed = tempScore / audioTimer;
        isBegin = true;
        AddScores();
    }
}
