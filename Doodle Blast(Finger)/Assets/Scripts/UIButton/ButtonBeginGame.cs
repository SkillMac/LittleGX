using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBeginGame : MonoBehaviour {
    private Button m_Button;
    private WindowUIMager m_UIMager;
    // Use this for initialization
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickButton);
    }

    public void Init(WindowUIMager m_UIMager)
    {
        this.m_UIMager = m_UIMager;
    }

    private void OnClickButton()
    {
        gameObject.SetActive(false);
        m_UIMager.m_ObjMager.transform.localScale = Vector3.one;
        m_UIMager.m_ObjMager.transform.localPosition = Vector3.zero;
        m_UIMager.SetRestartButton(false);
        CDataMager.canDraw = false;
        m_UIMager.SetEffect(true);
        m_UIMager.m_Effect.PlayAnimation();
    }
}
