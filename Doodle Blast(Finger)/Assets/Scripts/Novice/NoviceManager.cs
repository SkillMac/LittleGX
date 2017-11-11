using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoviceManager : MonoBehaviour {
    private const float SCALECOEFFICIENT = 1.2f;
    private const float WAITTIME = 4;
    public static bool HasNovice = false;
    public GameObject m_LftFinger;
    public GameObject m_RgtFinger;
    public LoadFileData m_Scene;
    private float timer;

    void Awake()
    {
        CDataMager.canDraw = true;
        if (PlayerPrefs.GetInt("HasNovice") == 0)
        {
            gameObject.SetActive(true);
            HasNovice = true;
            //PlayerPrefs.SetInt("HasNovice", 1);
        }
        else
            gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            m_Scene.transform.localScale += Vector3.one * 0.1f;
        if (Input.GetKeyDown(KeyCode.F2))
            m_Scene.transform.localScale -= Vector3.one * 0.1f;
        if (Input.GetKeyDown(KeyCode.F3))
            m_Scene.transform.localPosition += Vector3.one * 0.1f;
        NoviceEvents();
    }

    private void NoviceEvents()
    {
        switch(Typing.index)
        {
            case 0:
                BeginDraw();
                break;
            case 1:
                WaitSomTime(WAITTIME);
                break;
            case 2:
                WaitSomTime(WAITTIME);
                break;
            case 3:
                JudgeObjScale(true);
                break;
            case 4:
                JudgeObjMove();
                break;
            case 5:
                JudgeObjScale(false);
                break;
            case 6:
                WaitSomTime(WAITTIME);
                break;
            case 7:
                {
                    HasNovice = false;
                    //PlayerPrefs.SetInt("HasNovice", 1);
                    CDataMager.canDraw = true;
                    gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    private void SetInit()
    {
        m_LftFinger.SetActive(false);
        m_RgtFinger.SetActive(false);
        Typing.playAnimation = true;
    }

    private void BeginDraw()
    {
        if (Input.GetMouseButtonUp(0)) //|| Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (m_Scene.m_Draw.transform.childCount > 0)
            {
                CDataMager.canDraw = false;
                SetInit();
            }
        }
    }

    private void WaitSomTime(float time)
    {
        timer += Time.deltaTime;
        if(timer >= time)
        {
            SetInit();
            timer = 0;
        }
    }

    private void JudgeObjScale(bool isScale)
    {
        if(isScale)
        {
            if (m_Scene.transform.localScale.x > SCALECOEFFICIENT && Input.touchCount == 0)
                SetInit();
        }
        else
        {
            if (m_Scene.transform.localScale.x < SCALECOEFFICIENT && Input.touchCount == 0)
                SetInit();
        }
    }

    private void JudgeObjMove()
    {
        if (m_Scene.transform.localPosition != Vector3.zero && Input.touchCount == 0)
            SetInit();
    }
}
