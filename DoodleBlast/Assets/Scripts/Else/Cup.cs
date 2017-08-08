using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour {

    public static Cup Instance;
    
    //杯子内容器的四个顶点坐标
    public Vector3[] AllVers;
    //赢得最低分数线
    public Vector3 LineWin;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        
    }
    
	// Update is called once per frame
	void Update () {
        
        
    }
    
    //void OnGUI()
    //{
    //    GUILayout.Label("鼠标的x轴" + Input.mousePosition.x);
    //    GUILayout.Label("鼠标的y轴" + Input.mousePosition.y);
    //}

    List<float> m_Len;

    public float IsWin(Transform trans)
    {
        m_Len = new List<float>();

        if (AllVers == null || LineWin == null || trans == null) return 0;

        if(AllVers.Length == 4)
        {
            for(int i =0; i< trans.childCount;i++)
            {
                if(trans.GetChild(i).gameObject.activeSelf)
                {
                    if (trans.GetChild(i).position.x >= AllVers[0].x && trans.GetChild(i).position.x <= AllVers[1].x && trans.GetChild(i).position.y + 0.2f >= AllVers[2].y && trans.GetChild(i).position.y + 0.2f <= AllVers[0].y)
                    {
                        float off = trans.GetChild(i).lossyScale.x * trans.GetChild(i).GetComponent<CircleCollider2D>().radius;

                        m_Len.Add( trans.GetChild(i).position.y + off);
                    }
                }
            }
            //全都是负数，所以最大的是最后一个
            m_Len.Sort();

            if(m_Len.Count>0)
                return m_Len[m_Len.Count - 1];
        }

        

        return 0;
    }

    public bool Istrue(Transform trans)
    {
        if(IsWin(trans) != 0)
        {
            float dis = IsWin(trans);

            if(dis>= LineWin.y)
            {
                return true;
            }
        }

        return false;
    }

   

}
