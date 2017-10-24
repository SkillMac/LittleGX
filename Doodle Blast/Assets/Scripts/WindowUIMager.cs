using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowUIMager : MonoBehaviour {
    public static bool hasStartGame = false;
    public ButtonSoundControl m_Sound;
    public ButtonHomeControl m_Home;
    public ButtonBeginGame m_BeginGame;
    public ButtonUndoControl m_Undo;
    public ButtonClearConrol m_Clear;
    public ButtonReStart m_ReStart;
    public PlayEffectAnimation m_Effect;
    public LoadFileData m_ObjMager;
    public Text m_LevName;
    public StartWindow m_Start;
    public Image m_Tip;
    
	void Awake () {
        SetRestartButton(true);
        m_BeginGame.Init(this);
        m_Effect.Init(this);
        m_Undo.Init(this);
        m_Clear.Init(this);
        m_ReStart.Init(this);
        m_Start.Init(this);
        m_Tip.gameObject.SetActive(false);
	}

    void Start()
    {
        m_LevName.text = m_ObjMager.myLevIndex.ToString();
    }

    public void SetRestartButton(bool istrue)
    {
        m_Undo.gameObject.SetActive(istrue);
        m_Clear.gameObject.SetActive(istrue);
        m_ReStart.gameObject.SetActive(!istrue);
    }

    public void InitData()
    {
        m_ObjMager.m_Cup.ReSetAllData();
        SetRestartButton(true);
        m_ObjMager.m_Draw.DeleteAllLines();
        m_Effect.InitScale();
        CDataMager.canDraw = true;
        m_Start.gameObject.SetActive(true);
    }
}
