using UnityEngine;
using UnityEngine.UI;

public class ButtonBottle : MonoBehaviour{
    private Button m_Bottle;
    [HideInInspector]
    public Window_Editor m_EditorWindow;
    private PigmentMager m_Mager;

    public void Init(Window_Editor editor)
    {
        m_EditorWindow = editor;
    }
    // Use this for initialization
    void Start () {
        m_Bottle = GetComponent<Button>();
        m_Bottle.onClick.AddListener(onclick_Bottle);
        m_Mager = GetComponent<PigmentMager>();
        m_Mager.m_Window.gameObject.SetActive(false);
    }

    private void onclick_Bottle()
    {
        if (!m_EditorWindow.mask.activeSelf)
        {
            CDataMager.canDraw = true;
            m_EditorWindow.mask.transform.SetAsLastSibling();
            m_Bottle.transform.SetAsLastSibling();
            m_EditorWindow.mask.SetActive(true);
            m_Mager.m_Window.gameObject.SetActive(true);
        }
        else
        {
            CDataMager.canDraw = false;
            m_EditorWindow.mask.SetActive(false);
            m_EditorWindow.mask.transform.SetAsFirstSibling();
            m_Mager.m_Window.gameObject.SetActive(false);
            m_Mager.m_Window.m_Reset.ResetAllSpheres();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
