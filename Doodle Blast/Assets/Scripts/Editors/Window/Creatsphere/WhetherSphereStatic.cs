using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhetherSphereStatic : MonoBehaviour {
    private Vector3 tempPosition;
    private float tempTimer;
    private bool isStatic;
    private bool tempIsStatic;

    public bool IsStatic
    {
        get { return isStatic; }
    }

    void OnEnable()
    {
        isStatic = false;
        tempIsStatic = false;
        tempTimer = 0;
        tempPosition = Vector3.zero;
    }

	void Update () {
        SetEnable();
        WhetherStatic();
    }
    
    private void WhetherStatic()
    {
        tempTimer += Time.deltaTime;
        if (tempTimer > 0.8f)
        {
            tempTimer = 0;
            if (Mathf.Abs(Vector3.Distance(transform.position, tempPosition)) < 0.1f)
                tempIsStatic = true;
            else
                tempPosition = transform.position;
        }
        if(tempIsStatic)
        {
            if(tempTimer > 0.5f)
            {
                if (Mathf.Abs(Vector3.Distance(transform.position, tempPosition)) < 0.1f)
                    isStatic = true;
                tempIsStatic = false;
            }
        }
    }

    private void SetEnable()
    {
        if (Mathf.Abs(transform.position.x) >= CDataMager.scaleWidth * Screen.width / 200.0f || transform.position.y <= -CDataMager.scaleHeight * Screen.height / 200.0f)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void SetRigidbody2D(RigidbodyType2D type)
    {
        transform.GetComponent<Rigidbody2D>().bodyType = type;
    }
}
