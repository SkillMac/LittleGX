using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Window_Win : MonoBehaviour {
    public Button m_next;
    public GameObject midStar;
    public GameObject rightStar;
    private int current;
    
    // Use this for initialization
    void Start () {
        m_next.onClick.AddListener(OnClickNext);
	}

    void OnEnable()
    {
        int temp =  PlayerPrefs.GetInt("CurrentFixLev");
        int max = PlayerPrefs.GetInt("MaxFixLev");
        if(temp >= max)
        {
            if (max < CAllFixedLev.GetInstance.allFixedLevs.Count)
                max = temp + 1;
            else
                max = CAllFixedLev.GetInstance.allFixedLevs.Count;
        }
        PlayerPrefs.SetInt("MaxFixLev", max);
    }

    private void OnClickNext()
    {
        if(!CAllFixedLev.isFixedLev)
        {
            current = PlayerPrefs.GetInt("CurrentLev");
            int index = CAllEditorLevs.GetInstance.allLevID.BinarySearch(current);
            if (index < CAllEditorLevs.GetInstance.allLevID.Count - 1)
            {
                PlayerPrefs.SetInt("CurrentLev", CAllEditorLevs.GetInstance.allLevID[index + 1]);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
                SceneManager.LoadScene("Begin");
        }
        else
        {
            current = PlayerPrefs.GetInt("CurrentFixLev");
            int index = CAllFixedLev.GetInstance.allFixedLevs.Count;
            if(current < index)
            {
                PlayerPrefs.SetInt("CurrentFixLev", current + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
                SceneManager.LoadScene("Begin");
        }
    }
}
