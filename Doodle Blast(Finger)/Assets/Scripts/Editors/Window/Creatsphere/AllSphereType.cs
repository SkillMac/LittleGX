using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSphereType : MonoBehaviour {
    public GameObject rootType;
    public SphereType prefabType;
    public SphereMager m_Mager;
    private SphereType m_CurrentType;
    
    void Start()
    {
        CreatSpheretype();
    }
    private void CreatSpheretype()
    {
        Sprite[] temp = CDataMager.getInstance.allSphereSprite;
        for (int i = 0; i < temp.Length;i++)
        {
            m_CurrentType = Instantiate(prefabType, rootType.transform);
            m_CurrentType.Init(this);
            m_CurrentType.m_SphereName.text = temp[i].name;
            m_CurrentType.SetButtonSprite(temp[i]);
        }
    }

    public void Init(SphereMager m_Mager)
    {
        this.m_Mager = m_Mager;
    }

}
