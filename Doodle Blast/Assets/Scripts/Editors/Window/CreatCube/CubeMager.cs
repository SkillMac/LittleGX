using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMager : MonoBehaviour
{
    private const float OFFSET = 0.1f;
    public  int myWidthSize = 10;
    public int myHeightsize = 10;
    public int myRotation = 0;
    private float width;
    private float height;
    private float maxY;
    private float minY;
    //private CreatCubeButton m_WindowMager;
    private GameObject top;
    private GameObject cup;
    private CubeEditorWindow m_EditorWindow;
    private List<Transform> allChlids = new List<Transform>();
    private List<Vector3> chlidsPosition = new List<Vector3>();
    private float screenWidth;

    public void Init(CreatCubeButton window)
    {
        //m_WindowMager = window;
        top = window.top;
        cup = window.cup;
        m_EditorWindow = window.m_EditorWindow;
    }

    private void InitEditorWindowText()
    {
        m_EditorWindow.m_Width.text = transform.localScale.x.ToString();
        m_EditorWindow.m_Height.text = transform.localScale.y.ToString();
        m_EditorWindow.m_Rotate.text = myRotation.ToString();
    }
   
    private void UpdateChlidsPos()
    {
        chlidsPosition = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            chlidsPosition.Add(transform.GetChild(i).position);
        }
    }

    private void UpDateData()
    {
        GetSortByX();
        width = Mathf.Abs(chlidsPosition[0].x - chlidsPosition[3].x) / 2;
        height = Mathf.Abs(chlidsPosition[1].y - chlidsPosition[2].y) / 2;
    }

    void Start()
    {
        screenWidth = CDataMager.screenWidth;
        maxY = top.transform.position.y - top.transform.lossyScale.y * top.GetComponent<SpriteRenderer>().sprite.rect.height / 200;
        minY = cup.transform.position.y + cup.transform.lossyScale.y * cup.GetComponent<SpriteRenderer>().sprite.rect.height / 200;

        for (int i =0; i< transform.childCount;i++)
        {
            allChlids.Add(transform.GetChild(i));
            chlidsPosition.Add(transform.GetChild(i).position);
        }
    }

    void OnMouseDown()
    {
        if (transform.GetComponent<BoxCollider2D>().enabled == false) return;
        m_EditorWindow.Init(this);
        m_EditorWindow.gameObject.SetActive(true);
        InitEditorWindowText();
    }

    void OnMouseDrag()
    {
        if (transform.GetComponent<BoxCollider2D>().enabled == false) return;
        UpDateData();
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -1;
        if (pos.x > (screenWidth / 300f - width))
        {
            pos.x = screenWidth / 300f - width;
        }
        else if (pos.x < -screenWidth / 200f + width)
        {
            pos.x = -screenWidth / 200f + width;
        }
        if (pos.y > maxY - height)
            pos.y = maxY - height;
        else if (pos.y < minY +height)
            pos.y = minY + height;
        //Debug.Log(height + " ..." + maxY + "******" + minY);
        transform.position = pos;
    }
    
    /// <summary>
    /// 根据X坐标从小到大排列坐标
    /// </summary>
    /// <returns></returns>
    private List<Vector3> GetSortByX()
    {
        UpdateChlidsPos();
        chlidsPosition.Sort(SortRuleByX);
        return chlidsPosition;
    }
   
    private int SortRuleByX(Vector3 a,Vector3 b)
    {
        if (a.x > b.x) return 1;
        if (a.x < b.x) return -1;
        return 0;
    }
    
    //物体变大位置不动可能超出边界
    public void ChangePosAsScaleOrRotation()
    {
        List<Vector3> tempByX = GetSortByX();
        int count = tempByX.Count;
        Vector3 pos = transform.position;

        if (tempByX[count-1].x > screenWidth / 300f)
        {
            pos.x -= OFFSET;
        }
        else if (tempByX[0].x < -screenWidth / 200f)
        {
            pos.x += OFFSET;
        }

        if(tempByX[1].y > tempByX[2].y)
        {
            if (tempByX[1].y > maxY)
                pos.y -= OFFSET;
            else if (tempByX[2].y < minY)
                pos.y += OFFSET;
        }
        else
        {
            if (tempByX[2].y > maxY)
                pos.y -= OFFSET;
            else if (tempByX[1].y < minY)
                pos.y += OFFSET;
        }
        transform.position = pos;
    }
}
