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
