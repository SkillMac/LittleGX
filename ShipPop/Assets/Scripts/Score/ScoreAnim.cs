using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAnim : MonoBehaviour{

    private Text text;
    private int temp, number;

    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    public void InitScore()
    {
        StartCoroutine(InitScoreAnim());
    }
    public void AddScores(int num)
    {
        StartCoroutine(AddScore(num));
        StartCoroutine(Cut());
    }
    
    private IEnumerator AddScore(int num)
    {
        number = int.Parse(text.text);
        temp = number;
        number += num;
        Vector3 off = Vector3.one * 0.1f;
        for (int i = 0; i < 3; i++)
        {
            temp += num/3 ;
            string str = FormatNum(temp);
            text.text = str;
            transform.localScale += off;
            yield return new WaitForEndOfFrame();
        }
        text.text = FormatNum(number);
    }

    private IEnumerator InitScoreAnim()
    {
        number = int.Parse(text.text);
        temp = number;
        number = 0;
        for (int i = 0; i < 10; i++)
        {
            temp -= temp / 10;
            string str = FormatNum(temp);
            text.text = str;
            yield return new WaitForEndOfFrame();
        }
        text.text = "0";
    }

    private IEnumerator Cut()
    {
        Vector3 off = Vector3.one * 0.05f;
        for (int i = 0; i < 6; i++)
        {
            string str = FormatNum(temp);
            text.text = str;
            transform.localScale -= off;
            yield return new WaitForEndOfFrame();
        }
    }
    //根据数字每三位添加一个逗号
    private string FormatNum(int num)
    {
        string str = string.Format("{0:N}", num);
        string[] temp = str.Split("."[0]);
        return temp[0];
    }
}
