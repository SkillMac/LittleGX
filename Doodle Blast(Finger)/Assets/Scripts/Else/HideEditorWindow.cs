using UnityEngine;

public class HideEditorWindow : MonoBehaviour {
    public GameObject obj;

    void OnMouseDown()
    {
        obj.SetActive(false);
    }
}
