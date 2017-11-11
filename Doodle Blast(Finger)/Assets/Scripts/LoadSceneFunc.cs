using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFunc : MonoBehaviour {
    public bool isPlayAwake;
    public string sceneName;

    void Start()
    {
        if (isPlayAwake)
            LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
	
}
