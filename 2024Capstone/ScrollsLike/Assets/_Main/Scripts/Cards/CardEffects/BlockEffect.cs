using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Effect", menuName = "Effects/Block Effect")]
public class BlockEffect : CardEffect
{
    public override void Effect()
    {
        base.Effect();
        HealthManager.Instance.PlayerBlock = true;
    }
}
