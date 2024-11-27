using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "BaseStanceTrigger", menuName = "StanceEffects/BaseStanceTrigger")]
public class StanceTrigger : ScriptableObject
{
    protected StanceData _cardsData;
    public List<CardEffect> Effects { get { return _effects; } }

    public bool Temp { get { return _isTemp; } }
    [SerializeField] protected bool _isTemp;

    public UnityEvent Event { get { return Effects[0].CardEffectors[0].Strategy.Event; } }

    [SerializeField] private List<CardEffect> _effects;



   // [SerializeField] protected List<CardEffect> _effectsFromTrigger;
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
       // EffectManager.Instance.ActivateEffect(_effectsFromTrigger);
    }
    
}
