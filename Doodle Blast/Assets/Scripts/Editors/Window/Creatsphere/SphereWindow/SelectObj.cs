using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObj : MonoBehaviour
{
    private Button select;
    private SphereWindowMager m_window;
    public void Init(SphereWindowMager window)
    {
        m_window = window;
    }

    // Use this for initialization
    void Start () {
        select = GetComponent<Button>();
        select.onClick.AddListener(onclick_Select);
    }
    private void onclick_Select()
    {
        m_window.m_CreatMager.InitColor();
        m_window.m_Sphere.SetColor(Color.black);
        SetColor(Color.red);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void SetColor(Color color)
    {
        Image image = GetComponent<Image>();
        image.color = color;
    }
}
