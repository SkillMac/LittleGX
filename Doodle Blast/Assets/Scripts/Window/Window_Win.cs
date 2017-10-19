using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Window_Win : MonoBehaviour {
    public Button m_next;
    private int current;
    
    void OnEnable()
    {
        current = PlayerPrefs.GetInt("CurrentLev");
    }
    
    // Use this for initialization
    void Start () {
        m_next.onClick.AddListener(OnClickNext);
	}
	
    private void OnClickNext()
    {
        int index = CAllEditorLevs.GetInstance.allLevID.BinarySearch(current);
        if (index < CAllEditorLevs.GetInstance.allLevID.Count - 1)
        {
            PlayerPrefs.SetInt("CurrentLev", CAllEditorLevs.GetInstance.allLevID[index + 1]);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
            SceneManager.LoadScene("Begin");
    }
	
}
