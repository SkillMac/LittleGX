using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour {
    public GameObject obj;
    public GameObject gobj2, gobj3;

    void OnEnable()
    {
        Init(false);
        StartCoroutine(Waitmoment());
    }
    
	IEnumerator Waitmoment() {
		for (int i = 0; i < 120; i++) {
            yield return new WaitForEndOfFrame();
        }
        MyGameManager.Instance.ShowInterAD();
        obj.SetActive(true);
        Init(true);
        gameObject.SetActive(false);
    }

    public void Init(bool isabled)
    {
        gobj2.transform.GetComponent<Button>().enabled = isabled;
        gobj3.transform.GetComponent<Button>().enabled = isabled;
    }
}
