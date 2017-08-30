using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour {
	private const float SHAPE_SCALE = 0.6f;
	private const float ELEMENT_SCALE_N = 0.15f;
	private const float ELEMENT_SCALE_P = 0.12f;
	private Vector3 m_vec3StartPos;
	private Vector3 m_Vec3StartScale;
	private EColorType m_eColorType;
	private Transform[,] m_arrChildTrans;
	private List<ShapeElement> m_lstElement;

	void Awake() {
		ConfigShapeMgr shapeConf = ConfigShapeMgr.instance;
		m_arrChildTrans = new Transform[shapeConf.rowCount, shapeConf.colCount * 2];
		m_lstElement = new List<ShapeElement>();
	}

	void Start(){
        m_Vec3StartScale = transform.localScale;
        m_vec3StartPos = transform.position;
    }

	public void AddChild(int uRow, int uCol, Transform trans) {
		trans.parent = transform;
		trans.localScale = Vector3.one * ELEMENT_SCALE_N;
		m_arrChildTrans[uRow, uCol] = trans;
	}

	public void InitShape(EColorType eColorType) {
		m_eColorType = eColorType;
		transform.localScale = Vector3.one * SHAPE_SCALE;
		Vector3 vec3Offset = GetOffsetPos();
		for (int uRow = 0; uRow < m_arrChildTrans.GetLength(0); uRow++) {
			for (int uCol = 0; uCol < m_arrChildTrans.GetLength(1); uCol++) {
				if (m_arrChildTrans[uRow, uCol] != null) {
					ShapeElement element = m_arrChildTrans[uRow, uCol].GetComponent<ShapeElement>();
					element.InitShapeElement(uRow, uCol, m_eColorType, vec3Offset);
					m_lstElement.Add(element);
				}
			}
		}
	}

	private Vector3 GetOffsetPos() {
		int uLeft	= int.MaxValue;
		int uRight	= 0;
		int uTop	= int.MaxValue;
		int uBottom	= 0;
		for (int uRow = 0; uRow < m_arrChildTrans.GetLength(0); uRow++) {
			for (int uCol = 0; uCol < m_arrChildTrans.GetLength(1); uCol++) {
				if (m_arrChildTrans[uRow, uCol] != null) {
					uLeft	= Mathf.Min(uLeft, uCol);
					uRight	= Mathf.Max(uRight, uCol);
					uTop	= Mathf.Min(uTop, uRow);
					uBottom	= Mathf.Max(uBottom, uRow);
				}
			}
		}
		float fPosX = ((uRight - uLeft) * 0.5f + uLeft) * Element.ELEMENT_WIDTH / 2;
		float fPosY = ((uBottom - uTop) * 0.5f + uTop) * Element.ELEMENT_HEIGHT;
		return new Vector3(-fPosX, fPosY);
	}

	void Update() {
		if (Input.GetMouseButton(0)) {
            Vector3 offset = new Vector3(0, 1.0f, 0);
			transform.localScale = Vector3.one;
			for (int i = 0; i < m_lstElement.Count; i++) {
				m_lstElement[i].transform.localScale = Vector3.one * ELEMENT_SCALE_P;
			}
            transform.position = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)) / 100.0f + offset;
			GameMgr.instance.MouseDown(this);
        }
        if (Input.GetMouseButtonUp(0)) {
			GameMgr.instance.MouseUp(this);
        }
    }

	public void ReturnStart() {
        transform.position = m_vec3StartPos;
        transform.localScale = m_Vec3StartScale;
		for (int i = 0; i < m_lstElement.Count; i++) {
			m_lstElement[i].transform.localScale = Vector3.one * ELEMENT_SCALE_N;
		}
    }
	
	public List<ShapeElement> GetAllElement() {
		return m_lstElement;
	}

	public EColorType f_eColorType {
		get {
			return m_eColorType;
		}
	}

	public Vector3 f_vec3StartPos {
		set {
			m_vec3StartPos = value;
		}
	}
}
