using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSheet<T> where T : CommonConfigData,new() {
    private List<T> data;
    private string name;

    public ConfigSheet(ASteinGameDataHolder dataHolder)
    {
        name = dataHolder.DataName;
        data = new List<T>();
        while (dataHolder.MoveNext())
        {
            T curData;
            curData = new T();
            curData.Initialize(dataHolder);
            data.Add(curData);
        }
    }

    public T GetData(int key)
    {
        for (int i =0;i < data.Count; i++)
        {
            if (data[i].Key == key)
            {
                return data[i];
            }
        }
        Debug.LogError("Can't find sheet [" + name + ": key = " + key + "]");
        return null;
    }
}
