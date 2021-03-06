﻿using UnityEngine;

public class Gold : MonoBehaviour {
	private const float MOVE_DOWN_SPEED = 3.0f;
    private const float MOVE_UP_SPEED = 15.0f;
    public Transform _transMoveGold;
	public GameObject _goGold;
	public Transform sun;
    public Transform gold;
    public float yoffset;
	public int startAni;//开始闪烁的时间
	public int Disable;//物体消失的时间
	private Vector3 m_vec3Target;
	private bool m_bMoved = false;
	private bool m_bCanMove = false;
	private int m_uCount = 0;
	private float m_fTime;
	private Vector3 m_vec3FirstPos;
	private Animator m_animator;
	private Window_Delete m_winDelete;
	
	void Start() {
        m_animator = GetComponent<Animator>();
		m_vec3Target = GameMgr.instance.GetGoldTargetPos();

	}

	public void Init(Vector3 vec3StartPos, Window_Delete winDelete) {
		m_winDelete = winDelete;
		m_vec3FirstPos = new Vector3(vec3StartPos.x, yoffset, 0);
		_transMoveGold.transform.position = vec3StartPos;
	}
	
	void Update() {
		if (!m_bCanMove) {
            MoveToPos(m_vec3FirstPos,MOVE_DOWN_SPEED);
        }
		if (_transMoveGold.transform.position == m_vec3FirstPos) {
            waittime();
			if (startAni == 0) {
                m_animator.SetBool("BeginAni", true);
            }
			if (Disable == 0) {
				DestroySelf();
            }
        }
		if (m_bMoved) {
            sun.gameObject.SetActive(false);
            m_animator.SetBool("BeginAni", false);
            m_bCanMove = true;
            MoveToPos(m_vec3Target,MOVE_UP_SPEED);
        }
		if (_transMoveGold.transform.position == m_vec3Target) {
			if (m_uCount == 0) {
				GameMgr.instance.AddGold();
            }
            m_uCount++;
            m_animator.SetTrigger("IsColor");
			DestroySelf(0.2f);
        }
    }

	public bool CheckOnMouseDown(Vector3 vec3ClickPos) {
		if (Mathf.Abs(Vector3.Distance(_transMoveGold.transform.position, vec3ClickPos)) < 1.0f) {
			m_bMoved = true;
            SoundManager.Instance.ClickCoin();
			GameMgr.instance.AddScore();
			_goGold.transform.position = _transMoveGold.transform.position;
			_goGold.SetActive(true);
			return true;
		}
		return false;
	}

	private void MoveToPos(Vector3 pos,float speed) {
		_transMoveGold.transform.position = Vector3.MoveTowards(_transMoveGold.transform.position, pos, speed * Time.deltaTime);
    }

    public Vector3 GetFirstPos{
        get{
            return m_vec3FirstPos;
        }
    }

    private void waittime() {
		if(Time.realtimeSinceStartup - m_fTime > 1.0f) {
            m_fTime = Time.realtimeSinceStartup;
            startAni--;
            Disable--;
        }
    }

	private void DestroySelf(float time = 0f) {
		m_winDelete.RemoveGold(this);
		Destroy(gameObject, time);
	}
}
