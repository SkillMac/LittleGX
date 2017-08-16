using UnityEngine;

public class SunRotate : MonoBehaviour {
	
	void Update() {
        transform.Rotate(new Vector3(0, 0, 30));
	}
}
