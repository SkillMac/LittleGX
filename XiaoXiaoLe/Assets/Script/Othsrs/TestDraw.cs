using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour {
    public Vector3 startpos;
	private Vector3 m_Scale;
	private EColorType m_eColorType;
	private Transform[,] m_arrChildTrans;
	private List<ShapeElement> m_lstElement;

	void Awake() {
		ConfigShapeMgr shapeConf = ConfigShapeMgr.instance;
		m_arrChildTrans = new Transform[shapeConf.rowCount, shapeConf.colCount * 2];
		m_lstElement = new List<ShapeElement>();
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

	public void InitShape(EColorType colorType) {
		m_eColorType = colorType;
		transform.localScale = Vector3.one * 0.6f;
		Vector3 vec3 = GetOffsetPos();
		for (int row = 0; row < m_arrChildTrans.GetLength(0); row++) {
			for (int col = 0; col < m_arrChildTrans.GetLength(1); col++) {
				if (m_arrChildTrans[row, col] != null) {
					ShapeElement element = m_arrChildTrans[row, col].GetComponent<ShapeElement>();
					element.InitShapeElement(row, col, m_eColorType, vec3);
					m_lstElement.Add(element);
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
			EventMgr.MouseDown(this);
        }
        if (Input.GetMouseButtonUp(0)) {
			EventMgr.MouseUp(this);
        }
    }

	public void ReturnStart() {
        transform.position = startpos;
        transform.localScale = m_Scale;
		for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).localScale = new Vector3(0.15f, 0.15f, 0.15f);
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
}
