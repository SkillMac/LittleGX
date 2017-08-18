using UnityEngine;

public class Element : MonoBehaviour {
	public const float ELEMENT_WIDTH = 0.6f;
	public const float ELEMENT_HEIGHT = 0.5f;
	[SerializeField]
	private ElementType m_type;
	private Pos2Int m_pos;
	private SpriteRenderer m_image;
	private bool m_isInDeleteList = false;
	private int m_row;
	private int m_col;
	private bool m_isEmpty = true;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
    }

	public void Init(int row, int col, ElementType color) {
		m_image = transform.GetComponentInChildren<SpriteRenderer>();
		m_row = row;
		m_col = col;
		ResetPosition(Vector3.zero);
	}

	public void ResetPosition(Vector3 offsetPos) {
		float offset = 0f;
		if (m_row % 2 == 0) {
			offset = ELEMENT_WIDTH / 2;
		}
		Vector3 vec3Pos = new Vector3(m_col * ELEMENT_WIDTH + offset, -m_row * ELEMENT_HEIGHT) + offsetPos;
		transform.localPosition = vec3Pos;
	}

	public void ResetColor() {
		colorType = ElementType.Empty;
		m_isInDeleteList = false;
		m_isEmpty = true;
	}

	public void SetDarkColor(ElementType currColor) {
		colorType = currColor + 1;
	}

	public void ApplyColor() {
		colorType--;
		m_isEmpty = false;
	}

	public bool CheckIsEmpty() {
		return m_isEmpty;
	}

	public void SetInDeleteList() {
		m_isInDeleteList = true;
	}
	
	public bool isInDeleteList {
		get {
			return m_isInDeleteList;
		}
	}
	
	public Pos2Int pos {
		set {
			m_pos = value;
		}
		get {
			return m_pos;
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
