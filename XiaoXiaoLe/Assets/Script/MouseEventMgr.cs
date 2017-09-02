using UnityEngine;

public class MouseEventMgr : MonoBehaviour {
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			OnMouseDown();
		}
	}

	private void OnMouseDown() {
		Vector3 vec3MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vec3MousePos.z = 0;
		bool bFlag = GameMgr.instance.CheckClickGold(vec3MousePos);
		if (bFlag) {
			return;
		}
		GameMgr.instance.CheckClickShape(vec3MousePos);
	}
}
