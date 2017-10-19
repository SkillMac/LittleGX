using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPigment : MonoBehaviour {
    private Image m_Pigment;
	// Use this for initialization
	void Start () {
        m_Pigment = GetComponent<Image>();
    }
	
    public void SetImagePigment(float value)
    {
        value = Mathf.Clamp01(value);
        m_Pigment.fillAmount = value;
    }
}
