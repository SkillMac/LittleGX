using UnityEngine;

public class MouseEventMgr : MonoBehaviour {
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			OnMouseDown();
		}
	}

	private void OnMouseDown() {
		Vector3 vec3MousePos = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)) / 100.0f;
		bool bFlag = GameMgr.instance.CheckClickGold(vec3MousePos);
		if (bFlag) {
			return;
		}
		GameMgr.instance.CheckClickShape(vec3MousePos);
	}
}
