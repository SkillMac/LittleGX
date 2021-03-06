﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginTest : MonoBehaviour {
    private Button m_Button;
    private PigmentWindow m_Window;

    public void Init(PigmentWindow window)
    {
        m_Window = window;
    }

    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(Onclickbutton);
    }

    private void Onclickbutton()
    {
        if (m_Window.m_Mager.m_Bottle.m_EditorWindow.m_Cup.m_CupMager.m_JudgeMager.enabled) return;
        m_Window.gameObject.SetActive(false);
        CDataMager.canDraw = false;
        for (int i = 0; i < CDataMager.getInstance.allSpheres.Count; i++)
        {
            CDataMager.getInstance.allSpheres[i].SetRigidbody2D(RigidbodyType2D.Dynamic);
            CDataMager.getInstance.allSpheres[i].GetComponent<WhetherSphereStatic>().enabled = true;
        }
        m_Window.m_Mager.m_Bottle.m_EditorWindow.m_Cup.m_CupMager.m_JudgeMager.enabled = true;
        m_Window.m_Mager.m_Bottle.m_EditorWindow.m_Cup.m_CupMager.m_JudgeMager.InitData(CDataMager.getInstance.allSpheres, CDataMager.getInstance.winLinePositionY);
    }
}
