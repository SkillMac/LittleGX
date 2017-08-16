using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour {
    public GameObject obj;
    public GameObject gobj1, gobj2, gobj3;

	void Awake() {
        gobj1.transform.GetComponent<Prefabs>().enabled = false;
        gobj2.transform.GetComponent<Button>().enabled = false;
        gobj3.transform.GetComponent<Button>().enabled = false;
		for (int i = 0; i < gobj1.transform.childCount; i++) {
			if (gobj1.transform.GetChild(i).GetComponent<TestDraw>().enabled == true) {
                gobj1.transform.GetChild(i).GetComponent<TestDraw>().ReturnStart();
                gobj1.transform.GetChild(i).GetComponent<TestDraw>().enabled = false;
            }
        }
    }

	IEnumerator Start() {
		for (int i = 0; i < 120; i++) {
            yield return new WaitForEndOfFrame();
        }
        obj.SetActive(true);
        gameObject.SetActive(false);
    }
}
