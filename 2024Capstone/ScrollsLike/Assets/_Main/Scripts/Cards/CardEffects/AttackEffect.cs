using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Effect", menuName = "Effects/Attack Effect")]
public class AttackEffect : CardEffect
{
    [SerializeField] private int damage = 8;
    public override void Effect(TimeSlot target = null)
    {
        base.Effect();
        target.EnemyHit(damage);
    }
}
