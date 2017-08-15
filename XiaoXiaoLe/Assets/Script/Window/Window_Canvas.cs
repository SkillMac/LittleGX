﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window_Canvas : MonoBehaviour {

    public Text m_Currentscore;
    public Text m_Highscore;
    public Text m_gold;

    int  number;
    string str;

    public int goldScore;

    public GameObject preGold;

    private void Awake()
    {
		EventMgr.AddScoreEvent += OnAddScore;
		EventMgr.AddGoldEvent += OnAddGold;
		m_Highscore.text = FormatNum(PlayerPrefs.GetInt("Highscore"));

        m_gold.text = PlayerPrefs.GetInt("Gold").ToString();
    }
    // Use this for initialization
    void Start () {
        
        
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1))
        {
            PlayerPrefs.SetInt("Gold", 0);
            m_gold.text = PlayerPrefs.GetInt("Gold").ToString();
        }
	}

    public void AddScores(int num)
    {
        StartCoroutine(AddScore(num));

        StartCoroutine(Cut());
    }


    int temp;


    IEnumerator AddScore(int num)
    {
        temp = number;

        number += num;

        Vector3 off = Vector3.one * 0.1f;

        for(int i =0;i < 10; i++)
        {
            temp += num/10;
            
            str = FormatNum(temp);
            
            m_Currentscore.text = str;
            m_Currentscore.transform.localScale += off;

            yield return new WaitForEndOfFrame();
        }

        m_Currentscore.text = FormatNum(number);

        PlayerPrefs.SetInt("CurrentScore", number);
    }

    IEnumerator Cut()
    {
        Vector3 off = Vector3.one * 0.05f;

        for (int i = 0; i < 20; i++)
        {
            str = FormatNum(temp);

            m_Currentscore.text = str;
            m_Currentscore.transform.localScale -= off;

            yield return new WaitForEndOfFrame();
        }
    }
    //根据数字每三位添加一个逗号
    private string FormatNum(int num)
    {
        str = string.Format("{0:N}", num);

        string[] temp = str.Split("."[0]);

        return temp[0];
        
    }
	
	private void OnAddGold() {
		int dex = PlayerPrefs.GetInt("Gold") + 1;
		PlayerPrefs.SetInt("Gold", dex);
		m_gold.text = dex.ToString();
	}

	private void OnAddScore(Vector3 pp) {
		AddScores(goldScore);
		preGold.transform.position = pp + new Vector3(-1.0f, 0, 0);
		preGold.SetActive(true);
	}

    private void OnDestroy() {
		EventMgr.AddScoreEvent -= OnAddScore;
		EventMgr.AddGoldEvent -= OnAddGold;
	}
}
