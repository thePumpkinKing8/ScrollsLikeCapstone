using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Effects/Heal Effect")]
public class HealEffect : CardEffect
{
    [SerializeField] private int _amountHealed = 5;
    public override void Effect()
    {
        base.Effect();
        HealthManager.Instance.PlayerHealth += _amountHealed;
    }
}
