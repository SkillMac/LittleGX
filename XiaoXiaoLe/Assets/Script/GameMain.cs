using UnityEngine;

public class GameMain : MonoBehaviour {

	void Awake() {
		ConfigMgr.LoadConfig();
	}
}
