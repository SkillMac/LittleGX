using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidthBigger : MonoBehaviour {
    private const int MAXSIZE = 20;
    private CubeEditorWindow m_EditorWindow;
    private Button m_Button;
    private Text m_Width;
    private CubeMager m_Cube;

    public void Init(CubeEditorWindow window)
    {
        m_EditorWindow = window;
        m_Width = window.m_Width;
        m_Cube = window.m_Cube;
    }

    public void InitCube()
    {
        m_Cube = m_EditorWindow.m_Cube;
    }

	// Use this for initialization
	void Start () {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnclickButton);
    }
	
    private void OnclickButton()
    {
        InitCube();
        if (m_Cube == null) return;
        Vector3 scale = m_Cube.transform.localScale;
        int size = m_Cube.myWidthSize;
        if (size < MAXSIZE)
        {
            size += 1;
        }
        else
            size = MAXSIZE;
        scale.x = size * 0.1f;
        m_Cube.myWidthSize = size;
        m_Cube.transform.localScale = scale;
        m_Width.text = scale.x.ToString();
        m_Cube.ChangePosAsScaleOrRotation();
    }
}
