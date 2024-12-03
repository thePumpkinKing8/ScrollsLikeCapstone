using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EntrenchStrategy", menuName = "SOs/CardStrategy/EntrenchStrategy")]
public class EntrenchStrategy : CardEffectStrategy
{
    
    private void OnEnable()
    {
        _event = _cardEventData.DrawPhaseStartEvent;
        
    }
    public override void ApplyEffect(ICardEffectable target,int value ,CardData card)
    {
        target.ApplyEffect(CardEffectType.Block, TrackingManager.Instance.PreviousBlock, card);
        Debug.Log(TrackingManager.Instance.PreviousBlock);
    }

}
