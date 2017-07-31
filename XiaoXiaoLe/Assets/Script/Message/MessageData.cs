using UnityEngine;
using System.Collections;

public class MessageData
{

    public uint type;
    public object data;
    public MessageData(uint type, object data)
    {
        this.type = type;
        this.data = data;
    }
}
public struct Massage
{
    public string data;
}
