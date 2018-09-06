using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleUnitInformation {
    public string Name
    {
        get;
        set;
    }

    public int MaxHp
    {
        get;
        set;
    }

    public int CurHp
    {
        get;
        set;
    }

    private List<BattleEffect> _effectList;
    public List<BattleEffect> EffectList
    {
        get
        {
            if (_effectList== null)
            {
                _effectList = new List<BattleEffect>();
            }
            return _effectList;
        }
    }
     
    public abstract int GetNumOfCardsInHand();
}
