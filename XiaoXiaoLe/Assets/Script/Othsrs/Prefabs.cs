using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {
	private const int SHAPE_COUNT = 25;
    public Vector3[] Roots;
	public float offsetY = 0.2f;//Y轴坐标的偏移量
	public GameObject window_gv;
	private int m_uShapeNum = 0;
	private float m_fSpeed = 15.0f;
	private bool m_bIsMove;
    private bool m_bIsActive;
    private float m_fTimer;
	//随机数列的列数
	private int m_uColIndex;
	private int m_uLastIndex = 0;
	private int m_uCreatType;
	private int m_uDexcode;
	private int[] m_arrShapeGroup = {1, 6, 6, 6, 3, 3};
	private List<int> m_lstShapeGroup = new List<int>();

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
            int uShapeIndex = GetRandomIndex(m_uLastIndex);
			GameObject goShape = PrefabsFactory.CreateShape(uShapeIndex, transform);
            goShape.transform.position = Roots[i];
            m_uShapeNum++;
        }
    }
	
	void Update() {
		if (window_gv.activeSelf)
			return;
		if (Input.GetMouseButtonDown(0)) {
			Vector3 vec3MousePos = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f)) / 100.0f;
			for (int i = 0; i < Roots.Length && i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<TestDraw>().enabled = false;
				if (Mathf.Abs(Vector3.Distance(Roots[i], vec3MousePos)) < 1.2f) {
                    transform.GetChild(i).GetComponent<TestDraw>().enabled = true;
                }
            }
        }
		if (m_bIsMove) {
			for (int i = 0; i < transform.childCount; i++) {
                MoveWithIndex(transform.GetChild(i), i);
				if (transform.GetChild(i).position.y != Roots[i].y) {
                    m_bIsMove = false;
                }
            }
        }
		if(m_bIsActive) {
			if (Time.realtimeSinceStartup - m_fTimer > 1.0f) {
                window_gv.SetActive(true);
                m_bIsActive = false;
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

	private int GetRandomIndex(int uLastIndex) {
		int uLastGroup = m_lstShapeGroup[uLastIndex];
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
        return uIndex;
    }

	private void MoveWithIndex(Transform trans, int uIndex) {
		if (uIndex >= 0 && uIndex < Roots.Length) {
            Vector3 vec3Pos = Roots[uIndex];
            trans.position = Vector3.MoveTowards(trans.position, vec3Pos, m_fSpeed * Time.deltaTime);
			trans.GetComponent<TestDraw>().f_vec3StartPos = vec3Pos;
		}
    }
	
	public void OnMouseUpCreateByTrans(Transform trans) {
		m_uDexcode = trans.GetHashCode();
		//根据类型判断是否游戏可以继续
		if (!CanContinue()) {
			m_bIsActive = true;
			m_fTimer = Time.realtimeSinceStartup;
		}
		GameObject goShape = PrefabsFactory.CreateShape(m_uCreatType, transform);
		goShape.transform.position = Roots[Roots.Length - 1];
	}

	public void OnMouseUpCreateByIndex() {
		m_uShapeNum++;
		if (m_uShapeNum >= 30 && m_uShapeNum < 60) {
			m_uColIndex = 2;
		}
		if (m_uShapeNum >= 60) {
			m_uColIndex = 3;
		}
		m_uCreatType = GetRandomIndex(m_uLastIndex);
		m_bIsMove = true;
	}
	
	private bool CanContinue() {
		for (int uIndex = 0; uIndex < transform.childCount; uIndex++) {
			Transform childTrans = transform.GetChild(uIndex);
			//包含了想要删除的物体
			if (childTrans.GetHashCode() != m_uDexcode) {
				TestDraw shape = childTrans.GetComponent<TestDraw>();
				if (GameMgr.instance.CheckCanContinue(shape)) {
					return true;
				}
            }
        }
        return false;
    }
}
