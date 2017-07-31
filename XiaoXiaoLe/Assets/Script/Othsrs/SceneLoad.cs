using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour {

    public GameObject obj;

	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Start()
    {
        for (int i = 0; i < 120; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        obj.SetActive(true);
        gameObject.SetActive(false);
    }
}
