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
        if (WindowUIMager.hasStartGame) return;
        WindowUIMager.hasStartGame = true;
        m_UIMager.SetRestartButton(false);
        CDataMager.canDraw = false;
        m_UIMager.m_Effect.PlayAnimation();
    }
}
