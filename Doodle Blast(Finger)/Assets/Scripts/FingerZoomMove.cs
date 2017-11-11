using System.Collections;
using UnityEngine;

public class FingerZoomMove : MonoBehaviour {
    public GameObject m_Back;
    public DrawLines m_Draw;
    public GameObject m_Scene;
    private Vector3 oldPostion1;
    private Vector3 oldPosition2;
    private Vector3 tempPos1;
    private Vector3 tempPos2;
    private Camera m_Camera;
    private Vector3 m_CameraOffset;
    private bool isSingle;
    private float cameraWidth;
    private float cameraHeight;
    private IEnumerator m_IEnumerator;

    // Use this for initialization
    void Start () {
        m_Camera = Camera.main;
        cameraWidth = m_Back.GetComponent<SpriteRenderer>().sprite.rect.width * m_Back.transform.lossyScale.x / 200f;
        cameraHeight = m_Back.GetComponent<SpriteRenderer>().sprite.rect.height * m_Back.transform.lossyScale.x / 200f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!CDataMager.canDraw &&!NoviceManager.HasNovice) return;
        if (Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
                isSingle = true;
        }
        if (Input.touchCount > 1)
        {
            if(isSingle || (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began))
            {
                oldPostion1 = m_CameraOffset = m_Camera.ScreenToWorldPoint(Input.GetTouch(0).position);
                oldPosition2 = m_Camera.ScreenToWorldPoint(Input.GetTouch(1).position);
                isSingle = false;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                if(NoviceManager.HasNovice && (Typing.index==3 || Typing.index == 5))
                    ScaleScene();
                else if(NoviceManager.HasNovice && Typing.index == 4)
                    GetMovePos();
                else if(!NoviceManager.HasNovice)
                {
                    ScaleScene();
                    GetMovePos();
                }
            }
        }
    }

    private void ScaleScene()
    {
        tempPos1 = m_Camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        tempPos2 = m_Camera.ScreenToWorldPoint(Input.GetTouch(1).position);

        float currentDis = Vector3.Distance(tempPos1, tempPos2);
        float oldDis = Vector3.Distance(oldPostion1, oldPosition2);
        float scaleOffset = currentDis - oldDis;

        if (Mathf.Abs(scaleOffset) > 0.5f && Mathf.Abs(scaleOffset) < 2.0f)
        {
            if (m_IEnumerator != null)
                StopCoroutine(m_IEnumerator);
            m_IEnumerator = WaitSomeTime(m_Scene.transform.localScale.x + scaleOffset);
            StartCoroutine(m_IEnumerator);

            oldPostion1 = tempPos1;
            oldPosition2 = tempPos2;
        }
    }

    private void GetMovePos()
    {
        tempPos1 = m_Camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        Vector3 pos = m_Scene.transform.position + tempPos1 - m_CameraOffset;
        float offsetX = (m_Back.transform.lossyScale.x - 1.8f) * cameraWidth;
        float offsetY = (m_Back.transform.lossyScale.y - 1.8f) * cameraHeight;
        pos.x = Mathf.Clamp(pos.x, -offsetX, offsetX);
        pos.y = Mathf.Clamp(pos.y, -offsetY, offsetY);
        m_Scene.transform.position = pos;
        m_CameraOffset = tempPos1;
    }

    private IEnumerator WaitSomeTime(float value)
    {
        value = Mathf.Clamp(value, 1, 3);
        float temp = value - m_Scene.transform.localScale.x;
        for (int i = 0; i < 20; i++)
        {
            m_Scene.transform.localScale += (temp / 20) * Vector3.one;
            yield return new WaitForEndOfFrame();
        }
        m_Scene.transform.localScale = Vector3.one * value;
    }
}
