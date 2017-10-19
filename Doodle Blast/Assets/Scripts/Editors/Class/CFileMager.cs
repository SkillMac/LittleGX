using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CFileMager {
    private static CFileMager instance;

    private CFileMager() { }

    public static CFileMager GetInstance
    {
        get
        {
            if (instance == null)
                instance = new CFileMager();
            return instance;
        }
    }

    public CAllLevsData ReadFiles(string path)
    {
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        string strRead = sr.ReadLine();
        CAllLevsData cld = new CAllLevsData();
        if(strRead !=null)
            cld = JsonUtility.FromJson<CAllLevsData>(strRead);
        sr.Close();
        sr.Dispose();
        return cld;
    }

    public void WriteFiles(CAllLevsData cld,CLevel lev,string path)
    {
        cld.allEditorLevs.Add(lev);
        cld.allEditorLevCount++;
        string strSave = JsonUtility.ToJson(cld);
        FileStream fs = new FileStream(path, FileMode.Open);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(strSave);
        sw.Close();
        sw.Dispose();
        Debug.Log("Save Success!!!");
    }

    public void WriteFiles(CAllLevsData cld,string path)
    {
        string strSave = JsonUtility.ToJson(cld);
        FileStream fs = new FileStream(path, FileMode.Open);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(strSave);
        sw.Close();
        sw.Dispose();
    }
}
