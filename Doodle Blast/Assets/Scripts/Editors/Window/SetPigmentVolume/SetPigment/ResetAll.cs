using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetAll : MonoBehaviour {
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
        CDataMager.canDraw = true;
        m_Window.m_Mager.m_Draw.enabled = true;
        ResetAllSpheres();
    }

    public void ResetAllSpheres()
    {
        m_Window.m_Mager.ReSetAllLines();
        m_Window.m_Mager.m_Bottle.m_EditorWindow.m_Cup.m_CupMager.m_JudgeMager.ReSetAllData();
    }
}
