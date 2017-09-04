using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window_Canvas : MonoBehaviour {
    public Text m_Currentscore;
    public Text m_Highscore;
    public Text m_gold;
	public int goldScore;
	public RectTransform _rtGold;
	private int number;

	void Awake() {
		GameMgr.instance.f_windowCanvas = this;
		m_Highscore.text = HummerString.FormatNum(PlayerPrefs.GetInt("Highscore"));
        m_gold.text = PlayerPrefs.GetInt("Gold").ToString();
    }

	void Update() {
		if (Input.GetKeyDown(KeyCode.F1)) {
            PlayerPrefs.SetInt("Gold", 0);
            m_gold.text = PlayerPrefs.GetInt("Gold").ToString();
        }
	}

	public void AddScores(int num) {
        StartCoroutine(AddScore(num));
        StartCoroutine(Cut());
    }
	
	private IEnumerator AddScore(int num) {
		int temp = number;
        number += num;
        Vector3 off = Vector3.one * 0.1f;
		for (int i = 0; i < 10; i++) {
			temp += num / 10;
			m_Currentscore.text = HummerString.FormatNum(temp);
			m_Currentscore.transform.localScale += off;
            yield return new WaitForEndOfFrame();
        }
        m_Currentscore.text = HummerString.FormatNum(number);
        PlayerPrefs.SetInt("CurrentScore", number);
    }

	private IEnumerator Cut() {
        Vector3 off = Vector3.one * 0.05f;
		for (int i = 0; i < 20; i++) {
			m_Currentscore.transform.localScale -= off;
            yield return new WaitForEndOfFrame();
        }
    }

	public void OnAddGold() {
		int dex = PlayerPrefs.GetInt("Gold") + 1;
		PlayerPrefs.SetInt("Gold", dex);
		m_gold.text = dex.ToString();
	}

	public void OnAddScore() {
		AddScores(goldScore);
	}

	public Vector3 GetGoldTargetPos() {
		return new Vector3(_rtGold.position.x, _rtGold.position.y);
	}
}
