using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionProsessor {
    #region Singleton

    private static BattleActionProsessor _IBattleActionProsessor;

    public static BattleActionProsessor GetInstance()
    {
        if (_IBattleActionProsessor == null)
        {
           _IBattleActionProsessor= new BattleActionProsessor();
        }
        return _IBattleActionProsessor;
    }

    #endregion

    public 
}
