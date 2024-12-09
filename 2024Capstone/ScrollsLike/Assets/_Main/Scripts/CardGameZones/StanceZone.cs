using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StanceZone : Singleton<StanceZone>
{
    private GameCard _stance;
    private StanceData _stanceData;
    private Action _action;
    public void AddStance(StanceData data)
    {
        BlakesAudioManager.Instance.PlayAudio("CardPlace");
        if(_stance != null)
        {
            RemoveEffect();
            _stanceData.Deactivate();
            _stance.OnDeSpawn();           
        }
        _stance = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
        _stanceData = data;
        AddStatus();
        _stance.ReferenceCardData = data;
        _stance.transform.SetParent(transform);
        _stance.transform.position = Vector3.zero;
        CardGameManager.Instance.EffectDone();  
    }

    public void AddStatus()
    {
        foreach(StanceTrigger trigger in _stanceData.TriggeredEffects)
        {
            HealthManager.Instance.AddEffect(trigger);
        }      
    }

    public void RemoveEffect()
    {
        foreach (StanceTrigger trigger in _stanceData.TriggeredEffects)
        {
            HealthManager.Instance.RemoveEffect(trigger);
        }
        _stanceData.Deactivate();
    }

}
