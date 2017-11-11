using System.Collections.Generic;
using UnityEngine;

public class CDataMager
{
    public const float cupPositionY = -2.5f;
    public static bool isFirstLoadGame = true;
    public static float scaleWidth = 1200f / Screen.width;
    public static float scaleHeight = 800f / Screen.height;
    public static float screenWidth;
    public static bool canDraw = true;
    private static CDataMager instance;
    public List<SphereMager> allSpheres;
    public List<SphereWindowMager> allSphereWindows;
    public List<CubeMager> allCubes;
    public int myPigmentVolume;
    public float cupPositionX;
    public float winLinePositionY;
    public CupMager myCup;
    public PigmentMager myPigment;
    public ButtonCreat mySpheres;
    public Sprite[] allSphereSprite;
    public Sprite[] allCubeSprite;

    private CDataMager() { }
   
    public void Init()
    {
        allSpheres = new List<SphereMager>();
        allSphereWindows = new List<SphereWindowMager>();
        allCubes = new List<CubeMager>();
        myPigmentVolume = 10;
        cupPositionX = 0;
        winLinePositionY = 0;
    }

    public void DestroyAllData()
    {
        for (int i = 0; i < allCubes.Count; i++)
        {
            Object.Destroy(allCubes[i].gameObject);
        }
        mySpheres.InitAllData();
        myCup.InitAllCupData();
        myPigment.InitAllData();
    }
    public static CDataMager getInstance
    {
        get
        {
            if (instance == null)
                instance = new CDataMager();
            return instance;
        }
    }
}
