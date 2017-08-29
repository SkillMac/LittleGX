using UnityEngine;

public class Element : MonoBehaviour {
	public const float ELEMENT_WIDTH = 0.6f;
	public const float ELEMENT_HEIGHT = 0.5f;
	private ColorType m_type;
	private Pos2Int m_pos;
	private SpriteRenderer m_image;
	private bool m_isInDeleteList = false;
	private int m_row;
	private int m_col;
	private bool m_isEmpty = true;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
    }

	public void InitPos(int row, int col) {
		m_row = row;
		m_col = col;
	}

	public void InitElement(int row, int col, ColorType color, Vector3 offsetPos) {
		m_row = row;
		m_col = col;
		colorType = color;
		ResetPosition(offsetPos);
	}
	
	private void ResetPosition(Vector3 offsetPos) {
		Vector3 vec3Pos = new Vector3(m_col * ELEMENT_WIDTH / 2, -m_row * ELEMENT_HEIGHT) + offsetPos;
		transform.localPosition = vec3Pos;
	}

	public void ResetColor() {
		colorType = ColorType.Empty;
		m_isInDeleteList = false;
		m_isEmpty = true;
	}

	public void SetDarkColor(ColorType currColor) {
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
	
	public ColorType colorType {
		get {
			return m_type;
		}
		set {
			m_type = value;
			m_image.sprite = HummerRes.LoadElementSprite((int)m_type);
		}
	}

	public int f_uRow {
		get {
			return m_row;
		}
	}

	public int f_uCol {
		get {
			return m_col;
		}
	}
}

//元素的颜色类型
public enum ColorType {
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
