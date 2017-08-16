using System.Collections.Generic;
using UnityEngine;

public class HummerRes {
	private const string ELEMENT_PATH = "images/Element/";
	private const string COLOR_BACK			= "back";
	private const string COLOR_GREEN		= "green";
	private const string COLOR_DARK_GREEN	= "green (1)";
	private const string COLOR_SIG			= "sig";
	private const string COLOR_DARK_SIG		= "sig (1)";
	private const string COLOR_PIN			= "pin";
	private const string COLOR_DARK_PIN		= "pin (1)";
	private const string COLOR_RED			= "red";
	private const string COLOR_DARK_RED		= "red (1)";
	private const string COLOR_YELLOW		= "yellow";
	private const string COLOR_DARK_YELLOW	= "yellow (1)";
	private const string COLOR_YY			= "yy";
	private const string COLOR_DARK_YY		= "yy (1)";
	private const string COLOR_WHITE		= "white";
	private static List<string> lstColorName;

	private static void InitColorList() {
		if (lstColorName == null) {
			lstColorName = new List<string>();
			lstColorName.Add(COLOR_BACK);
			lstColorName.Add(COLOR_GREEN);
			lstColorName.Add(COLOR_DARK_GREEN);
			lstColorName.Add(COLOR_SIG);
			lstColorName.Add(COLOR_DARK_SIG);
			lstColorName.Add(COLOR_PIN);
			lstColorName.Add(COLOR_DARK_PIN);
			lstColorName.Add(COLOR_RED);
			lstColorName.Add(COLOR_DARK_RED);
			lstColorName.Add(COLOR_YELLOW);
			lstColorName.Add(COLOR_DARK_YELLOW);
			lstColorName.Add(COLOR_YY);
			lstColorName.Add(COLOR_DARK_YY);
			lstColorName.Add(COLOR_WHITE);
		}
	}

	private static string GetElementColor(int index) {
		InitColorList();
		string str = ELEMENT_PATH + lstColorName[index];
		return str;
	}

	public static Sprite LoadElementSprite(int index) {
		string path = GetElementColor(index);
		Sprite spr = Resources.Load<Sprite>(path);
		return spr;
	}
}
