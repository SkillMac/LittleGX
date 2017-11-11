using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoviceAnimation : MonoBehaviour {
    private Animator m_Animator;
    private string m_Name;
    
	void Start () {
        m_Animator = GetComponent<Animator>();
        string str = ((AllParamenters)(2)).ToString();
	}
	
	void Update ()
    {
		if(Typing.playFingerAnim)
        {
            m_Name = ((AllParamenters)(Typing.index)).ToString();
            m_Animator.SetBool(m_Name, true);
        }
        else
        {
            if(m_Name !=null)
                m_Animator.SetBool(m_Name, false);
        }
	}
}

public enum AllParamenters
{
    isDraw,
    isBottle,
    isUndo,
    isBigger,
    isMove,
    isSmaller,
    isBegin,
}
