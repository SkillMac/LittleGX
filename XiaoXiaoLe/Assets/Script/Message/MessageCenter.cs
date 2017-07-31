using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MessageCenter : MonoBehaviour
{
    static Dictionary<int, IMessageHandler> Dic = new Dictionary<int, IMessageHandler>();

    static Queue<MessageData> Que = new Queue<MessageData>();//消息接收中心
                                                             // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Distribute();
    }

    public static void ReceiveMassage(MessageData data)
    {
        if (!Que.Contains(data))
        {
            Que.Enqueue(data);//入队
        }
    }
    //分发消息
    void Distribute()
    {
        if (Que.Count <= 0) return;

        int[] keys = new int[Dic.Keys.Count];//先分配空间
        Dic.Keys.CopyTo(keys, 0);

        while (Que.Count > 0)
        {

            MessageData md = Que.Dequeue();//出栈给每一个终端发送消息

            for (int i = 0; i < keys.Length; i++)
            {

                Dic[keys[i]].MassageHandler(md.type, md.data);

            }
        }
    }
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="code">脚本唯一ID HasCode</param>
    /// <param name="id">接口ID</param>
    public static void Registed(int code, IMessageHandler id)
    {
        if (!Dic.ContainsKey(code))
        {
            Dic.Add(code, id);
        }
        else
            Debug.Log("This component has be registed!");
    }
    /// <summary>
    /// 注销
    /// </summary>
    /// <param name="com">组件</param>
    public static void Cancel(int com)
    {
        if (Dic.ContainsKey(com))
        {
            Dic.Remove(com);
        }
        else
            Debug.Log("This component is not exist!");
    }

}
