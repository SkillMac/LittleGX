using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deleter : MonoBehaviour {
    private Button m_deleter;
    private SphereWindowMager m_window;
    public void Init(SphereWindowMager window)
    {
        m_window = window;
    }
    // Use this for initialization
    void Start () {
        m_deleter = GetComponent<Button>();
        m_deleter.onClick.AddListener(onclick_delete);
	}
	
    private void onclick_delete()
    {
        CDataMager.getInstance.allSpheres.Remove(m_window.m_Sphere);
        CDataMager.getInstance.allSphereWindows.Remove(m_window);
        Destroy(m_window.m_Sphere.gameObject);
        Destroy(m_window.gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
