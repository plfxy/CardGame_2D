using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigSheet
{
    string Name { get; }
    void Initialize(ASteinGameDataHolder dataHolder);
}

public class ConfigSheet<T> : IConfigSheet where T : CommonConfigData, new() {
    private List<T> data;
    private string _name;

    public string Name
    {
        get{
            return _name;
        }
    }

    public ConfigSheet()
    {
    }

    public void Initialize(ASteinGameDataHolder dataHolder)
    {
        _name = dataHolder.DataName;
        data = new List<T>();
        while (dataHolder.MoveNext())
        {
            T curData;
            curData = new T();
            curData.Initialize(dataHolder);
            data.Add(curData);
        }
    }

    public T GetDataByKey(int key)
    {
        for (int i =0;i < data.Count; i++)
        {
            if (data[i].Key == key)
            {
                return data[i];
            }
        }
        Debug.LogError("Can't find sheet [" + _name + ": key = " + key + "]");
        return null;
    }

    public T GetDataByIndex(int index)
    {
        if (GetDataLength() >= index)
        {
            return data[index];
        }
        Debug.LogError("Can't find sheet [" + _name + ": index = " + index + "]");
        return null;
    }

    public int GetDataLength()
    {
        return data.Count;
    }
}
