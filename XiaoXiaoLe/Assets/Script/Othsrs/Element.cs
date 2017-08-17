using UnityEngine;

public class Element : MonoBehaviour {
	public ElementType m_type;
	private Pos2Int vec2Pos;
	private SpriteRenderer m_image;
	private bool isInDeleteList = false;
	private int m_row, m_col;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
    }

	public void Init(int row, int col, ElementType color) {
		m_image = transform.GetComponentInChildren<SpriteRenderer>();
		float offset = 0f;
		if (row % 2 == 0) {
			offset = 0.3f;
		}
		m_row = row;
		m_col = col;
		Vector3 vec3Pos = new Vector3(col * 0.6f + offset, -row * 0.5f);
		transform.localPosition = vec3Pos;
		colorType = color;
	}

	public void ResetPosition(Vector3 offsetPos) {
		float offset = 0f;
		if (m_row % 2 == 0) {
			offset = 0.3f;
		}
		Vector3 vec3Pos = new Vector3(m_col * 0.6f + offset, -m_row * 0.5f) + offsetPos;
		transform.localPosition = vec3Pos;
	}

	public void ResetColor() {
		colorType = ElementType.Empty;
		isInDeleteList = false;
	}

	public bool CheckIsEmpty() {
		return m_type == ElementType.Empty;
	}

	public void SetInDeleteList() {
		isInDeleteList = true;
	}

	public bool IsInDeleteList() {
		return isInDeleteList;
	}
	
	public Pos2Int pos {
		set {
			vec2Pos = value;
		}
		get {
			return vec2Pos;
		}
	}

	public Vector3 position {
		get {
			return transform.position;
		}
	}
	
	public ElementType colorType {
		get {
			return m_type;
		}
		set {
			m_type = value;
			m_image.sprite = HummerRes.LoadElementSprite((int)m_type);
		}
	}
}

//元素的颜色类型
public enum ElementType {
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
	Darkyy
}
