using UnityEngine;

public class Remora : MonoBehaviour
{
    public GameObject m_Draw;
    
    private void OnMouseEnter()
    {
        m_Draw.GetComponent<DrawLines>().enabled = false;
    }
    private void OnMouseExit()
    {
        if(CDataMager.canDraw)
            m_Draw.GetComponent<DrawLines>().enabled = true;
    }
}
