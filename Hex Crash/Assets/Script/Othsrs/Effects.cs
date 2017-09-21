using UnityEngine;

public class Effects : MonoBehaviour {
	float timer;
	
	void OnEnable() {
		timer = 0f;
	}

	void Update() {
		timer += Time.deltaTime;
		if (timer > 1.0f) {
			gameObject.SetActive(false);
		}
    }
}
