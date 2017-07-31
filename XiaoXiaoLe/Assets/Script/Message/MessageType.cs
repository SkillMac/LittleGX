using UnityEngine;
using System.Collections;

public class MessageType
{

    public const uint SYSTEM = 0;

    private const uint MOUSE = 1000;
    public const uint MOUSE_DOWN = MOUSE + 1;
    public const uint MOUSE_UP = MOUSE_DOWN + 1;
    public const uint MOUSE_UP_CREAT = MOUSE_UP + 1;

    private const uint UI = 2000;
    public const uint UI_ADDGOLD = UI + 1;
    public const uint UI_ADDSCORE = UI_ADDGOLD + 1;
}
