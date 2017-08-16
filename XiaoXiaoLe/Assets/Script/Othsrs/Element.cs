using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
	public ElementType m_type;
	private Vector2 position;
	private SpriteRenderer m_image;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
    }
	
	public Vector2 GetPosition {
		set {
			position = value;
		}
		get {
			return position;
		}
	}
	
	public ElementType Color {
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
    Darkyy,
    
    white =100,
    Wait = 200,
}
