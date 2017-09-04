using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
	public const float ELEMENT_WIDTH = 0.6f;
	public const float ELEMENT_HEIGHT = 0.5f;
	private static Color COLOR_BACK			= new Color(0.24f, 0.24f, 0.24f);
	private static Color COLOR_GREEN		= new Color(0.42f, 0.83f, 0.56f);
	private static Color COLOR_DARK_GREEN	= new Color(0.24f, 0.58f, 0.36f);
	private static Color COLOR_SIG			= new Color(0.77f, 0.91f, 0.48f);
	private static Color COLOR_DARK_SIG		= new Color(0.69f, 0.82f, 0.44f);
	private static Color COLOR_PIN			= new Color(0.51f, 0.53f, 0.96f);
	private static Color COLOR_DARK_PIN		= new Color(0.49f, 0.5f, 0.86f);
	private static Color COLOR_RED			= new Color(0.96f, 0.51f, 0.68f);
	private static Color COLOR_DARK_RED		= new Color(0.87f, 0.49f, 0.64f);
	private static Color COLOR_YELLOW		= new Color(0.95f, 0.82f, 0.51f);
	private static Color COLOR_DARK_YELLOW	= new Color(0.88f, 0.77f, 0.51f);
	private static Color COLOR_YY			= new Color(0.91f, 0.61f, 0.42f);
	private static Color COLOR_DARK_YY		= new Color(0.87f, 0.58f, 0.39f);
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
