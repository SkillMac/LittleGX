using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Editor : MonoBehaviour {
    public ButtonCube m_Cube;
    public ButtonSphere m_Sphere;
    public ButtonCup m_Cup;
    public ButtonBottle m_Bottle;
    public GameObject mask;

    void Awake()
    {
        CDataMager.getInstance.Init();
        CDataMager.canDraw = false;
        m_Cube.Init(this);
        m_Sphere.Init(this);
        m_Cup.Init(this);
        m_Bottle.Init(this);

        float width = Screen.height * 1.5f;
        if(width > Screen.width)
        {
            CDataMager.screenWidth = Screen.width * CDataMager.scaleWidth;
        }
        else
        {
            CDataMager.screenWidth = width * CDataMager.scaleWidth;
        }

        if(CDataMager.getInstance.allSphereSprite ==null)
        {
            CDataMager.getInstance.allSphereSprite = Resources.LoadAll<Sprite>("Images/Spheres");
        }
        if(CDataMager.getInstance.allCubeSprite == null)
        {
            CDataMager.getInstance.allCubeSprite = Resources.LoadAll<Sprite>("Images/Cubes");
        }
    }
}
