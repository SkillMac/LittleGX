using UnityEngine;
using System.Collections;

public interface IMessageHandler
{

    //接口内的方法默认为public，不能自己设置类型

    void MassageHandler(uint type, object data);//数据的类型和参数
}