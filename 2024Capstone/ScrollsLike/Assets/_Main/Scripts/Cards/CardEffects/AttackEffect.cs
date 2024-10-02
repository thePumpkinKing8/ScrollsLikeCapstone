using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Effect", menuName = "Effects/Attack Effect")]
public class AttackEffect : CardEffect
{
    [SerializeField] private int damage = 8;
    public override void Effect()
    {
        base.Effect();
        CardGameManager.Instance.EnemyHit(damage);
    }
}
