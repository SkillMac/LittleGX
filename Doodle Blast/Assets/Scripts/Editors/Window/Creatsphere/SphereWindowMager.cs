using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereWindowMager : MonoBehaviour {
    public SelectObj m_SelectSphere;
    public Bigger m_bigger;
    public Smaller m_smaller;
    public Deleter m_deleter;
    public Text m_Scale;
    public Button m_Type;
    public ButtonCreat m_CreatMager;
    public SphereMager m_Sphere;

    public void Init(SphereMager sphere, ButtonCreat creat)
    {
        m_Sphere = sphere;
        m_CreatMager = creat;
    }

    void Awake()
    {
        m_SelectSphere.Init(this);
        m_bigger.Init(this);
        m_smaller.Init(this);
        m_deleter.Init(this);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
