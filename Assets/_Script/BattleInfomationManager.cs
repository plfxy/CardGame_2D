using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInformationManager {
    #region Singleton

    private static BattleInformationManager _IBattleInformationManager;

    public static BattleInformationManager GetInstance()
    {
        if (_IBattleInformationManager == null)
        {
            _IBattleInformationManager = new BattleInformationManager();
        }
        return _IBattleInformationManager;
    }

    #endregion

    private BattleUnitInformation PlayerInfomation, EnemyInformation;

}
