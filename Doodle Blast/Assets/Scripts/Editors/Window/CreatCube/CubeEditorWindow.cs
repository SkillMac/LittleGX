using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeEditorWindow : MonoBehaviour {
    public Text m_Width;
    public Text m_Height;
    public Text m_Rotate;
    public WidthBigger m_WidthBigger;
    public WidthSmaller m_WidthSmaller;
    public HeightBigger m_HeightBigger;
    public HeightSmaller m_HeightSmaller;
    public Clockwise m_Clockwise;
    public Anticlockwise m_Anticlockwise;
    public DeleteCube m_Delete;
    [HideInInspector]
    public CubeMager m_Cube;
    public void Init(CubeMager cube)
    {
        m_Cube = cube;
    }
	// Use this for initialization
	void Start () {
        m_WidthBigger.Init(this);
        m_WidthSmaller.Init(this);
        m_HeightBigger.Init(this);
        m_HeightSmaller.Init(this);
        m_Clockwise.Init(this);
        m_Anticlockwise.Init(this);
        m_Delete.Init(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
