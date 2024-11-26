using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaulStrategy", menuName = "SOs/CardStrategy/MaulStrategy")]
public class MaulStrategy : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, CardData card)
    {
        target.ApplyEffect(CardEffectType.Damage, HealthManager.Instance.Poison, card);
    }
}
