using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smaller : MonoBehaviour {
    private Button m_smaller;
    private SphereWindowMager m_window;
    public void Init(SphereWindowMager window)
    {
        m_window = window;
    }
    // Use this for initialization
    void Start () {
        m_smaller = GetComponent<Button>();
        m_smaller.onClick.AddListener(onclick_smaller);
	}
	
    private void onclick_smaller()
    {
        m_window.m_CreatMager.InitColor();
        Vector2 scale = m_window.m_Sphere.transform.localScale;
        if (scale.x >= 0.9f)
            scale -= Vector2.one * 0.1f;
        else
            scale = Vector2.one * 0.8f;
        m_window.m_Sphere.transform.localScale = scale;
        m_window.m_Scale.text = scale.x.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
