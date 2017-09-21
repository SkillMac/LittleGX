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

    void OnEnable()
    {
        Init();
    }
  
    void Start() {
        m_Share.onClick.AddListener(ClickShare);
        m_RePlay.onClick.AddListener(ClickPlay);
    }
	
    private void ClickShare() {
        //待处理
    }

	private void ClickPlay() {
        gameObject.SetActive(false);
        GameMgr.instance.ReStartGame();
    }

	private void AddScores(int num) {
        StartCoroutine(AddScore(num));
    }

	private IEnumerator AddScore(int num) {
        number += num;
        for (int i = 0; i < 60; i++) {
            tempnum += num / 60;
            m_Current.text = HummerString.FormatNum(tempnum);
			yield return new WaitForEndOfFrame();
        }
        m_Current.text = HummerString.FormatNum(number);
    }

    private void Init()
    {
        number = 0;
        tempnum = 0;
        int hight;
        if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("Highscore"))
        {
            hight = PlayerPrefs.GetInt("CurrentScore");
            m_Hight.text = HummerString.FormatNum(hight);
        }
        else
        {
            hight = PlayerPrefs.GetInt("Highscore");
            m_Hight.text = HummerString.FormatNum(hight);
        }
        PlayerPrefs.SetInt("Highscore", hight);
        AddScores(PlayerPrefs.GetInt("CurrentScore"));
    }
}
