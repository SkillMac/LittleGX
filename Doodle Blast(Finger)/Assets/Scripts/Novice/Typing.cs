using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    private const float WAITTIME = 0.5f;
    public static bool playAnimation = false;
    public static bool playFingerAnim = false;
    public static int index;
    public string[] m_Word;
    private Text m_Text;
    private int count;
    private bool canDo;
    private float timer;
    
	void Start ()
    {
        m_Text = GetComponent<Text>();
        canDo = true;
    }

    void Update()
    {
        if (canDo)
        {
            if(count >= m_Word[index].Length)
            {
                canDo = false;
                playAnimation = false;
                playFingerAnim = true;
                return;
            }
            else
            {
                m_Text.text += m_Word[index][count];
                count++;
            }
        }
        if(playAnimation && !canDo)
        {
            playFingerAnim = false;
            if (index > m_Word.Length - 1) return;
            timer += Time.deltaTime;
            if (timer >= WAITTIME)
            {
                if(index != m_Word.Length - 1)
                {
                    canDo = true;
                    timer = 0;
                    count = 0;
                    m_Text.text = "";
                }
                index++;
            }
        }
    }
}
