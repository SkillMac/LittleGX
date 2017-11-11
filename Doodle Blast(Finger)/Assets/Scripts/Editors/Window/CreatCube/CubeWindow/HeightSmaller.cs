using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightSmaller : MonoBehaviour {
    private const int MINSIZE = 9;
    private CubeEditorWindow m_EditorWindow;
    private Button m_Button;
    private Text m_Height;
    private CubeMager m_Cube;

    public void Init(CubeEditorWindow window)
    {
        m_EditorWindow = window;
        m_Height = window.m_Height;
        m_Cube = window.m_Cube;
    }

    public void InitCube()
    {
        m_Cube = m_EditorWindow.m_Cube;
    }

    // Use this for initialization
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnclickButton);
    }

    private void OnclickButton()
    {
        InitCube();
        if (m_Cube == null) return;
        Vector3 scale = m_Cube.transform.localScale;
        int size = m_Cube.myHeightsize;
        if (size > MINSIZE)
        {
            size -= 1;
        }
        else
            size = MINSIZE;
        scale.y = size * 0.1f;
        m_Cube.myHeightsize = size;
        m_Cube.transform.localScale = scale;
        m_Height.text = scale.y.ToString();
    }
}
