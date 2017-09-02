using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {
	private const int SHAPE_COUNT = 25;
    public Vector3[] Roots;
	public GameObject window_gv;
	private int m_uShapeNum = 0;
    private bool m_bGameOver;
    private float m_fGameOverTime;
	//随机数列的列数
	private int m_uColIndex;
	private int m_uLastIndex = 0;
	private int[] m_arrShapeGroup = {1, 6, 6, 6, 3, 3};
	private List<int> m_lstShapeGroup = new List<int>();
	private List<TestDraw> m_lstShape = new List<TestDraw>();

	void Awake() {
		GameMgr.instance.f_prefabs = this;
		InitShapeGroup();
	}

	private void InitShapeGroup() {
		for (int uGroup = 0; uGroup < m_arrShapeGroup.Length; uGroup++) {
			for (int i = 0; i < m_arrShapeGroup[uGroup]; i++) {
				m_lstShapeGroup.Add(uGroup);
			}
		}
	}

	void Start() {
		m_uColIndex = 1;
		for (int i = 0; i < Roots.Length; i++) {
            int uShapeIndex = GetRandomIndex();
			TestDraw shape = PrefabsFactory.CreateShape(this, uShapeIndex);
            shape.transform.position = Roots[i];
			m_lstShape.Add(shape);
        }
    }
	
	void Update() {
		if (window_gv.activeSelf)
			return;
		if(m_bGameOver) {
			if (Time.realtimeSinceStartup - m_fGameOverTime > 1.0f) {
                window_gv.SetActive(true);
                m_bGameOver = false;
            }
        }
	}

	public void CheckClickShape(Vector3 vec3ClickPos) {
		if (window_gv.activeSelf)
			return;
		for (int i = 0; i < m_lstShape.Count; i++) {
			TestDraw shape = m_lstShape[i];
			if (Vector3.Distance(shape.transform.position, vec3ClickPos) < 1.2f) {
				shape.OnMouseDown();
				break;
			}
		}
	}
	
	private int GetRandomMaxNum(int uLastGroup) {
		int uNum = 0;
		ConfigPrefabsMgr configMgr = ConfigPrefabsMgr.instance;
		for (int uIndex = 0; uIndex < SHAPE_COUNT; uIndex++) {
			if (uLastGroup != 0 && uLastGroup == m_lstShapeGroup[uIndex]) {
				continue;
			}
			uNum += configMgr.GetData(uIndex.ToString(), m_uColIndex - 1);
		}
		return uNum;
	}

	private int GetRandomIndex() {
		if (m_uShapeNum >= 30 && m_uShapeNum < 60) {
			m_uColIndex = 2;
		} else if (m_uShapeNum >= 60) {
			m_uColIndex = 3;
		}
		int uLastGroup = m_lstShapeGroup[m_uLastIndex];
		int uMax = GetRandomMaxNum(uLastGroup);
		int uRandomNum = Random.Range(0, uMax);
		int uNum = 0;
		int uIndex = 0;
		ConfigPrefabsMgr configMgr = ConfigPrefabsMgr.instance;
		for (int i = 0; i < SHAPE_COUNT; i++) {
			if (uLastGroup != 0 && uLastGroup == m_lstShapeGroup[i]) {
				continue;
			}
			uNum += configMgr.GetData(i.ToString(), m_uColIndex - 1);
			if (uRandomNum < uNum) {
				uIndex = i;
				break;
			}
		}
        m_uLastIndex = uIndex;
		m_uShapeNum++;
		return uIndex;
    }
	
	public void CheckEndAndCreateShape() {
		if (!CanContinue()) {
			m_bGameOver = true;
			m_fGameOverTime = Time.realtimeSinceStartup;
		}
		int uCreatType = GetRandomIndex();
		TestDraw shape = PrefabsFactory.CreateShape(this, uCreatType);
		shape.transform.position = Roots[Roots.Length - 1];
		m_lstShape.Add(shape);
	}

	public void ShapeForward() {
		for (int i = 0; i < m_lstShape.Count; i++) {
			m_lstShape[i].DoMove(Roots[i]);
		}
	}
	
	private bool CanContinue() {
		for (int uIndex = 0; uIndex < m_lstShape.Count; uIndex++) {
			if (GameMgr.instance.CheckCanContinue(m_lstShape[uIndex])) {
				return true;
			}
		}
        return false;
    }

	public void RemoveShap(TestDraw shap) {
		m_lstShape.Remove(shap);
	}
}
