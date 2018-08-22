using UnityEngine;
using System.Collections;

public abstract class ASteinGameDataHolder
{
    protected const string exceptionMessageFormat = "DataForm {0} DO NOT have data {1}";
    protected const int NONE_DATA_KEY = -1;

    public abstract string DataName
    {
        get;
        set;
    }

    public abstract void Release();

    public abstract bool MoveNext();

    public abstract byte ReadByte(string name);

    public abstract sbyte ReadSByte(string name);

    public abstract int ReadInt(string name);

    public abstract long ReadLong(string name);

    public abstract string ReadUTF8(string name);

    public abstract float ReadFloat(string name);

    public virtual bool ReadBool(string name)
    {
        byte _boolVale = ReadByte(name);
        return _boolVale == 1;
    }

    /// <summary>
    ///读取毫秒（ms）时间
    /// </summary>
    /// <returns>The MS time.</returns>
    /// <param name="name">Name.</param>
    public virtual float ReadMSTime(string name)
    {
        return (float)ReadLong(name) / 1000.0f;
    }

    public virtual float ReadPercentValue(string name)
    {
        return (float)ReadLong(name) / 100.0f;
    }

    public virtual int[] ReadIdList(string name, char splitChar = ',')
    {
        string str = ReadUTF8(name);

        if (str == null || str.Equals(""))
            return null;
        string[] splitList = str.Split(new char[] { splitChar });
        int[] idArr = new int[splitList.Length];
        for (int i = 0; i < splitList.Length; ++i)
        {
            idArr[i] = int.Parse(splitList[i]);
        }
        return idArr;
    }

    public virtual string[] ReadStringList(string name, char splitChar = ',')
    {
        string str = ReadUTF8(name);

        if (str == null || str.Equals(""))
            return null;
        string[] splitList = str.Split(new char[] { splitChar });
        return splitList;
    }
}

