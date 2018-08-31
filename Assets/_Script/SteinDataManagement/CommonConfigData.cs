using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonConfigData {
    public int Key
    {
        get
        {
            return key;
        }
    }
    protected int key;

    public CommonConfigData()
    {
    }

    public abstract void Initialize(ASteinGameDataHolder dataHolder);
}
