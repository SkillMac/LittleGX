﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, 0, 30));
	}
}
