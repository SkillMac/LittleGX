using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCube : MonoBehaviour{
    public CreatCubeButton m_CreatButton;
    public CubeEditorWindow m_CubeEditorWindow;
    //public HideEditorWindow m_Back;
    private Button m_Cube;
    private Window_Editor m_EditorWindow;
    public void Init(Window_Editor editor)
    {
        m_EditorWindow = editor;
        //m_Back.gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        m_Cube = GetComponent<Button>();
        m_Cube.onClick.AddListener(onclick_Cube);
	}

    private void onclick_Cube()
    {
        if (!m_EditorWindow.mask.activeSelf)
        {
            m_EditorWindow.mask.transform.SetAsLastSibling();
            m_Cube.transform.SetAsLastSibling();
            m_EditorWindow.mask.SetActive(true);
            m_CreatButton.gameObject.SetActive(true);
            m_CubeEditorWindow.gameObject.SetActive(true);
            //m_Back.gameObject.SetActive(true);

            if (CDataMager.getInstance.allCubes.Count >0)
            {
                for(int i =0; i<CDataMager.getInstance.allCubes.Count;i++)
                {
                    CDataMager.getInstance.allCubes[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        else
        {
            m_EditorWindow.mask.SetActive(false);
            m_EditorWindow.mask.transform.SetAsFirstSibling();
            m_CreatButton.gameObject.SetActive(false);
            m_CubeEditorWindow.gameObject.SetActive(false);
            //m_Back.gameObject.SetActive(false);

            if (CDataMager.getInstance.allCubes.Count > 0)
            {
                for (int i = 0; i < CDataMager.getInstance.allCubes.Count; i++)
                {
                    CDataMager.getInstance.allCubes[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
}
