using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
	public const float ELEMENT_WIDTH = 0.6f;
	public const float ELEMENT_HEIGHT = 0.5f;
    private static Color COLOR_BACK = new Color(0.243f,0.243f,0.243f);
    private static Color COLOR_GREEN = new Color(0.42f,0.835f,0.557f);
    private static Color COLOR_DARK_GREEN = new Color(0.322f,0.58f,0.408f);
	private static Color COLOR_SIG			= new Color(0.78f,0.914f,0.486f);
	private static Color COLOR_DARK_SIG		= new Color(0.541f,0.627f,0.357f);
	private static Color COLOR_PIN			= new Color(0.506f, 0.518f, 0.941f);
	private static Color COLOR_DARK_PIN		= new Color(0.376f, 0.384f, 0.655f);
	private static Color COLOR_RED			= new Color(0.957f, 0.514f, 0.678f);
	private static Color COLOR_DARK_RED		= new Color(0.655f, 0.376f, 0.482f);
	private static Color COLOR_YELLOW		= new Color(0.953f, 0.824f, 0.514f);
	private static Color COLOR_DARK_YELLOW	= new Color(0.655f, 0.573f, 0.376f);
	private static Color COLOR_YY			= new Color(0.914f, 0.604f, 0.416f);
	private static Color COLOR_DARK_YY		= new Color(0.631f, 0.435f, 0.318f);
	private static List<Color> lstColor = new List<Color>();
	private SpriteRenderer m_spriteRenderer;
	protected int m_uRow;
	protected int m_uCol;

	void Awake() {
		lstColor.Add(COLOR_BACK);
		lstColor.Add(COLOR_GREEN);
		lstColor.Add(COLOR_DARK_GREEN);
		lstColor.Add(COLOR_SIG);
		lstColor.Add(COLOR_DARK_SIG);
		lstColor.Add(COLOR_PIN);
		lstColor.Add(COLOR_DARK_PIN);
		lstColor.Add(COLOR_RED);
		lstColor.Add(COLOR_DARK_RED);
		lstColor.Add(COLOR_YELLOW);
		lstColor.Add(COLOR_DARK_YELLOW);
		lstColor.Add(COLOR_YY);
		lstColor.Add(COLOR_DARK_YY);
		m_spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

	protected void SetSprite(EColorType eColorType) {
		m_spriteRenderer.color = lstColor[(int)eColorType];
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
