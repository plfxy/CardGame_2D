using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelIOTest : MonoBehaviour {
    public XLSDataHolder skillXLSDataHolder;
    public ASteinGameDataHolder skillDataHolder;
    // Use this for initialization
    void Start () {
        /*using (FileStream file = new FileStream(Application.dataPath + "\\Spec\\Skill.xls",FileMode.Open))
        {
            skillXLSDataHolder = new XLSDataHolder(file);
        }
        skillXLSDataHolder.ReadSheet("Skill");*/
        //ConfigSheet<SkillConfigData> skillConfigSheet = new ConfigSheet<SkillConfigData>(skillXLSDataHolder);
        //Debug.LogError("the type name is:" + skillConfigSheet.GetType() + ".");
        //Debug.LogError(skillConfigSheet.GetData(2).Desc);
        //System.Type.GetType()
        //Debug.LogError(System.Reflection.Assembly.GetAssembly(typeof(ConfigSheet)));
        //object a = System.Activator.CreateInstance("ConfigSheet`1[SkillConfigData]");

        //object a = System.Activator.CreateInstance(System.Type.GetType("ConfigSheet`1[SkillConfigData]"));
        //Debug.LogError(a);
        Debug.LogError(((ConfigSheet<SkillConfigData>)DataManager.GetInstance().GetSheet("Skill")).GetDataByKey(2).Desc);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
