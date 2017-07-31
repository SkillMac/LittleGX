using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Element : MonoBehaviour,IPointerClickHandler {

    public bool IsEmpty;
    public float dex;
    float timer;


    [System.Serializable]
    public struct Elemen
    {
        public ElementType type;
        public Sprite  image;
    }
    //所有的元素
    public Elemen[] AllElement;

    private Vector2 position;

    public Vector2 GetPosition
    {
        set
        {
            position = value;
        }
        get
        {
            return position;
        }
    }

    //返回当前所有元素的个数
    public int NumEle
    {
        get
        {
            return AllElement.Length;
        }
    }

    private SpriteRenderer m_image;
    public ElementType m_type;

    private Dictionary<ElementType, Sprite> elementDic;

    public ElementType Color
    {
        get { return m_type; }
        set { SetType(value); }
    }

    private void Awake()
    {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();

        elementDic = new Dictionary<ElementType, Sprite>();

        for(int i =0;i< NumEle;i++)
        {
            if(!elementDic.ContainsKey(AllElement[i].type))
            {
                elementDic.Add(AllElement[i].type, AllElement[i].image);
            }
        }
        
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
        if(IsEmpty)
        {
            wait();

            if(dex <= 0.5f)
            {
                Color = ElementType.Empty;
                IsEmpty = false;
            }
        }
	}


    public void SetType(ElementType newType)
    {
        m_type = newType;

        if (elementDic.ContainsKey(newType))
        {
            m_image.sprite = elementDic[newType];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerEnter.name);
    }

    void wait()
    {
        if (Time.realtimeSinceStartup - timer > 0.5f)
        {
            timer = Time.realtimeSinceStartup;
            dex--;
            Debug.Log(dex);
        }
    }

}
//元素的颜色类型
public enum ElementType
{
    Empty,
    Green,
    DarkGreen,
    sig,
    Darksig,
    pin,
    Darkpin,
    red,
    Darkred,
    yellow,
    Darkyellow,
    yy,
    Darkyy,
    
    white =100,
}
