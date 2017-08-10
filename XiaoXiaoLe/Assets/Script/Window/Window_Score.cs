using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Score : MonoBehaviour {
    
    Vector3 pos;
    
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
    }

  
    private void OnEnable()
    {
        StartCoroutine(Close());
    }
    
    IEnumerator Close()
    {
        pos = transform.position;
        
        for(int i =0;i<35;i++)
        {
            pos.y += 0.01f;
            transform.position = pos;

            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
}

public enum ScoreType
{
    m_0 = 0,
    m_1 = 1,
    m_5 = 5,
    m_6 = 6,
    m_7,
    m_8,
    m_9,
}
