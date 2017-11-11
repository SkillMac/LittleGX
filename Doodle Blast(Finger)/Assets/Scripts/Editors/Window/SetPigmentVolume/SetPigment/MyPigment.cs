using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPigment : MonoBehaviour {
    private Image m_Pigment;
    private IEnumerator m_IEnumerator;
    // Use this for initialization
    void Start () {
        m_Pigment = GetComponent<Image>();
    }
	
    public void SetImageValue(float value)
    {
        value = Mathf.Clamp01(value);
        if (m_IEnumerator != null)
            StopCoroutine(m_IEnumerator);
        m_IEnumerator = WaitSomeTime(value);
        StartCoroutine(m_IEnumerator);
    }

    private IEnumerator WaitSomeTime(float value)
    {
        float temp = value - m_Pigment.fillAmount;
        for (int i =0;i< 20;i++)
        {
            m_Pigment.fillAmount += temp / 20;
            yield return new WaitForEndOfFrame();
        }
        m_Pigment.fillAmount = value;
    }
}
