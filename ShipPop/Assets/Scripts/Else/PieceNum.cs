using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceNum : MonoBehaviour {
    
    [System.Serializable]
    public struct NumSprite
    {
        public NumType type;
        public Sprite sprite;
    }

    public NumSprite[] Allsprites;

    private SpriteRenderer sprite;
    
    private NumType m_type;

    public NumType GetnewType
    {
        set
        {
            Setsprite(value);
        }
        get
        {
            return m_type;
        }
    }

    private Dictionary<NumType, Sprite> m_sprites;

    private void Awake()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        m_sprites = new Dictionary<NumType, Sprite>();

        for (int i =0;i<Allsprites.Length;i++)
        {
            if(!m_sprites.ContainsKey(Allsprites[i].type))
            {
                m_sprites.Add(Allsprites[i].type, Allsprites[i].sprite);
            }
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setsprite(NumType newtype)
    {
        m_type = newtype;
        sprite.sprite = m_sprites[newtype];
    }

}

public enum NumType
{
    num0001,
    num0002,
    num0003,
    num0004,
    num0005,
    num0006,
    num0007,
    num0008,
    num0009,
    num0010,
    num0011,
    num0012,
    num0013,
    num0014,
    num0015,
    num0016,
    num0017,
    num0018,
    num0019,
    num0020,
    num0021,
    num0022,
    num0023,
    num0024,
    num0025,
    num0026,
}