using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaulStrategy", menuName = "SOs/CardStrategy/MaulStrategy")]
public class MaulStrategy : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        target.ApplyEffect(CardEffectType.Damage, (object)target == HealthManager.Instance ? HealthManager.Instance.Poison : EnemyManager.Instance.Poison , card);
    }
}
