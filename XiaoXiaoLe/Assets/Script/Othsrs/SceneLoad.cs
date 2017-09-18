using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour {
    public GameObject obj;
    public GameObject gobj2, gobj3;

	void Awake() {
        gobj2.transform.GetComponent<Button>().enabled = false;
        gobj3.transform.GetComponent<Button>().enabled = false;
        
    }

	IEnumerator Start() {
		for (int i = 0; i < 120; i++) {
            yield return new WaitForEndOfFrame();
        }
        MyGameManager.Instance.ShowInterAD();
        obj.SetActive(true);
        gameObject.SetActive(false);
    }
}
