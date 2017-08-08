using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadscene : MonoBehaviour {

    public string sceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene()
    {
        if(sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadCurrentScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        DrawLine.m_Lenght = 0;
        Window_canvas.current_Lenght = 0;
    }
}
