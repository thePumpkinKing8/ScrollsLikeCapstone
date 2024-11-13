using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CardData", menuName = "Stance")]
public class StanceData : CardData
{
    private bool _isActive;

    public void Activate()
    {
        _isActive = true;
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
    public List<StanceEffect> TriggeredEffects //effects that are passivly active
    {
        get
        {
            foreach (StanceEffect effect in _triggeredEffects)
            {
                effect.GetData(this);
            }
            return _triggeredEffects;
        }
    }
    [SerializeField] private List<StanceEffect> _triggeredEffects = new List<StanceEffect>();
}
