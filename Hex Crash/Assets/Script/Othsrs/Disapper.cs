using UnityEngine;

public class Disapper : MonoBehaviour {
    public Sprite[] allSprite;
    private SpriteRenderer reder;
    
    void Start() {
        reder = GetComponent<SpriteRenderer>();
	}
	
    public void Enable(int dex) {
        reder = GetComponent<SpriteRenderer>();
        reder.sprite = allSprite[dex];
        SoundManager.Instance.Cheers(dex);
    }

    public void Dis() {
        transform.position = Vector3.zero;
        reder.color = new Color(1.0f, 1, 1, 1);
        gameObject.SetActive(false);
    }
}
