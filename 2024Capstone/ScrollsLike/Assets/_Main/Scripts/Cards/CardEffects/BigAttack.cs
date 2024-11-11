using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Big Attack", menuName = "Effects/Big Attack")]
public class BigAttack : CardEffect
{
    [SerializeField] private int _selfDamage = 4;
    [SerializeField] private int enDamage = 11;
    public override void Effect(TimeSlot target = null)
    {
        base.Effect();
        HealthManager.Instance.PlayerHit(_selfDamage);
        target.EnemyHit(enDamage);
    }
}
