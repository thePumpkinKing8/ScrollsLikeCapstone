using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RegenStrategy", menuName = "SOs/CardStrategy/RegenStrategy")]
public class RegenStrat : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        foreach(TimeSlot slot in CardGameManager.Instance.EnemySlot)
        {
            if (!slot.Active)
            {
                slot.EnemyHeal(EnemyManager.Instance.EnemyHealth / 2);
                return;
            }            
        }
        CardGameManager.Instance.EffectDone();
    }
}
