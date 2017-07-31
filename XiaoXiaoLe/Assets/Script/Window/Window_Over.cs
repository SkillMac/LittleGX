using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Window_Over : MonoBehaviour {

    public Text m_Current;
    public Text m_Hight;

    public Button m_Share;
    public Button m_RePlay;

    int number , tempnum;

    string str;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("Highscore"))
        {
            m_Hight.text = (PlayerPrefs.GetInt("CurrentScore")).ToString();
        }
        else
        {
            m_Hight.text = PlayerPrefs.GetInt("Highscore").ToString();
        }

        PlayerPrefs.SetInt("Highscore", int.Parse(m_Hight.text));

        AddScores(PlayerPrefs.GetInt("CurrentScore"));
    }
    // Use this for initialization
    void Start () {

        m_Share.onClick.AddListener(ClickShare);
        m_RePlay.onClick.AddListener(ClickPlay);
        
    }
	
    void ClickShare()
    {
        //待处理
    }

    void ClickPlay()
    {
        SceneManager.LoadScene("Test");
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScores(int num)
    {
        StartCoroutine(AddScore(num));
    }

    IEnumerator AddScore(int num)
    {
        number += num;

        for (int i = 0; i < 90; i++)
        {
            tempnum += num / 90;

            str = FormatNum(tempnum);

            m_Current.text = str;

            yield return new WaitForEndOfFrame();
        }

        m_Current.text = number.ToString();
    }
    //根据数字每三位添加一个逗号
    private string FormatNum(int num)
    {
        str = string.Format("{0:N}", num);

        string[] temp = str.Split("."[0]);

        return temp[0];
    }

    
}
