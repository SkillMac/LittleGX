using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartMyGame : MonoBehaviour {
	
	public void ReStart() {
        GameMgr.instance.ReStartGame();
    }
}
