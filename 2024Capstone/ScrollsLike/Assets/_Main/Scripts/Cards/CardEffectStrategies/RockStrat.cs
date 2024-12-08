using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RockStrategy", menuName = "SOs/CardStrategy/RockStrategy")]
public class RockStrat : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        if(!TrackingManager.Instance.Geology)
            target.ApplyEffect(CardEffectType.Damage, value, card);
        else
            target.ApplyEffect(CardEffectType.Damage, 6, card);
    }
}
