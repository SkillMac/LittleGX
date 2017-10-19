using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteCube : MonoBehaviour {
    private Button m_Button;
    private CubeEditorWindow m_EditorWindow;
    private CubeMager m_Cube;

    public void Init(CubeEditorWindow window)
    {
        m_EditorWindow = window;
        m_Cube = m_EditorWindow.m_Cube;
    }

	// Use this for initialization
	void Start () {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnclickButton);
	}
	
    private void OnclickButton()
    {
        m_Cube = m_EditorWindow.m_Cube;
        if (m_Cube == null) return;
        CDataMager.getInstance.allCubes.Remove(m_Cube);
        Destroy(m_Cube.gameObject);
        m_EditorWindow.gameObject.SetActive(false);
    }
}
