using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreat : MonoBehaviour {
    public Transform rootSphere;
    public Transform rootSphereWindow;
    public SphereMager prefabSphere;
    public SphereWindowMager prefabSphereWindow;
    private SphereMager m_Sphere;
    private SphereWindowMager m_SphereWindow;
    private Button creatSphere;

    private List<Vector2> m_Pos = new List<Vector2>();
    private const float OFFSETX = 0.5f;
    private const int MAXSPHERES = 20;

    // Use this for initialization
    void Awake ()
    {
        creatSphere = GetComponent<Button>();
        creatSphere.onClick.AddListener(onclick_CreatSphere);
        CDataMager.getInstance.mySpheres = this;
    }

    private void onclick_CreatSphere()
    {
        if (CDataMager.getInstance.allSpheres.Count > MAXSPHERES) return;
        Vector3 pos = Vector3.zero;
        IsInVector2(ref pos.x);
        pos.y = rootSphere.transform.position.y;
        m_Sphere = Instantiate(prefabSphere, pos,Quaternion.identity);
        m_Sphere.transform.parent = rootSphere;
        m_SphereWindow = Instantiate(prefabSphereWindow, rootSphereWindow);
        m_Sphere.Init(m_SphereWindow,this);
        m_SphereWindow.Init(m_Sphere,this);
        CDataMager.getInstance.allSpheres.Add(m_Sphere);
        CDataMager.getInstance.allSphereWindows.Add(m_SphereWindow);
    }
   
    public void InitColor()
    {
        for(int i =0;i< CDataMager.getInstance.allSpheres.Count;i++)
        {
            CDataMager.getInstance.allSpheres[i].SetColor(Color.white);
            CDataMager.getInstance.allSphereWindows[i].m_SelectSphere.SetColor(Color.white);
        }
    }

    public void InitAllData()
    {
        for (int i = 0; i < CDataMager.getInstance.allSpheres.Count; i++)
        {
            Destroy(CDataMager.getInstance.allSpheres[i].gameObject);
            Destroy(CDataMager.getInstance.allSphereWindows[i].gameObject);
        }
        m_Pos = new List<Vector2>();
    }

    private void IsInVector2(ref float posx)
    {
        if (CDataMager.getInstance.allSpheres.Count == 0)
        {
            m_Pos.Add(new Vector2(- OFFSETX, OFFSETX));
        }
        for (int i = 0; i < m_Pos.Count; i++)
        {
            if (posx + OFFSETX >= m_Pos[i].x && posx + OFFSETX <= m_Pos[i].y)
            {
                if (posx - OFFSETX >= m_Pos[i].x)
                {
                    if (m_Pos[i].y - posx > posx - m_Pos[i].x)
                    {
                        posx = m_Pos[i].x;
                        if (i == 0)
                        {
                            m_Pos[i] = new Vector2(posx - OFFSETX, m_Pos[i].y);
                        }
                        else
                        {
                            if (posx - OFFSETX > m_Pos[i - 1].y)
                            {
                                m_Pos[i] = new Vector2(posx - OFFSETX, m_Pos[i].y);
                            }
                            else
                            {
                                m_Pos[i] = new Vector2(m_Pos[i - 1].x, m_Pos[i].y);
                                m_Pos.Remove(m_Pos[i - 1]);
                            }
                        }
                    }
                    else
                    {
                        posx = m_Pos[i].y;
                        if (i == m_Pos.Count - 1)
                        {
                            m_Pos[i] = new Vector2(m_Pos[i].x, posx + OFFSETX);
                        }
                        else
                        {
                            if (posx + OFFSETX < m_Pos[i + 1].x)
                            {
                                m_Pos[i] = new Vector2(m_Pos[i].x, posx + OFFSETX);
                            }
                            else
                            {
                                m_Pos[i] = new Vector2(m_Pos[i].x, m_Pos[i + 1].y);
                                m_Pos.Remove(m_Pos[i + 1]);
                            }
                        }
                    }
                }
                else
                {
                    if (i > 0)
                    {
                        if (posx - OFFSETX > m_Pos[i - 1].y)
                        {
                            m_Pos[i] = new Vector2(posx - OFFSETX, m_Pos[i].y);
                        }
                        else
                        {
                            m_Pos[i] = new Vector2(m_Pos[i - 1].x, m_Pos[i].y);
                        }
                    }
                    else
                    {
                        m_Pos[i] = new Vector2(posx - OFFSETX, m_Pos[i].y);
                    }
                }
                return;
            }
            else if (posx - OFFSETX >= m_Pos[i].x && posx - OFFSETX <= m_Pos[i].y)
            {
                if (posx + OFFSETX <= m_Pos[i].y)
                {
                    if (m_Pos[i].y - posx > posx - m_Pos[i].x)
                    {
                        posx = m_Pos[i].x;
                        if (i == 0)
                        {
                            m_Pos[i] = new Vector2(posx - OFFSETX, m_Pos[i].y);
                        }
                        else
                        {
                            if (posx - OFFSETX > m_Pos[i - 1].y)
                            {
                                m_Pos[i] = new Vector2(posx - OFFSETX, m_Pos[i].y);
                            }
                            else
                            {
                                m_Pos[i] = new Vector2(m_Pos[i - 1].x, m_Pos[i].y);
                                m_Pos.Remove(m_Pos[i - 1]);
                            }
                        }
                    }
                    else
                    {
                        posx = m_Pos[i].y;
                        if (i == m_Pos.Count - 1)
                        {
                            m_Pos[i] = new Vector2(m_Pos[i].x, posx + OFFSETX);
                        }
                        else
                        {
                            if (posx + OFFSETX < m_Pos[i + 1].x)
                            {
                                m_Pos[i] = new Vector2(m_Pos[i].x, posx + OFFSETX);
                            }
                            else
                            {
                                m_Pos[i] = new Vector2(m_Pos[i].x, m_Pos[i + 1].y);
                                m_Pos.Remove(m_Pos[i + 1]);
                            }
                        }
                    }
                }
                else
                {
                    if (i < m_Pos.Count - 1)
                    {
                        if (posx + OFFSETX < m_Pos[i + 1].x)
                            m_Pos[i] = new Vector2(m_Pos[i].x, posx + OFFSETX);
                        else
                            m_Pos[i] = new Vector2(m_Pos[i].x, m_Pos[i + 1].y);
                    }
                    else
                    {
                        m_Pos[i] = new Vector2(m_Pos[i].x, posx + OFFSETX);
                    }
                    return;
                }

            }
            else if (posx + OFFSETX < m_Pos[i].x)
            {
                if (i == 0)
                {
                    m_Pos.Insert(0, new Vector2(posx - OFFSETX, posx + OFFSETX));
                    return;
                }
                else
                {
                    if (posx - OFFSETX > m_Pos[i - 1].y)
                    {
                        m_Pos.Insert(i - 1, new Vector2(posx - OFFSETX, posx + OFFSETX));
                        return;
                    }
                }
            }
            else if (posx - OFFSETX > m_Pos[i].y)
            {
                if (i == m_Pos.Count - 1)
                {
                    m_Pos.Add(new Vector2(posx - OFFSETX, posx + OFFSETX));
                    return;
                }
                else
                {
                    if (posx + OFFSETX < m_Pos[i + 1].x)
                    {
                        m_Pos.Insert(i + 1, new Vector2(posx - OFFSETX, posx + OFFSETX));
                        return;
                    }
                }
            }
        }
    }
}
