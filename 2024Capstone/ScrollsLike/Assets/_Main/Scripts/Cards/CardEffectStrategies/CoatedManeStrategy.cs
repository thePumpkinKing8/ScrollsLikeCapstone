using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoatedManeStrategy", menuName = "SOs/CardStrategy/CoatedManeStrategy")]
public class CoatedManeStrategy : CardEffectStrategy
{
    private void OnEnable()
    {
        _event = _cardEventData.EnemyBlocked;
    }
    public override void ApplyEffect(ICardEffectable target, CardData card)
    {
        HealthManager.Instance.ApplyEffect(CardEffectType.Damage, EnemyManager.Instance.DamageBlocked, card);
        
    }
}
