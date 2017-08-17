using UnityEngine;

public class TestDraw : MonoBehaviour {
    public Vector3 startpos;
    public Vector3 m_Scale;
    public PrefabsType typp;
	
	void Start(){
        m_Scale = transform.localScale;
        startpos = transform.position;
    }

	void Update() {
		if (Input.GetMouseButton(0)) {
            Vector3 offset = new Vector3(0, 1.0f, 0);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).localScale = new Vector3(1.0f, 1.0f, 1.0f) * 0.12f;
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
