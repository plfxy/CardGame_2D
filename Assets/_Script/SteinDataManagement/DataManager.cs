using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager {
    private List<IConfigSheet> _sheetList;

    #region singleton

    private static DataManager _dataManager;

    private DataManager()
    {
    }

    public static DataManager GetInstance()
    {
        if (_dataManager == null)
        {
            _dataManager = new DataManager();
            _dataManager.Initialize();
        }
        return _dataManager;
    }

    #endregion
    
    private void Initialize()
    {
        XLSDataHolder sheetIndexXLSHolder;
        ConfigSheet<SheetIndexConfigData> SheetIndex= new ConfigSheet<SheetIndexConfigData>();
        using (FileStream file = new FileStream(Application.dataPath + "\\Spec\\SheetIndex.xls", FileMode.Open))
        {
            sheetIndexXLSHolder = new XLSDataHolder(file);
        }
        sheetIndexXLSHolder.ReadSheet(0);
        SheetIndex.Initialize(sheetIndexXLSHolder);
        _sheetList = new List<IConfigSheet>();
        for (int i = 0; i < SheetIndex.GetDataLength(); i++)
        {
            IConfigSheet tmpIConfigSheet;
            XLSDataHolder tmpXLSDataHolder;
            System.Type tmpType;
            string curSheetName;

            curSheetName = SheetIndex.GetDataByIndex(i).SheetName;
            tmpType = System.Type.GetType("ConfigSheet`1[" + curSheetName + "ConfigData]");
            tmpIConfigSheet = (IConfigSheet)System.Activator.CreateInstance(tmpType);
            using (FileStream file = new FileStream(Application.dataPath + "\\Spec\\" + curSheetName + ".xls", FileMode.Open))
            {
                tmpXLSDataHolder = new XLSDataHolder(file);
            }
            tmpXLSDataHolder.ReadSheet(0);
            tmpIConfigSheet.Initialize(tmpXLSDataHolder);
            _sheetList.Add(tmpIConfigSheet);
        }
    }

    public IConfigSheet GetSheet(string sheetName)
    {
        for (int i= 0;i < _sheetList.Count; i++){
            if (_sheetList[i].Name == sheetName)
            {
                return _sheetList[i];
            }
        }
        Debug.LogError("Can't Find Sheet Which Name is : "+ sheetName);
        return null;
    }
    
}
