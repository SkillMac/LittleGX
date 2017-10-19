using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBack : MonoBehaviour {
    private Button m_Button;
    private SaveData m_Winodw;

    public void Init(SaveData m_Winodw)
    {
        this.m_Winodw = m_Winodw;
    }
	// Use this for initialization
	void Start () {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(ClickButton);
	}
	
    private void ClickButton()
    {
        m_Winodw.OnUnable();
        m_Winodw.m_Window.gameObject.SetActive(true);
        m_Winodw.m_Window.m_Mager.m_Bottle.transform.SetAsLastSibling();
    }
}
