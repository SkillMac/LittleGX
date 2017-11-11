using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WindowUIMager : MonoBehaviour {
    public ButtonSoundControl m_Sound;
    public ButtonHomeControl m_Home;
    public ButtonBeginGame m_BeginGame;
    public ButtonUndoControl m_Undo;
    public ButtonClearConrol m_Clear;
    public ButtonReStart m_ReStart;
    public ButtonReturnState m_ReState;
    public PlayEffectAnimation m_Effect;
    public LoadFileData m_ObjMager;
    public Text m_LevName;
    public Image m_Tip;
    public GameObject m_EffectObj;
    public GameObject prefabSmogEffect;
    [HideInInspector]
    public int m_Count;
    private List<GameObject> allSmogs = new List<GameObject>();
    
	void Awake () {
        SetRestartButton(true);
        m_BeginGame.Init(this);
        m_Effect.Init(this);
        m_Undo.Init(this);
        m_Clear.Init(this);
        m_ReState.Init(this);
        m_ReStart.Init(this);
        m_Tip.gameObject.SetActive(false);
        CreatSmogEffect();
    }

    void Start()
    {
        m_LevName.text = m_ObjMager.myLevIndex.ToString();
    }

    private void CreatSmogEffect()
    {
        float temp = prefabSmogEffect.GetComponent<RectTransform>().rect.width;
        m_Count = (int)(m_Effect.gameObject.GetComponent<RectTransform>().rect.width / temp);
        for(int i =0;i< m_Count; i++)
        {
            GameObject obj = Instantiate(prefabSmogEffect, m_Effect.transform);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * temp - 10f, 0);
            obj.SetActive(false);
            allSmogs.Add(obj);
        }
    }

    public void SetRestartButton(bool istrue)
    {
        m_Undo.gameObject.SetActive(istrue);
        m_Clear.gameObject.SetActive(istrue);
        m_ReStart.gameObject.SetActive(!istrue);
    }

    public void InitData()
    {
        m_BeginGame.gameObject.SetActive(true);
        m_ObjMager.m_Cup.ReSetAllData();
        SetRestartButton(true);
        m_Effect.InitScale();
        SetEffect(false);
        CDataMager.canDraw = true;
    }

    public void InitDatas()
    {
        InitData();
        m_ObjMager.m_Draw.DeleteAllLines();
    }

    public void SetEffect(bool isBegin)
    {
        m_Effect.gameObject.SetActive(isBegin);
        m_EffectObj.gameObject.SetActive(!isBegin);
    }

    public void SetActiveEffect(int index,bool istrue)
    {
        allSmogs[index].SetActive(istrue);
    }
}
