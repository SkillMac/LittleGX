using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bigger : MonoBehaviour {
    private Button bigger;
    private SphereWindowMager m_window;
    public void Init(SphereWindowMager window)
    {
        m_window = window;
    }
	// Use this for initialization
	void Start () {
        bigger = GetComponent<Button>();
        bigger.onClick.AddListener(onclick_Bigger);
	}
	
    private void onclick_Bigger()
    {
        m_window.m_CreatMager.InitColor();
        Vector2 scale = m_window.m_Sphere.transform.localScale;
        if (scale.x <= 2.9f)
        {
            scale += Vector2.one * 0.1f;
            m_window.m_Sphere.ChangePositionAsScale(0.1f);
        }
        else
            scale = Vector2.one * 3.0f;
        m_window.m_Sphere.transform.localScale = scale;
        m_window.m_Scale.text = scale.x.ToString();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
