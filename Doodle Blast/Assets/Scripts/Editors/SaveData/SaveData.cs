using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour {
    public ButtonBack m_Back;
    public ButtonSave m_Save;
    public PigmentWindow m_Window;
    public GameObject m_Success;

    void OnEnable()
    {
        m_Window.m_Mager.m_Bottle.m_EditorWindow.mask.transform.SetAsLastSibling();
    }

    void Start()
    {
        m_Back.Init(this);
        m_Save.Init(this);
    }

    public void Init(PigmentWindow m_Window)
    {
        this.m_Window = m_Window;
    }

    public void OnUnable()
    {
        gameObject.SetActive(false);
        CDataMager.canDraw = true;
        m_Window.m_Mager.m_Draw.enabled = true;
    }
}
