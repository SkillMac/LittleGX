using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestDraw : MonoBehaviour {
    
    public Vector3 oldPos, startpos;
    public float dirx,diry;

    public Vector3 m_Scale;

    public PrefabsType typp;

    // Use this for initialization
    void Start()
    {
        m_Scale = transform.localScale;

        startpos = transform.position;

        oldPos = transform.position * 100.0f + new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
        
    }
    // Update is called once per frame
    void Update () {

        //if (Input.GetMouseButtonDown(0))
        //{
        //    oldPos = Input.mousePosition;
        //    startpos = Input.mousePosition;
        //}
        if (Input.GetMouseButton(0))
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localScale = new Vector3(1.0f, 1.0f, 1.0f) * 0.12f;
            }

            dirx = Input.mousePosition.x - oldPos.x;
            Drag(dirx);
            
            diry = Input.mousePosition.y - oldPos.y;

            transform.Translate(0, diry / 100, 0);

            oldPos = Input.mousePosition;

            if(Mathf.Abs( Vector3.Distance(transform.position * 100 + new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0),Input.mousePosition)) >1.0f)
            {
                transform.position = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)) /100.0f;
            }

            MessageCenter.ReceiveMassage(new MessageData(MessageType.MOUSE_DOWN, GetChilds()));

        }
        if (Input.GetMouseButtonUp(0))
        {
            MessageCenter.ReceiveMassage(new MessageData(MessageType.MOUSE_UP, GetChilds()));
            
        }
    }

    public void ReturnStart()
    {
        transform.position = startpos;

        Debug.Log(startpos);

        oldPos = transform.position * 100.0f + new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);

        transform.localScale = m_Scale;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
    }


    Transform[] GetChilds()
    {
        Transform[] postion = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            postion[i] = transform.GetChild(i);
        }

        return postion;
    }
    public virtual void Drag(float f)
    {
        transform.Translate(f / 100, 0, 0);
    }

    //相对坐标
    public Vector3[] GetAbsPos()
    {
        if (transform.childCount == 1) return null;
        else
        {
            Vector3[] AbsPos = new Vector3[transform.childCount-1];

            for (int i = 1; i < transform.childCount; i++)
            {
                AbsPos[i-1] = (transform.GetChild(i).localPosition - transform.GetChild(0).localPosition);
            }

            return AbsPos;
        }
    }
}

