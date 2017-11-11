using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSphere : MonoBehaviour {
    public GameObject m_CreatWindow;
    public AllSphereType m_AllType;
    private Button m_Sphere;
    private Window_Editor m_EditorWindow;
    public void Init(Window_Editor editor)
    {
        m_EditorWindow = editor;
    }
    // Use this for initialization
    void Start () {
        m_CreatWindow.SetActive(false);
        m_AllType.gameObject.SetActive(false);
        m_Sphere = GetComponent<Button>();
        m_Sphere.onClick.AddListener(onclick_Sphere);
    }
    private void onclick_Sphere()
    {
        if (!m_EditorWindow.mask.activeSelf)
        {
            m_CreatWindow.SetActive(true);
            m_EditorWindow.mask.transform.SetAsLastSibling();
            m_Sphere.transform.SetAsLastSibling();
            m_EditorWindow.mask.SetActive(true);
            m_AllType.gameObject.SetActive(false);

            if (CDataMager.getInstance.allSpheres.Count >0)
            {
                for(int i =0;i < CDataMager.getInstance.allSpheres.Count;i++)
                {
                    CDataMager.getInstance.allSpheres[i].SetInitCollider(true);
                    CDataMager.getInstance.allSphereWindows[i].enabled = true;
                }
            }
        }
        else
        {
            m_CreatWindow.SetActive(false);
            m_EditorWindow.mask.SetActive(false);
            m_EditorWindow.mask.transform.SetAsFirstSibling();
            m_AllType.gameObject.SetActive(false);

            if (CDataMager.getInstance.allSpheres.Count > 0)
            {
                for (int i = 0; i < CDataMager.getInstance.allSpheres.Count; i++)
                {
                    CDataMager.getInstance.allSpheres[i].SetColor(Color.white);
                    CDataMager.getInstance.allSpheres[i].SetInitCollider(false);
                    CDataMager.getInstance.allSphereWindows[i].enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
