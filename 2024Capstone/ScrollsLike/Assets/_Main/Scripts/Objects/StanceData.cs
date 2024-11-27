using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CardData", menuName = "Stance")]
public class StanceData : CardData
{
    public bool IsActive { get; private set; }

    public void Activate()
    {
        IsActive = true;
        SetUp();
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void SetUp()
    {
        foreach (StanceTrigger effect in _triggeredEffects)
        {
            effect.GetData(this);
        }
    }
    /*
    public List<StanceEffect> StaticEffects //effects that are passivly active
    {
        get
        {
            foreach (StanceEffect effect in _staticEffects)
            {
                effect.GetData(this);
            }
            return _staticEffects;
        }
    }
    [SerializeField] private List<StanceEffect> _staticEffects = new List<StanceEffect>();
    */
    public List<StanceTrigger> TriggeredEffects //effects that are passivly active
    {
        get
        { 
            return _triggeredEffects;
        }
    }
    [SerializeField] private List<StanceTrigger> _triggeredEffects = new List<StanceTrigger>();
}
