using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelIOTest : MonoBehaviour {
    public XLSDataHolder skillXLSDataHolder;
    public ASteinGameDataHolder skillDataHolder;
    // Use this for initialization
    void Start () {
        using (FileStream file = new FileStream(Application.dataPath + "\\Spec\\Skill.xls",FileMode.Open))
        {
            skillXLSDataHolder = new XLSDataHolder(file);
        }
        skillXLSDataHolder.ReadSheet("Skill");
        ConfigSheet<SkillConfigData> skillConfigSheet = new ConfigSheet<SkillConfigData>(skillXLSDataHolder);
        Debug.LogError(skillConfigSheet.GetData(2).Desc);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
