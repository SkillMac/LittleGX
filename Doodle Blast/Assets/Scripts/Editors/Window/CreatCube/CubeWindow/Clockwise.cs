using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clockwise : MonoBehaviour {
    private CubeEditorWindow m_EditorWindow;
    private Button m_Button;
    private Text m_Rotate;
    private CubeMager m_Cube;

    public void Init(CubeEditorWindow window)
    {
        m_EditorWindow = window;
        m_Rotate = window.m_Rotate;
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
        m_Cube.transform.Rotate(new Vector3(0, 0, -5));
        int rotation = m_Cube.myRotation;
        if (rotation == -175)
            rotation = -(rotation - 5);
        else
        rotation -= 5;
        m_Cube.myRotation = rotation;
        m_Rotate.text = m_Cube.myRotation.ToString();
        m_Cube.ChangePosAsScaleOrRotation();
    }
}
