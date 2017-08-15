using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Play : MonoBehaviour {

    public List<Transform> AllElement;

    private List<Transform> OldElement = new List<Transform>();

    private Transform[] old;
    private int index;
	
	private void OnMouseDown(Transform[] tran) {
		if (tran == null || tran == old)
			return;
		Vector3[] pos = new Vector3[tran.Length];
		ElementType currenttype;
		currenttype = tran[0].GetComponent<Element>().Color;
		for (int i = 0; i < tran.Length; i++) {
			pos[i] = tran[i].position;
		}
		for (int i = 0; i < OldElement.Count; i++) {
			if (OldElement.Count == 0)
				return;
			OldElement[i].GetComponent<Element>().Color = ElementType.Empty;
		}
		Transform[] current = ComPos(pos);
		if (current != null && IsVer(current)) {
			OldElement.Clear();
			for (int i = 0; i < current.Length; i++) {
				current[i].GetComponent<Element>().Color = currenttype;
				old = tran;
				OldElement.Add(current[i]);
			}
		}
	}

	private void OnMouseUp(Transform[] arrTrans) {
		if (OldElement.Count == 0)
			return;
		for (int i = 0; i < OldElement.Count; i++) {
			AllElement.Add(OldElement[i]);
		}
		OldElement.Clear();
		index++;
		EventMgr.MouseUpCreateByIndex();
	}

    private void Awake() {
		EventMgr.MouseUpEvent += OnMouseUp;
		EventMgr.MouseDownEvent += OnMouseDown;
		AllElement = new List<Transform>();

        for(int i =0;i<transform.childCount;i++)
        {
            Transform chlid = transform.GetChild(i);
            AllElement.Add(chlid);

            Debug.Log(chlid.GetChild(0).localScale);
        }
    }
    // Use this for initialization
    void Start () {
        //Debug.Log(AllElement.Count);

        for (int i = 0; i < AllElement.Count; i++)
        {
            AllElement[i].GetComponent<Element>().Color = ElementType.Empty;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //判断是否可以放进去
    bool IsVer(Transform[] current)
    {
        for(int i =0;i < current.Length;i++)
        {
            if (current[i] == null) return false;

            if((current[i].GetComponent<Element>().Color != ElementType.Empty)&&OldElement.Contains(current[i]))
            {
                return false;
            }
        }
        return true;
    }
    //根据坐标获取当前的元素
    Transform[] ComPos(Vector3 []pos)
    {
        Transform[] current = new Transform[pos.Length];

        if(AllElement !=null)
        {
            for(int a =0;a<pos.Length;a++)
            {
                for (int i = 0; i < AllElement.Count; i++)
                {
                    float offset = Math.Abs(Vector3.Distance(AllElement[i].position, pos[a]));

                    if (offset < 0.3f)
                    {
                        current[a] = AllElement[i];
                    }
                }
            }
            return current;
        }
        return null;
    }

    private void OnDestroy() {
		EventMgr.MouseUpEvent -= OnMouseUp;
		EventMgr.MouseDownEvent -= OnMouseDown;
	}
}
