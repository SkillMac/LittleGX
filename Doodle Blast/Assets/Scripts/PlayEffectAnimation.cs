using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayEffectAnimation : MonoBehaviour {
    private Animation m_Animation;
    private WindowUIMager m_UIMager;
    private IEnumerator m_PlayFunc;

    void Start () {
        m_Animation = GetComponent<Animation>();
	}

    private IEnumerator Play()
    {
        m_Animation.Play("Effect");
        yield return new WaitForSeconds(m_Animation.GetClip("Effect").length);
        m_UIMager.m_ObjMager.SetAllSpheresType();
        m_UIMager.m_ObjMager.m_Cup.enabled = true;
    }

    private void StopAnimation()
    {
        StopCoroutine(m_PlayFunc);
        m_Animation.Stop();
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
