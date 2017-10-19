using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFileData : MonoBehaviour {
    public JudgeCanWin m_Cup;
    public DrawLines m_Draw;
    public GameObject m_CubePrefab;
    public Transform m_CubeRoot;
    public GameObject m_SpherePrefab;
    public Transform m_SphereRoot;
    private List<Transform> allSpheres = new List<Transform>();

    void Start()
    {
        LoadDataCreatLev();
    }
   
    private void SetCup(CCup cup)
    {
        m_Cup.transform.position = cup.position;
        m_Cup.winLine.transform.position = cup.winStandard;
    }

    private void SetBottleValue(CPigment pigment)
    {
        m_Draw.maxPigmentLength = pigment.memorySize;
    }

    private void SetCubes(CBarrier[] cubes)
    {
        if (cubes.Length == 0) return;
        for(int i =0;i<cubes.Length;i++)
        {
            Vector3 pos = cubes[i].position;
            Vector3 scal = cubes[i].scale;
            Quaternion rotation = cubes[i].rotation;
            GameObject obj = Instantiate(m_CubePrefab, pos, rotation, m_CubeRoot);
            obj.transform.localScale = scal;
        }
    }

    private void SetSpheres(CSphere[] spheres)
    {
        if (spheres.Length == 0) return;
        for (int i = 0; i < spheres.Length; i++)
        {
            Vector3 pos = spheres[i].position;
            Vector3 scal = spheres[i].scale;
            GameObject obj = Instantiate(m_SpherePrefab, m_SphereRoot);
            obj.transform.position = pos;
            obj.transform.localScale = scal;
            allSpheres.Add(obj.transform);
        }
    }

    private void LoadDataCreatLev()
    {
        CLevel myLev = CAllEditorLevs.GetInstance.allEditorLevs[PlayerPrefs.GetInt("CurrentLev")];
        SetCup(myLev.m_cup);
        SetBottleValue(myLev.m_pigment);
        SetCubes(myLev.m_barriers);
        SetSpheres(myLev.m_spheres);
        m_Cup.InitData(allSpheres);
    }

    public void SetAllSpheresType()
    {
        for (int i = 0;i<allSpheres.Count;i++)
        {
            allSpheres[i].GetComponent<WhetherSphereStatic>().SetRigidbody2D(RigidbodyType2D.Dynamic);
            allSpheres[i].GetComponent<WhetherSphereStatic>().enabled = true;
        }
    }
}
