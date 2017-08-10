using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Tables
{
    public string[,] datas;

    public Tables(string path)
    {
        Load(path);
    }
    void Load(string path)
    {
        TextAsset ta = Resources.Load<TextAsset>(path);

        string[] buff = ta.text.Split("\n"[0]);

        for (int i = 0; i < buff.Length; i++)
        {
            string[] temp = buff[i].Split("\t"[0]);

            if (datas == null)
            {
                datas = new string[buff.Length, temp.Length];
            }

            for (int j = 0; j < temp.Length; j++)
            {
                datas[i, j] = temp[j];
            }
        }
    }

    //获取行数
    public int GetLineData()
    {
        return datas.GetLength(0);
    }
    //获取lie数
    public int GetLenghtData()
    {
        return datas.GetLength(1);
    }
    /// <summary>
    /// 根据ID获取整行的信息
    /// </summary>
    /// <param name="id">物品ID</param>
    /// <returns></returns>
    public string[] GetDataWithID(string id)
    {
        for (int i = 0; i < datas.GetLength(0); i++)
        {
            if (datas[i, 0].CompareTo(id) == 0)
            {
                string[] temp = new string[datas.GetLength(1)];

                for (int j = 0; j < datas.GetLength(1); j++)
                {
                    temp[j] = datas[i, j];
                }

                return temp;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据ID和下标获取数据
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="id">物品ID</param>
    /// <param name="index">下标</param>
    /// <returns></returns>
    public T GetDataWithIDAndIndex<T>(string id, int index)
    {
        string[] data = GetDataWithID(id);

        if (data == null) return default(T);

        return (T)Convert.ChangeType(data[index], typeof(T));
    }

    /// <summary>
    /// 根据行数的下标获取ID
    /// </summary>
    /// <param name="index">行数下标</param>
    /// <returns></returns>
    public string GetIDWithIndex(int index)
    {
        if (datas.GetLength(0) > index)
        {
            return datas[index, 0];
        }
        return null;
    }

    /// <summary>
    /// 切除字符串中指定的字符
    /// </summary>
    /// <param name="c">要切除的字符</param>
    /// <param name="str">指定的字符串</param>
    /// <returns></returns>
    public int[] GetString(char c, string str)
    {
        if (str == null) return null;

        string temp = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                temp += str[i];
            }
        }

        string[] buff = temp.Split(","[0]);

        int[] buffs = new int[buff.Length];
        for (int i = 0; i < buff.Length; i++)
        {
            buffs[i] = int.Parse(buff[i]);
        }
        return buffs;
    }
}

public enum TableName
{
    prefabtype,
    maps,
    Score,
}
