﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HideEditorWindow : MonoBehaviour {
    public GameObject obj;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }
    void OnMouseDown()
    {
        obj.SetActive(false);
    }
  
}
