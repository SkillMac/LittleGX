using System.Collections;
using UnityEngine;

public class Window_Score : MonoBehaviour {
    
	void OnEnable() {
		StartCoroutine(Close());
    }

	private IEnumerator Close() {
		Vector3 pos = transform.position;
		for (int i = 0; i < 35; i++) {
            pos.y += 0.01f;
            transform.position = pos;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
}
