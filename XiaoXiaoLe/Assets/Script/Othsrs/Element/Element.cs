using UnityEngine;

public class Element : MonoBehaviour {
	public const float ELEMENT_WIDTH = 0.6f;
	public const float ELEMENT_HEIGHT = 0.5f;
	private SpriteRenderer m_spriteRenderer;
	protected int m_uRow;
	protected int m_uCol;

	void Awake() {
        m_spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

	protected void SetSprite(EColorType eColorType) {
		m_spriteRenderer.sprite = HummerRes.LoadElementSprite((int)eColorType);
	}

	public int f_uRow {
		get {
			return m_uRow;
		}
	}

	public int f_uCol {
		get {
			return m_uCol;
		}
	}
}

//元素的颜色类型
public enum EColorType {
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
