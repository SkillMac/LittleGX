using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevButtonWindow : MonoBehaviour {
    public LevButtonScript m_LevButton;
    public ButtonDeleteLev m_Delete;
    public Text m_Text;
    public GameObject m_Lock;
    public int m_Count;
    public LoadAllEditorLevs m_Window;

    void Awake()
    {
        m_Delete.gameObject.SetActive(false);
        m_Delete.Init(this);
        m_LevButton.Init(this);
        m_Lock.SetActive(false);
    }

    public void Init(LoadAllEditorLevs m_Window)
    {
        this.m_Window = m_Window;
    }
}
