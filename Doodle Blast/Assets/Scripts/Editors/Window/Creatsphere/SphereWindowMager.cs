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
    public ImageType m_Type;
    public ButtonCreat m_CreatMager;
    public SphereMager m_Sphere;
    public AllSphereType m_SpheresType;

    public void Init(SphereMager sphere, ButtonCreat creat,AllSphereType type)
    {
        m_Sphere = sphere;
        m_CreatMager = creat;
        m_SpheresType = type;
    }

    void Awake()
    {
        m_SelectSphere.Init(this);
        m_bigger.Init(this);
        m_smaller.Init(this);
        m_deleter.Init(this);
        m_Type.Init(this);
    }
}
