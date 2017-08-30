using UnityEngine;

public class AniScore : MonoBehaviour {
    
	void OnAnimationEnd() {
		gameObject.SetActive(false);
	}
}
