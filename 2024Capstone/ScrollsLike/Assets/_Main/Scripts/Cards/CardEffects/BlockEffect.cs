using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Effect", menuName = "Effects/Block Effect")]
public class BlockEffect : CardEffect
{
    [SerializeField] private int _blockGain;
    public override void Effect()
    {
        base.Effect();
        CardGameManager.Instance.PlayerBlock(_blockGain);
    }
}
