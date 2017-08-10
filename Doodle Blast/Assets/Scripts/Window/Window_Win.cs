using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Win : MonoBehaviour {
    
    private Button m_next;

    int current;

    int MaxLev = 9;

    private void Awake()
    {
        current = PlayerPrefs.GetInt("CurrentLev");
        int maxLev = PlayerPrefs.GetInt("Level");

        if(current==maxLev)
        {
            if(maxLev < MaxLev)
                PlayerPrefs.SetInt("Level", maxLev + 1);
            else
                PlayerPrefs.SetInt("Level", maxLev);
        }

        m_next = transform.GetChild(2).GetComponent<Button>();

    }
    // Use this for initialization
    void Start () {

        m_next.onClick.AddListener(OnClickNext);
	}
	
    void OnClickNext()
    {
        if(current < MaxLev)
        {
            PlayerPrefs.SetInt("CurrentLev", current + 1);
            Loadscene.Instance.LoadCurrentScene();
        }
        else
            Loadscene.Instance.LoadScene("Begin");

        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
