using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Effect", menuName = "Effects/Attack Effect")]
public class PlayerDamageEffect : CardEffect
{
    [SerializeField] private int _damage;
    public override void Effect()
    {
        base.Effect();
        CardGameManager.Instance.PlayerHit(_damage);
    }
}
