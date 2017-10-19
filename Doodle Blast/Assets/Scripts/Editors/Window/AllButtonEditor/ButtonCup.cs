using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCup : MonoBehaviour
{
    public CupMager m_CupMager;
    private Button m_Cup;
    private Window_Editor m_EditorWindow;
    public void Init(Window_Editor editor)
    {
        m_EditorWindow = editor;
    }
    // Use this for initialization
    void Start () {
        m_Cup = GetComponent<Button>();
        m_Cup.onClick.AddListener(onclick_Cup);
    }
    private void onclick_Cup()
    {
        if (!m_EditorWindow.mask.activeSelf)
        {
            m_EditorWindow.mask.transform.SetAsLastSibling();
            m_Cup.transform.SetAsLastSibling();
            m_EditorWindow.mask.SetActive(true);
            m_CupMager.currentCup.SetInitCollider(true);
            m_CupMager.currentCupWinLine.SetInitCollider(true);
        }
        else
        {
            m_EditorWindow.mask.SetActive(false);
            m_EditorWindow.mask.transform.SetAsFirstSibling();
            m_CupMager.currentCup.SetInitCollider(false);
            m_CupMager.currentCupWinLine.SetInitCollider(false);
        }
    }
}
