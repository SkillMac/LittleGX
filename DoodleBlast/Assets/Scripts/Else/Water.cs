using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public GameObject m_water;

    private float m_max = 1.938f;

	// Use this for initialization
	void Start () {

        m_water.transform.localScale = new Vector3(1, 0, 1);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(float y)
    {
        StartCoroutine(move(Mathf.Abs(y- transform.position.y)));
    }

    IEnumerator move(float y)
    {
        Vector3 pos = transform.position;
        Vector3 scal = m_water.transform.localScale;

        for (int i =0;i<40;i++)
        {
            pos.y += y / 40.0f;
            scal.y += (y * 1.17f)/ (m_max * 40.0f);//物体的最大尺寸

            transform.position = pos;


            m_water.transform.localScale = scal;

            yield return new WaitForEndOfFrame();
        }
    }
}
