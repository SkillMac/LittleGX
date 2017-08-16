using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
    
    [System.Serializable]
	public struct Elemen {
        public ElementType type;
		public Sprite image;
    }

    //所有的元素
    public Elemen[] AllElement;
	public ElementType m_type;
	private Vector2 position;
	private SpriteRenderer m_image;
	private Dictionary<ElementType, Sprite> elementDic;

	void Awake() {
        m_image = transform.GetComponentInChildren<SpriteRenderer>();
        elementDic = new Dictionary<ElementType, Sprite>();
		for (int i = 0; i < AllElement.Length; i++) {
			if (!elementDic.ContainsKey(AllElement[i].type)) {
                elementDic.Add(AllElement[i].type, AllElement[i].image);
            }
        }
    }

	private void SetType(ElementType newType) {
        m_type = newType;
		if (elementDic.ContainsKey(newType)) {
            m_image.sprite = elementDic[newType];
        }
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
			SetType(value);
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
