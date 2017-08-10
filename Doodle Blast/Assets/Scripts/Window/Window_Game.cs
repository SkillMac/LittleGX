using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Game : MonoBehaviour {

    public GameObject[] AllLevPrefabs;

    private void Awake()
    {
        if (AllLevPrefabs.Length == 0) return;

        int currentLev = PlayerPrefs.GetInt("CurrentLev");

        if (AllLevPrefabs.Length < currentLev) return;

        Instantiate(AllLevPrefabs[currentLev]);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
