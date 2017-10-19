using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWaterValue : MonoBehaviour {
    private JudgeCanWin m_Window;
    private Vector3 oldWaterLevelPos;
    private Vector3 oldLocalScale;
    public void Init(JudgeCanWin window)
    {
        m_Window = window;
    }

    public void InitData()
    {
        m_Window.waterLevel.transform.localPosition = oldWaterLevelPos;
        transform.localScale = oldLocalScale;
    }
	// Use this for initialization
	void Start () {
        oldWaterLevelPos = m_Window.waterLevel.transform.localPosition;
        Vector3 temp = transform.localScale;
        temp.y = 0;
        transform.localScale = temp;
        oldLocalScale = temp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToWaterLevel(float offset)
    {
        float temp = (offset - transform.position.y) / transform.parent.localScale.y;
        StartCoroutine(MoveOfFrame(Mathf.Abs(temp)));
    }

    IEnumerator MoveOfFrame(float distance)
    {
        Vector3 pos = oldWaterLevelPos;
        Vector3 scal = transform.localScale;
        Vector3 targetPos = new Vector3(pos.x, pos.y + distance, pos.z);
        Vector3 targetScale = new Vector3(scal.x,scal.y + distance / m_Window.waterHeigth,scal.z);

        for (int i = 0; i < 40; i++)
        {
            pos.y += distance / 40.0f;
            scal.y += distance / (m_Window.waterHeigth * 40.0f);
            m_Window.waterLevel.transform.localPosition = pos;
            transform.localScale = scal;
            yield return new WaitForEndOfFrame();
        }
        m_Window.waterLevel.transform.localPosition = targetPos;
        transform.localScale = targetScale;
    }
}
