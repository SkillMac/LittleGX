using UnityEngine;
using UnityEngine.UI;

public class ButtonReturnState : MonoBehaviour {
    private Button m_Button;
    public WindowUIMager m_UIMager;
    // Use this for initialization
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        m_UIMager.InitData();
    }

    public void Init(WindowUIMager m_UIMager)
    {
        this.m_UIMager = m_UIMager;
    }
}
