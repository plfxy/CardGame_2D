using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfigData:CommonConfigData
{
    public int ID
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public string Desc
    {
        get;
        set;
    }
    
    public override void Initialize(ASteinGameDataHolder dataHolder)
    {
        ID = dataHolder.ReadInt("ID");
        key = ID;
        Name = dataHolder.ReadUTF8("Name");
        Desc = dataHolder.ReadUTF8("Desc");
    }
}
