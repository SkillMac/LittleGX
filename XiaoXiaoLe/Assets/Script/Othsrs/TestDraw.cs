using System;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour {
    public Vector3 startpos;
    public Vector3 m_Scale;

	[Serializable]
	public struct Row {
		public bool[] arr;
	}
	public Row[] _arrShapeMap;
	public ElementType _elementType;

	public List<Pos2Int> m_lstChildPos = new List<Pos2Int>();

	private Transform[,] m_arrChildTrans;

	void Awake() {
		ConfigShapeMgr shapeConf = ConfigShapeMgr.instance;
		m_arrChildTrans = new Transform[shapeConf.rowCount, shapeConf.colCount * 2];
	}

	void Start(){
        m_Scale = transform.localScale;
        startpos = transform.position;
    }

	public void AddChild(int row, int col, Transform trans) {
		trans.parent = transform;
		trans.localScale = Vector3.one * 0.15f;
		m_arrChildTrans[row, col] = trans;
	}

	public void InitShape(ElementType colorType) {
		_elementType = colorType;
		transform.localScale = Vector3.one * 0.6f;
		Vector3 vec3 = GetOffsetPos();
		for (int row = 0; row < m_arrChildTrans.GetLength(0); row++) {
			for (int col = 0; col < m_arrChildTrans.GetLength(1); col++) {
				if (m_arrChildTrans[row, col] != null) {
					Element element = m_arrChildTrans[row, col].GetComponent<Element>();
					element.InitElement(row, col, _elementType, vec3);
				}
			}
		}
	}

	private Vector3 GetOffsetPos() {
		int left = int.MaxValue, right = 0, top = int.MaxValue, bottom = 0;
		for (int row = 0; row < m_arrChildTrans.GetLength(0); row++) {
			for (int col = 0; col < m_arrChildTrans.GetLength(1); col++) {
				if (m_arrChildTrans[row, col] != null) {
					if (left > col) {
						left = col;
					}
					if (right < col) {
						right = col;
					}
					if (top > row) {
						top = row;
					}
					if (bottom < row) {
						bottom = row;
					}
				}
			}
		}
		float posX = ((right - left) * 0.5f + left) * Element.ELEMENT_WIDTH / 2;
		float posY = ((bottom - top) * 0.5f + top) * Element.ELEMENT_HEIGHT;
		Vector3 vec3Pos = new Vector3(-posX, posY);
		return vec3Pos;
	}

	void Update() {
		if (Input.GetMouseButton(0)) {
            Vector3 offset = new Vector3(0, 1.0f, 0);
			transform.localScale = Vector3.one;
			for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).localScale = Vector3.one * 0.12f;
            }
            transform.position = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)) / 100.0f + offset;
			EventMgr.MouseDown(GetChilds());
        }
        if (Input.GetMouseButtonUp(0)) {
			EventMgr.MouseUp(GetChilds());
        }
    }

	public void ReturnStart() {
        transform.position = startpos;
        transform.localScale = m_Scale;
		for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
    }
	
	private Transform[] GetChilds() {
        Transform[] postion = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
            postion[i] = transform.GetChild(i);
        }
        return postion;
    }
	
    //相对坐标
	public Vector3[] GetAbsPos() {
		if (transform.childCount == 1)
			return null;
		Vector3[] AbsPos = new Vector3[transform.childCount - 1];
		for (int i = 1; i < transform.childCount; i++) {
			AbsPos[i - 1] = (transform.GetChild(i).localPosition - transform.GetChild(0).localPosition);
		}
		return AbsPos;
	}
}
