using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Window_Over : MonoBehaviour {
    public Text m_Current;
    public Text m_Hight;
    public Button m_Share;
    public Button m_RePlay;
    private int number = 0, tempnum;
	
    void Awake() {
        int hight;
        if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("Highscore")) {
            hight = PlayerPrefs.GetInt("CurrentScore");
            m_Hight.text = HummerString.FormatNum(hight);
        } else {
            hight = PlayerPrefs.GetInt("Highscore");
            m_Hight.text = HummerString.FormatNum(hight);
        }
        PlayerPrefs.SetInt("Highscore", hight);
        AddScores(PlayerPrefs.GetInt("CurrentScore"));
    }

    void Start() {
        m_Share.onClick.AddListener(ClickShare);
        m_RePlay.onClick.AddListener(ClickPlay);
    }
	
    private void ClickShare() {
        //待处理
    }

	private void ClickPlay() {
        SceneManager.LoadScene("Test");
    }

	private void AddScores(int num) {
        StartCoroutine(AddScore(num));
    }

	private IEnumerator AddScore(int num) {
        number += num;
        for (int i = 0; i < 90; i++) {
            tempnum += num / 90;
            m_Current.text = HummerString.FormatNum(tempnum);
			yield return new WaitForEndOfFrame();
        }
        m_Current.text = HummerString.FormatNum(number);
    }
}
