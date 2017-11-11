using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatCubeButton : MonoBehaviour {
    private const int MAXCUBES = 20;
    public GameObject rootCube;
    public CubeMager prefabCube;
    public GameObject top;
    public GameObject cup;
    public CubeEditorWindow m_EditorWindow;
    private Button m_Creat;
    private CubeMager m_Cube;
    
    // Use this for initialization
    void Start () {
        m_Creat = GetComponent<Button>();
        m_Creat.onClick.AddListener(onclick_Creat);
	}
	
    private void onclick_Creat()
    {
        if (CDataMager.getInstance.allCubes.Count > MAXCUBES) return;
        m_Cube = Instantiate(prefabCube, rootCube.transform);
        m_Cube.Init(this);
        CDataMager.getInstance.allCubes.Add(m_Cube);
    }

    void Update()
    {
        
    }
}
