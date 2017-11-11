using UnityEngine;
using UnityEngine.UI;

public class ImageType : MonoBehaviour {
    private Button m_Type;
    private SphereWindowMager m_window;

    // Use this for initialization
    void Start () {
        m_Type = GetComponent<Button>();
        m_Type.onClick.AddListener(OnclickButton);
	}

    public void SetColor(Color color)
    {
        transform.GetComponent<Image>().color = color;
    }

    private void OnclickButton()
    {
        if(m_window.m_CreatMager.m_CurrentWindow != null)
            m_window.m_CreatMager.m_CurrentWindow.m_Type.SetColor(Color.white);
        SetColor(Color.red);
        m_window.m_CreatMager.m_CurrentWindow = m_window;
        m_window.m_SpheresType.gameObject.SetActive(true);
        m_window.m_SpheresType.Init(m_window.m_Sphere);
    }

    public void Init(SphereWindowMager window)
    {
        m_window = window;
    }
}
