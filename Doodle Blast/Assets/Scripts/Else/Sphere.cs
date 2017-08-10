using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

    private bool IsStatic;

    public bool GetStatic
    {
        get
        {
            return IsStatic;
        }
    }

    
    Vector3 pos;

    float timer;

	// Use this for initialization
	void Start () {
		
	}
    private void OnEnable()
    {
        timer = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update () {
        
        Waitseconds();
    }

    //等待一秒判断位置是否相同
   void Waitseconds()
    {
        if(Time.realtimeSinceStartup - timer >1.0f)
        {
            timer = Time.realtimeSinceStartup;

            if (Mathf.Abs(Vector3.Distance(transform.position,pos)) < 0.1f)
            {
                IsStatic = true;
            }
            else
            {
                pos = transform.position;
            }

        }
    }
   

}
