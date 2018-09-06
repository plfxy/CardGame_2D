using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetIndexConfigData : CommonConfigData
{
    public int ID
    {
        get;
        set;
    }

    public string SheetName
    {
        get;
        set;
    }

    public override void Initialize(ASteinGameDataHolder dataHolder)
    {
        ID = dataHolder.ReadInt("ID");
        key = ID;
        SheetName = dataHolder.ReadUTF8("SheetName");
    }
}
