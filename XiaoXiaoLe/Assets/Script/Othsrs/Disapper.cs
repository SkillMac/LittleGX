using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapper : MonoBehaviour {

    public Sprite[] allSprite;

    private SpriteRenderer reder;
    private List<Sprite> m_sprite;
    
    private void Awake()
    {
        m_sprite = new List<Sprite>();

        if (allSprite == null) return;

        for(int i =0;i<allSprite.Length;i++)
        {
            m_sprite.Add(allSprite[i]);
        }
    }
    // Use this for initialization
    void Start () {

        reder = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Enable(int dex)
    {
        reder = GetComponent<SpriteRenderer>();
        reder.sprite = allSprite[dex];
    }

    public void Dis()
    {
        transform.position = Vector3.zero;
        reder.color = new Color(1.0f, 1, 1, 1);
        gameObject.SetActive(false);
    }
}
