using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour,IMessageHandler {
    
    public int xDim, yDim;

    public float offsetx, offsetY;

    public GameObject prefabBG;

    public int[] datas;

    public static List<Transform> AllElement;//所有元素的集合

    private List<Transform> OldElement = new List<Transform>();

    private object old;
    private  int index;

    public Transform[,] ArraryEle;

    public Transform[] window_score;

    public Window_Canvas m_canvas;

    public GameObject m_gold;

    public GameObject m_Effect;

    private List<Transform> m_Delete;
    
    private void Awake()
    {
        MessageCenter.Registed(this.GetHashCode(), this);

        //Vector3 transpos = new Vector3()
        AllElement = new List<Transform>();
    }


    // Use this for initialization
    void Start () {

        Tables mapTab  = DataManager.tables[TableName.maps];

        xDim = mapTab.GetLineData();
        yDim = mapTab.GetLenghtData();

        ArraryEle = new Transform[xDim, yDim];

        //根据列表实例化出来棋盘
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                if (int.Parse( mapTab.datas[x, y] ) != 0)
                {
                    GameObject background = Instantiate(prefabBG, GetWorldPos(x, y), Quaternion.identity);
                    background.transform.parent = transform;
                    AllElement.Add(background.transform);

                    background.transform.GetComponent<Element>().Color = ElementType.Empty;

                    background.GetComponent<Element>().GetPosition = new Vector2(x, y);

                    ArraryEle[x, y] = background.transform;

                    GameObject effect = Instantiate(m_Effect, GetWorldPos(x, y), Quaternion.identity);
                    effect.transform.parent = background.transform;

                    effect.SetActive(false);

                }
                else
                {
                    ArraryEle[x, y] = null;
                }
            }
        }
    }
    
	// Update is called once per frame
	void Update () {
        
    }
    //可以删除左边的列，返回可以删除的该行的元素
    public Transform CanDeletLeft(Transform target)
    {
        int i = 0;
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        for (int aa = 0; aa < xDim; aa++)
        {
            i = (int)(pos.y - pos.x) + aa;

            if(i>=0&&i<yDim)
            {
                if (ArraryEle[aa, i] != null)
                {
                    if (ArraryEle[aa, i].GetComponent<Element>().Color == ElementType.Empty)
                        return null;
                }
            }
            
        }
        return target;
    }
    //可以删除右边的列，返回可以删除的该行的元素
    public Transform CanDeletRight(Transform target)
    {
        int i = 0;
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        for (int aa = 0; aa < xDim; aa++)
        {
            i = (int)(pos.y + pos.x) - aa;

            if (i >= 0 && i < yDim)
            {
                if (ArraryEle[aa, i] != null)
                {
                    if (ArraryEle[aa, i].GetComponent<Element>().Color == ElementType.Empty)
                        return null;
                }
            }
        }
        return target;
    }
    //可以删除该行的列，返回可以删除的该行的元素
    public Transform CanDeletLine(Transform target)
    {
        Vector2 pos = target.GetComponent<Element>().GetPosition;

        for (int aa = 0; aa < yDim; aa++)
        {
            if (ArraryEle[(int)pos.x, aa] != null)
            {
                if (ArraryEle[(int)pos.x, aa].GetComponent<Element>().Color == ElementType.Empty)
                    return null;
            }
        }
        return target;
    }

    //删除该行的列，把该行的元素改为空的
    public void DeleLine(Transform target,float count,int cc)
    {
        Vector2 pos = target.GetComponent<Element>().GetPosition;

        int dex = 0;

        List<Transform> CanDelete = new List<Transform>();

        for (int aa = 0; aa < yDim; aa++)
        {
            if (ArraryEle[(int)pos.x, aa] != null)
            {
                ArraryEle[(int)pos.x, aa].GetComponent<Element>().dex = count + 1;
                ArraryEle[(int)pos.x, aa].GetComponent<Element>().IsEmpty = true;

                ArraryEle[(int)pos.x, aa].GetChild(1).gameObject.SetActive(true);
                ArraryEle[(int)pos.x, aa].GetChild(1).GetComponent<Effects>().dex = count;

                if(!CanDelete.Contains(ArraryEle[(int)pos.x, aa]))
                    CanDelete.Add(ArraryEle[(int)pos.x, aa]);

                dex++;
            }
        }
        
        

        if(OldElement.Count >0)
        {
            for(int i =0;i< OldElement.Count;i++)
            {
                for(int j =0;j<CanDelete.Count;j++)
                {
                    if(OldElement[i] == CanDelete[j] && OldElement[i] != target && !m_Delete.Contains(OldElement[i]))
                    {
                        m_Delete.Add(OldElement[i]);
                    }
                }
            }
        }
        GetScoreWithNum(dex, target.position, cc, count);
    }
    //删除该行的列，把该行的元素改为空的
    public void DeleLeft(Transform target, float count,int cc)
    {
        Vector2 pos = target.GetComponent<Element>().GetPosition;

        int ii = 0;
        int dex = 0;
        List<Transform> CanDelete = new List<Transform>();

        for (int aa = 0; aa < xDim; aa++)
        {
            ii = (int)(pos.y - pos.x) + aa;

            if (ii >= 0 && ii < yDim)
            {
                if (ArraryEle[aa, ii] != null)
                {
                    ArraryEle[aa, ii].GetComponent<Element>().dex = count + 1;
                    ArraryEle[aa, ii].GetComponent<Element>().IsEmpty = true;

                    ArraryEle[aa, ii].GetChild(1).gameObject.SetActive(true);
                    ArraryEle[aa, ii].GetChild(1).GetComponent<Effects>().dex = count;

                    if(!CanDelete.Contains(ArraryEle[aa, ii]))
                        CanDelete.Add(ArraryEle[aa, ii]);
                    dex++;
                }
            }
        }

        if (OldElement.Count > 0)
        {
            for (int i = 0; i < OldElement.Count; i++)
            {
                for (int j = 0; j < CanDelete.Count; j++)
                {
                    if (OldElement[i] == CanDelete[j] && OldElement[i] != target && !m_Delete.Contains(OldElement[i]))
                    {
                        m_Delete.Add(OldElement[i]);
                    }
                }
            }
        }

        GetScoreWithNum(dex, target.position, cc, count);
    }
    //删除该行的列，把该行的元素改为空的
    public void DeleRight(Transform target, float count,int cc)
    {
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        int ii = 0;
        int dex = 0;

        List<Transform> CanDelete = new List<Transform>();

        for (int aa = 0; aa < xDim; aa++)
        {
            ii = (int)(pos.y + pos.x) - aa;
            if (ii >= 0 && ii < yDim)
            {
                if (ArraryEle[aa, ii] != null)
                {
                    ArraryEle[aa, ii].GetComponent<Element>().dex = count+1;
                    ArraryEle[aa, ii].GetComponent<Element>().IsEmpty = true;

                    ArraryEle[aa, ii].GetChild(1).gameObject.SetActive(true);
                    ArraryEle[aa, ii].GetChild(1).GetComponent<Effects>().dex = count;

                    if (!CanDelete.Contains(ArraryEle[aa, ii]))
                        CanDelete.Add(ArraryEle[aa, ii]);
                    dex++;
                }
            }
        }

        if (OldElement.Count > 0)
        {
            for (int i = 0; i < OldElement.Count; i++)
            {
                for (int j = 0; j < CanDelete.Count; j++)
                {
                    if (OldElement[i] == CanDelete[j] && OldElement[i] != target && !m_Delete.Contains(OldElement[i]))
                    {
                        m_Delete.Add(OldElement[i]);
                    }
                }
            }
        }

        GetScoreWithNum(dex, target.position, cc, count);
    }


    Vector2  GetWorldPos(int x,int y)
    {
        return new Vector2( (y - (yDim-1)/ 2.0f)*offsetx  + transform.position.x,
             ((xDim-1) / 2.0f - x)* offsetY + transform.position.y) ;
    }

    public void MassageHandler(uint type, object data)
    {
        if (type == MessageType.MOUSE_DOWN)
        {
            if (data is Transform[])
            {
                if (data == old) return;

                Transform[] tran = (Transform[])data;
                if (tran == null) return;

                Vector3[] pos = new Vector3[tran.Length];

                ElementType currenttype;
                
                currenttype = tran[0].GetComponent<Element>().Color ;

                for (int i = 0; i < tran.Length; i++)
                {
                    pos[i] = tran[i].position;
                }

                for (int i = 0; i < OldElement.Count; i++)
                {
                    if (OldElement.Count == 0) return;
                    OldElement[i].GetComponent<Element>().Color = ElementType.Empty;
                }

                Transform[] current = ComPos(pos);

                if (current != null && IsVer(current,currenttype))
                {
                    OldElement.Clear();
                    for (int i = 0; i < current.Length; i++)
                    {
                        current[i].GetComponent<Element>().Color = currenttype +1;

                        old = data;
                        OldElement.Add(current[i]);
                    }
                }
                else
                {
                    //2017.7.26 9:00不添加拖动一直出
                    OldElement.Clear();
                }
            }
        }

        if (type == MessageType.MOUSE_UP)
        {

            int index_Line = 0;
            int index_Left = 0;
            int index_Right = 0;

            if (data is Transform[])
            {
                Transform[] tran = (Transform[])data;
                if (tran[0] == null) return;

                ElementType currenttype;

                currenttype = tran[0].GetComponent<Element>().Color;
                
                Vector3[] poss = new Vector3[tran.Length];
                
                for (int i = 0; i < tran.Length; i++)
                {
                    poss[i] = tran[i].position;
                }
                
                Transform[] current = ComPos(poss);

                if (current == null || !IsVer(current, currenttype))
                {
                    tran[0].parent.GetComponent<TestDraw>().ReturnStart();

                    OldElement.Clear();
                    return;
                }
                else if(current !=null && IsVer(current, currenttype))
                {
                    
                    Destroy(tran[0].parent.gameObject);

                    MessageCenter.ReceiveMassage(new MessageData(MessageType.MOUSE_UP_CREAT, tran[0].parent));

                    index++;


                    MessageCenter.ReceiveMassage(new MessageData(MessageType.MOUSE_UP_CREAT, index));
                }

                m_Delete = new List<Transform>();

                for (int i = 0; i < OldElement.Count; i++)
                {
                    if (OldElement.Count == 0) return;

                    OldElement[i].GetComponent<Element>().Color -= 1;
                    
                    Vector2 pos = OldElement[i].GetComponent<Element>().GetPosition;
                    
                    if (CanDeletLine(OldElement[i]) != null && CanDeletLeft(OldElement[i]) == null && CanDeletRight(OldElement[i]) == null)
                    {
                        if (index_Line == 0 )
                        {
                            DeleLine(OldElement[i], 0,1);
                        }
                        
                        if (index_Line == 1)
                        {
                            DeleLine(OldElement[i], 1.0f,2);
                        }
                        else if (index_Line == 2)
                        {
                            DeleLine(OldElement[i], 1.2f, 2);
                        }
                        //else if (index_Line == 3)
                        //{
                        //    DeleLine(OldElement[i], 1.4f,3);
                        //}

                        if (m_Delete.Count > 0)
                        {
                            for (int a = 0; a < m_Delete.Count; a++)
                            {
                                if (OldElement[i] == m_Delete[a])
                                    index_Line--;
                            }
                        }

                        index_Line++;
                    }

                    if (CanDeletLine(OldElement[i]) != null && CanDeletLeft(OldElement[i]) != null && CanDeletRight(OldElement[i]) == null)
                    {
                        DeleLine(OldElement[i],1,1);
                        StartCoroutine(WaitSeconds());

                        DeleLeft(OldElement[i], 1.2f,2);

                        CreatGold(OldElement[i]);
                    }

                    if (CanDeletLine(OldElement[i]) != null && CanDeletLeft(OldElement[i]) == null && CanDeletRight(OldElement[i]) != null)
                    {
                        DeleLine(OldElement[i],1,1);

                        StartCoroutine(WaitSeconds());

                        DeleRight(OldElement[i],1.2f,2);

                        CreatGold(OldElement[i]);
                    }

                    if (CanDeletLine(OldElement[i]) == null && CanDeletLeft(OldElement[i]) != null && CanDeletRight(OldElement[i]) == null)
                    {
                        if (index_Left == 0)
                        {
                            DeleLeft(OldElement[i], 0,1);
                        }
                        
                        if (index_Left == 1)
                        {
                            DeleLeft(OldElement[i], 1.0f,2);
                        }
                        else if (index_Left == 2)
                        {
                            DeleLeft(OldElement[i], 1.2f,2);
                        }
                        else if (index_Left == 3)
                        {
                            DeleLeft(OldElement[i], 1.4f,3);
                        }

                        if (m_Delete.Count > 0)
                        {
                            for (int a = 0; a < m_Delete.Count; a++)
                            {
                                if (OldElement[i] == m_Delete[a])
                                    index_Left--;
                            }
                        }
                        index_Left++;
                    }

                    if (CanDeletLine(OldElement[i]) == null && CanDeletLeft(OldElement[i]) != null && CanDeletRight(OldElement[i]) != null)
                    {
                        DeleRight(OldElement[i],1,1);

                        StartCoroutine(WaitSeconds());

                        DeleLeft(OldElement[i], 1.2f,2);

                        CreatGold(OldElement[i]);
                    }

                    if (CanDeletLine(OldElement[i]) == null && CanDeletLeft(OldElement[i]) == null && CanDeletRight(OldElement[i]) != null)
                    {
                        if (index_Right == 0)
                        {
                            DeleRight(OldElement[i], 0,1);
                        }

                       
                        if (index_Right == 1)
                        {
                            DeleRight(OldElement[i], 1.0f,2);
                        }
                        else if (index_Right == 2)
                        {
                            DeleRight(OldElement[i], 1.2f,2);
                        }
                        else if (index_Right == 3)
                        {
                            DeleRight(OldElement[i], 1.4f,3);
                        }

                        if (m_Delete.Count > 0)
                        {
                            for (int a = 0; a < m_Delete.Count; a++)
                            {
                                if (OldElement[i] == m_Delete[a])
                                    index_Right--;
                            }
                        }

                        index_Right++;
                    }

                    if (CanDeletLine(OldElement[i]) != null && CanDeletLeft(OldElement[i]) != null && CanDeletRight(OldElement[i]) != null)
                    {
                        DeleRight(OldElement[i],1,1);

                        StartCoroutine(WaitSeconds());

                        DeleLeft(OldElement[i], 1.2f,2);

                        StartCoroutine(WaitSeconds());

                        DeleLine(OldElement[i], 1.4f,3);

                        CreatGold(OldElement[i]);
                    }
                    
                }

                m_Delete.Clear();

                if (index_Line >= 2 || index_Left >= 2 || index_Right >= 2)
                {
                    CreatGold(OldElement[0]);
                }

                if (index_Line == 0 && index_Left == 0 && index_Right == 0)
                {
                    GetScoreWithNum(0, OldElement[0].position, 0, 0);
                }

                OldElement.Clear();
                
                Debug.Log(index);
            }
        }
    }

    //创建小金币
    void CreatGold(Transform pos)
    {
        GameObject obj = Instantiate(m_gold);
        obj.transform.position = pos.position;


    }
    //判断是否可以放进去
    bool IsVer(Transform[] current,ElementType et)
    {
        for (int i = 0; i < current.Length; i++)
        {
            if (current[i] == null) return false;

            if (current[i].GetComponent<Element>().Color > ElementType.Empty && current[i].GetComponent<Element>().Color != et + 1)
            {
                Debug.Log(current[i].GetComponent<Element>().Color);

                return false;
            }
        }
        return true;
    }
    //根据坐标获取当前的元素
    public static Transform[] ComPos(Vector3[] pos)
    {
        Transform[] current = new Transform[pos.Length];

        if (AllElement != null)
        {
            for (int a = 0; a < pos.Length; a++)
            {
                for (int i = 0; i < AllElement.Count; i++)
                {
                    float offset = Math.Abs(Vector3.Distance(AllElement[i].position, pos[a]));

                    if (offset < 0.25f)
                    {
                        current[a] = AllElement[i];
                    }
                }
            }
            return current;
        }
        return null;
    }


    //根据消除数量获得分数并且获取当前分数的Sprite
    void GetScoreWithNum(int num,Vector3 pos,int indexs,float count)
    {
        if (indexs < 0) return;

        if (window_score[indexs] == null) return;

        window_score[indexs].position = pos ;

        window_score[indexs].gameObject.SetActive(true);
        window_score[indexs].GetComponent<Window_Score>().dex = count;

        Tables scoreTab = DataManager.tables[TableName.Score];

        int scorenum = scoreTab.GetDataWithIDAndIndex<int>(num.ToString(), 1);

        string path = scoreTab.GetDataWithIDAndIndex<string>(num.ToString(), 2);
        
        window_score[indexs].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Num/" + path); ;
        window_score[indexs].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1.0f, 0);

        m_canvas.AddScores(scorenum);
    }
    
    IEnumerator WaitSeconds()
    {
        //for(int i =0; i < 100;i++)
        //{
        yield return new WaitForEndOfFrame();
        //}
    }

    private void OnDestroy()
    {
        MessageCenter.Cancel(this.GetHashCode());
    }

}
