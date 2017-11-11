using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayEffectAnimation : MonoBehaviour {
    private Animation m_Animation;
    private WindowUIMager m_UIMager;
    private IEnumerator m_PlayFunc;
    private Image m_Image;
    private int m_count;
    private float m_Length;
    private bool isBegin = false;

    void Awake () {
        m_Animation = GetComponent<Animation>();
        m_Image = GetComponent<Image>();
	}

    void Update()
    {
        if(isBegin)
        {
            m_Length = 1 - m_Image.fillAmount;
            if (m_Length >= (1f / m_UIMager.m_Count) * m_count
                && m_Length < (1f / m_UIMager.m_Count) * (m_count+1))
            {
                m_UIMager.SetActiveEffect(m_UIMager.m_Count -m_count-1,true);
                if(m_count < m_UIMager.m_Count -1)
                    m_count++;
            }
            if (m_Length == 1)
            {
                m_count = 0;
                m_Length = 0;
                isBegin = false;
                UnableEffect();
            }
        }
    }

    private void UnableEffect()
    {
        for(int i=0;i< m_UIMager.m_Count;i++)
        {
            m_UIMager.SetActiveEffect(i,false);
        }
    }

    private IEnumerator Play()
    {
        m_Animation.Play("Effect");
        isBegin = true;
        yield return new WaitForSeconds(m_Animation.GetClip("Effect").length);
        m_UIMager.m_ObjMager.SetAllSpheresType();
        m_UIMager.m_ObjMager.m_Cup.enabled = true;
    }

    private void StopAnimation()
    {
        StopCoroutine(m_PlayFunc);
        m_Animation.Stop();
        m_count = 0;
        m_Length = 0;
        isBegin = false;
        UnableEffect();
        transform.GetComponent<Image>().fillAmount = 1;
    }

    public void Init(WindowUIMager m_UIMager)
    {
        this.m_UIMager = m_UIMager;
    }
    
    public void InitScale()
    {
        StopAnimation();
    }

    public void PlayAnimation()
    {
        m_PlayFunc = Play();
        StartCoroutine(m_PlayFunc);
    }
}
