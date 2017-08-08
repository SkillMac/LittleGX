using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnim : MonoBehaviour {

    public AnimationClip clearAnimation;

    public Window_Spheres pbj;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WaitClear()
    {
        StartCoroutine(Clear());
    }
    private IEnumerator Clear()
    {
        Animator an = GetComponent<Animator>();

        if(an)
        {
            an.Play(clearAnimation.name);

            DrawLine.Instance.SetBool();

            yield return new WaitForSeconds(clearAnimation.length);

            pbj.SetRigidbody();

            yield return new WaitForSeconds(0.1f);

            pbj.IsBegin = true;
        }
    }
}
