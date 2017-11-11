using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereType : MonoBehaviour {
    public Text m_SphereName;
    public Button m_Type;
    private Sprite m_Sprite;
    private AllSphereType m_AllSphere;
	// Use this for initialization
	void Start () {
        m_Sprite = m_Type.transform.GetComponent<Image>().sprite;
        m_Type.onClick.AddListener(OnclickButton);
	}
	
    private void OnclickButton()
    {
        m_AllSphere.m_Mager.SetButtonImage(m_Sprite);
    }

    public void Init(AllSphereType m_AllSphere)
    {
        this.m_AllSphere = m_AllSphere;
    }

    public void SetButtonSprite(Sprite spr)
    {
        m_Sprite = spr;
        m_Type.transform.GetComponent<Image>().sprite = spr;
    }
}
