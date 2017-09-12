using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour {
	private const float SHAPE_SCALE = 0.6f;
	private const float ELEMENT_SCALE_N = 0.15f;
	private const float ELEMENT_SCALE_P = 0.12f;
	private const float AUTO_MOVE_SPEED = 15.0f;
    private const float RETURN_MOVE_SPEED = 30.0f;
	private Vector3 m_vec3StartPos;
	private Vector3 m_Vec3StartScale;
	private EColorType m_eColorType;
	private Transform[,] m_arrChildTrans;
	private List<ShapeElement> m_lstElement;
	private bool m_bMouseEnable = false;
	private bool m_bMove = false;
	private Prefabs m_prefabs;
    [HideInInspector]
    public bool m_returnStart = false;

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

	public void InitShape(Prefabs prefabs, EColorType eColorType) {
		m_prefabs = prefabs;
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
		if (m_bMouseEnable) {
			MouseEvent();
		}
		if (m_bMove) {
			Move();
		}
        if (m_returnStart)
        {
            ReturnStart();
        }
    }

	private void MouseEvent() {
		if (Input.GetMouseButton(0)) {
			Vector3 offset = new Vector3(0, 1.0f, 0);
			Vector3 vec3MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			vec3MousePos.z = 0;
			transform.position = vec3MousePos + offset;
			GameMgr.instance.MouseDown(this);
		}
		if (Input.GetMouseButtonUp(0)) {
			m_bMouseEnable = false;
			GameMgr.instance.MouseUp(this);
		}
	}

	public void OnMouseDown() {
		m_bMove = false;
		m_bMouseEnable = true;
		transform.localScale = Vector3.one;
		for (int i = 0; i < m_lstElement.Count; i++) {
			m_lstElement[i].transform.localScale = Vector3.one * ELEMENT_SCALE_P;
		}
	}

	private void Move() {
		transform.position = Vector3.MoveTowards(transform.position, m_vec3StartPos, AUTO_MOVE_SPEED * Time.deltaTime);
		if (transform.position == m_vec3StartPos) {
			m_bMove = false;
		}
	}

	public void DoMove(Vector3 vec3TargetPos) {
		m_bMove = true;
		m_vec3StartPos = vec3TargetPos;
	}

	public void ReturnStart() {
        transform.position = Vector3.MoveTowards(transform.position, m_vec3StartPos, RETURN_MOVE_SPEED * Time.deltaTime);
        transform.localScale = m_Vec3StartScale;
		for (int i = 0; i < m_lstElement.Count; i++) {
			m_lstElement[i].transform.localScale = Vector3.one * ELEMENT_SCALE_N;
		}
        if (transform.position == m_vec3StartPos)
        {
            m_returnStart = false;
        }
    }

	public void DestroySelf() {
		m_prefabs.RemoveShap(this);
		Destroy(gameObject);
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
