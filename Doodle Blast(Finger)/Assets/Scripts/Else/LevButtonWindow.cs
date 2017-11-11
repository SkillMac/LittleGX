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
    public Transform m_Stars;

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

    public void SetStarsNum(int num)
    {
        if (num == 0) m_Stars.gameObject.SetActive(false);
        for (int i = 0; i < num; i++)
        {
            m_Stars.GetChild(i).gameObject.SetActive(true);
        }
    }
}
