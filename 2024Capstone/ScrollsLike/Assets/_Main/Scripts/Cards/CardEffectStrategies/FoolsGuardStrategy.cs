using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FoolsGuardStrategy", menuName = "SOs/CardStrategy/FoolsGuardStrategy")]
public class FoolsGuardStrategy : CardEffectStrategy
{
    private void OnEnable()
    {
        _event = _cardEventData.PlayerHit;
    }
    public override void ApplyEffect(ICardEffectable target,int value ,CardData card)
    {
        target.ApplyEffect(CardEffectType.Damage,TrackingManager.Instance.StrikesPlayed , card);
        Debug.Log(TrackingManager.Instance.StrikesPlayed);
    }

}
