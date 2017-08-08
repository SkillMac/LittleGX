using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remora : MonoBehaviour {

    public GameObject obj;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        obj.GetComponent<DrawLine>().enabled = false;
    }
    private void OnMouseExit()
    {
        obj.GetComponent<DrawLine>().enabled = true;
    }

}
