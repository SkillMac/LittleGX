using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataClass {
    private static LoadDataClass instance;
    public static LoadDataClass Instace{
        get{
            if(instance == null){
                instance = new LoadDataClass();
            }
            return instance;
        }
    }
    private const int SPRITENUM = 25;
    private const string myPath = "My/";
    private const string goldPath = "Gold/";
    private const string enemyPath = "Enemy/";

    public static List<Sprite> lstAllMySprites = new List<Sprite>();
    public static List<Sprite> lstAllGoldSprites = new List<Sprite>();
    public static List<Sprite> lstAllEnemySprites = new List<Sprite>();

    private int[] rolePob = { 9, 10, 6};
    private int[] levPob = { 23, 2 };
    private int[] creatPob = { 4, 4, 2 };
    public static List<int> lstRole = new List<int>();
    public static List<int> lstLev = new List<int>();
    public static List<int> lstCreat = new List<int>();

    public void Init()
    {
        LoadDataByPath();
        InitLsts();
    }
    private void LoadDataByPath()
    {
        for(int i = 1; i<=SPRITENUM;i++)
        {
            AddLst(myPath, i, lstAllMySprites);
            AddLst(goldPath, i, lstAllGoldSprites);
            AddLst(enemyPath, i, lstAllEnemySprites);
        }
    }

    private void InitLsts()
    {
        InitLstPob(rolePob, lstRole);
        InitLstPob(levPob, lstLev);
        InitLstPob(creatPob, lstCreat);
    }

    public GameObject CreatGobjByID(int id,Vector2 pos,GameObject prefab,GameObject obj)
    {
        if(obj == null)
            obj = Object.Instantiate(prefab, pos, Quaternion.identity);
        SpriteRenderer sr = obj.transform.GetChild(0).GetComponent<SpriteRenderer>() ;
        PieceNum pn = obj.GetComponent<PieceNum>();

        int type = 0;
        int lev = 0;
        type = lstRole[id];
        lev = lstLev[id];
        pn.GetCurrentLev = lev;
        pn.Type = (PieceType)type;
        switch (type)
        {
            case 0:
                sr.sprite = lstAllMySprites[lev];
                break;
            case 1:
                sr.sprite = lstAllGoldSprites[lev];
                break;
            case 2:
                sr.sprite = lstAllEnemySprites[lev];
                //Object.Destroy(obj.GetComponent<Animator>());
                break;
        }
        return obj;
    }

    public void InitCreatElement(GameObject obj,int id,int HighDex,RuntimeAnimatorController control)
    {
        SpriteRenderer sr = obj.transform.GetChild(0).GetComponent<SpriteRenderer>();
        PieceNum pn = obj.transform.GetComponent<PieceNum>();
        int type = lstCreat[id];
        int lev = 0;
        pn.Type = (PieceType)type;
        switch (type)
        {
            case 0:
                sr.sprite = lstAllMySprites[0];
                //AddComponent(obj, control);
                break;
            case 1:
                sr.sprite = lstAllGoldSprites[0];
                //AddComponent(obj, control);
                break;
            case 2:
                {
                    if (HighDex >= 3)
                    {
                        int dex = Random.Range(0, HighDex);
                        int rangenum = Random.Range(1, 100);
                        //红色高等级物体出现的概率
                        if (rangenum >= 1 && rangenum <= 30) {
                            lev = HighDex - 1; }
                        else { lev = dex; }
                    }
                    sr.sprite = lstAllEnemySprites[lev];
                }
                break;
        }
        Debug.Log(type + "******" + lev+"*****"+ HighDex);
        pn.GetCurrentLev = lev;
    }

    private void AddComponent(GameObject obj,RuntimeAnimatorController control)
    {
        if(obj.GetComponent<Animator>() == null)
        {
            Animator anim = obj.AddComponent<Animator>();
            anim.runtimeAnimatorController = control;
        }
    }
    private void AddLst(string path,int index,List<Sprite> lst)
    {
        Sprite tempMy = Resources.Load<Sprite>(path + index);
        if (!lst.Contains(tempMy))
        {
            lst.Add(tempMy);
        }
    }

    private void InitLstPob(int[] pob,List<int> lst)
    {
        for (int i = 0; i < pob.Length; i++)
        {
            for (int j = 0; j < pob[i]; j++)
            {
                lst.Add(i);
            }
        }
    }
}

public enum PieceType
{
    My,
    Gold,
    Enemy,
}
