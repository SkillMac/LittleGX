﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EventMgr {
	public static event Action<List<List<Element>>> DeleteEvent;
	public static event Action<Vector3> AddScoreEvent;
	public static event Action AddGoldEvent;
	public static event Action<Transform> MouseUpDeleteEvent;
	public static event Action MouseUpCreateByIndexEvent;
	public static event Action<Transform> MouseUpCreateByTransEvent;
	public static event Action<TestDraw> MouseUpEvent;
	public static event Action<TestDraw> MouseDownEvent;

	public static void Delete(List<List<Element>> list) {
		if (DeleteEvent != null) {
			DeleteEvent(list);
        }
    }

	public static void AddScore(Vector3 vec3) {
		if (AddScoreEvent != null) {
			AddScoreEvent(vec3);
		}
	}

	public static void AddGold() {
		if (AddGoldEvent != null) {
			AddGoldEvent();
		}
	}

	public static void MouseUpDelete(Transform trans) {
		if (MouseUpDeleteEvent != null) {
			MouseUpDeleteEvent(trans);
		}
	}

	public static void MouseUpCreateByIndex() {
		if (MouseUpCreateByIndexEvent != null) {
			MouseUpCreateByIndexEvent();
		}
	}

	public static void MouseUpCreateByTrans(Transform trans) {
		if (MouseUpCreateByTransEvent != null) {
			MouseUpCreateByTransEvent(trans);
		}
	}

	public static void MouseUp(TestDraw shape) {
		if (MouseUpEvent != null) {
			MouseUpEvent(shape);
		}
	}

	public static void MouseDown(TestDraw shape) {
		if (MouseDownEvent != null) {
			MouseDownEvent(shape);
		}
	}
}
