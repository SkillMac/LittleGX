using UnityEngine;

public class Element : MonoBehaviour {
	public ElementType m_type;
	private Pos2Int vec2Pos;
	private SpriteRenderer m_image;
	private bool isInDeleteList = false;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
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
