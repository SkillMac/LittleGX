using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeleteBeforeLine : MonoBehaviour
{
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
        m_Window.m_Mager.m_Draw.DeleteBeforeLine();
    }
}
