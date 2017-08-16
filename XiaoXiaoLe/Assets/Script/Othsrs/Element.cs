using UnityEngine;

public class Element : MonoBehaviour {
	public ElementType m_type;
	private Vector2 position;
	private SpriteRenderer m_image;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
    }

	public void ResetColor() {
		colorType = ElementType.Empty;
	}

	public bool CheckIsEmpty() {
		return m_type == ElementType.Empty;
	}
	
	public Vector2 GetPosition {
		set {
			position = value;
		}
		get {
			return position;
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
