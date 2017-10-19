using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUnableLater : MonoBehaviour {
    public float timer;
    public PigmentWindow m_Window;

    void OnEnable()
    {
        SetUnable();
    }
   
    private void SetUnable()
    {
        StartCoroutine(WaitSomeTime());
    }

    private IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(timer);
        CDataMager.canDraw = true;
        m_Window.gameObject.SetActive(true);
        m_Window.m_Mager.m_Draw.enabled = true;
        gameObject.SetActive(false);
    }
}
