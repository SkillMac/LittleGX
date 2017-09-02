using System.Collections.Generic;
using UnityEngine;

public class GameMgr {
	private static GameMgr _instance;
	private Window_Creat m_windowCreate;
	private Window_Canvas m_windowCanvas;
	private Window_Delete m_windowDelete;
	private Prefabs m_prefabs;

	private GameMgr() {

	}

	public void AddScore() {
		m_windowCanvas.OnAddScore();
	}

	public void AddGold() {
		m_windowCanvas.OnAddGold();
	}

	public Vector3 GetGoldTargetPos() {
		return m_windowCanvas.GetGoldTargetPos();
	}

	public void Delete(List<List<BackElement>> list) {
		m_windowDelete.OnDelete(list);
	}

	public bool CheckClickGold(Vector3 vec3ClickPos) {
		return m_windowDelete.CheckClickGold(vec3ClickPos);
	}

	public void ShapeForward() {
		m_prefabs.ShapeForward();
	}

	public void CheckEndAndCreateShape() {
		m_prefabs.CheckEndAndCreateShape();
	}
	
	public void CheckClickShape(Vector3 vec3ClickPos) {
		m_prefabs.CheckClickShape(vec3ClickPos);
	}

	public void MouseUp(TestDraw shape) {
		m_windowCreate.OnMouseUp(shape);
	}

	public void MouseDown(TestDraw shape) {
		m_windowCreate.OnMouseDown(shape);
	}

	public bool CheckCanContinue(TestDraw shape) {
		return m_windowCreate.CheckCanContinue(shape);
	}

	public static GameMgr instance {
		get {
			if (_instance == null) {
				_instance = new GameMgr();
			}
			return _instance;
		}
	}

	public Window_Creat f_windowCreate {
		set {
			m_windowCreate = value;
		}
	}

	public Window_Canvas f_windowCanvas {
		set {
			m_windowCanvas = value;
		}
	}

	public Window_Delete f_windowDelete {
		set {
			m_windowDelete = value;
		}
	}

	public Prefabs f_prefabs {
		set {
			m_prefabs = value;
		}
	}
}
