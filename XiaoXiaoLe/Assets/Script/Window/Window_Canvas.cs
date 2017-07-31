using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Canvas : MonoBehaviour ,IMessageHandler{

    public Text m_Currentscore;
    public Text m_Highscore;
    public Text m_gold;

    int  number;
    string str;

    public int goldScore;

    public GameObject preGold;

    private void Awake()
    {
        MessageCenter.Registed(this.GetHashCode(), this);
        
        m_Highscore.text = PlayerPrefs.GetInt("Highscore").ToString();

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
    }


    int temp;


    IEnumerator AddScore(int num)
    {
        temp = number;

        number += num;

        for(int i =0;i < 10;i++)
        {
            temp += num/10;

            str = FormatNum(temp);
            
            m_Currentscore.text = str;

            yield return new WaitForEndOfFrame();
        }

        m_Currentscore.text = FormatNum(number);

        PlayerPrefs.SetInt("CurrentScore", number);
    }

    //根据数字每三位添加一个逗号
    private string FormatNum(int num)
    {
        str = string.Format("{0:N}", num);

        string[] temp = str.Split("."[0]);

        return temp[0];

        //if (num < 0)
        //{
        //    return "";
        //}

        //string str = "";
        //string strnum;


        //while (num > 0)
        //{

        //    int numA = num % 1000;
        //    num /= 1000;

        //    strnum = numA.ToString();

        //    string temp = strnum;

        //    if (numA == 0) temp = strnum = "000";
        //    if (strnum.Length != 3)
        //    {
        //        for (int i = 0; i < 3 - strnum.Length; i++)
        //        {
        //            temp = "0" + strnum;
        //        }
        //    }

        //    if (str == "")
        //    {
        //        str = temp;
        //    }
        //    else
        //    {

        //        str = temp + "," + str;
        //    }
        //}
        //return str;
    }

    public void MassageHandler(uint type, object data)
    {
        if(type == MessageType.UI_ADDGOLD)
        {
            int dex = PlayerPrefs.GetInt("Gold") + 1;

            PlayerPrefs.SetInt("Gold",  dex);

            m_gold.text = dex.ToString();
            
        }

        if(type == MessageType.UI_ADDSCORE)
        {
            if(data is Vector3)
            {
                Vector3 pp = (Vector3)data;

                AddScores(goldScore);

                preGold.transform.position = pp + new Vector3(-1.0f, 0, 0);

                preGold.SetActive(true);
            }
           
        }
    }

    private void OnDestroy()
    {
        MessageCenter.Cancel(this.GetHashCode());
    }
}
