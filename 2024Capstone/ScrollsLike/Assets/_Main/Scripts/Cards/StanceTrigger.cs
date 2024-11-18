using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BaseStanceTrigger", menuName = "StanceEffects/BaseStanceTrigger")]
public class StanceTrigger : ScriptableObject
{
    protected  StanceData _cardsData;

    [SerializeField] protected List<CardEffect> _effectsFromTrigger;
    protected virtual void SetUpTrigger()
    {
        
    }

    public void GetData(StanceData data)
    {
        _cardsData = data;
        SetUpTrigger();
    }
    protected virtual void Trigger()
    {
        EffectManager.Instance.ActivateEffect(_effectsFromTrigger);
    }
    
}
