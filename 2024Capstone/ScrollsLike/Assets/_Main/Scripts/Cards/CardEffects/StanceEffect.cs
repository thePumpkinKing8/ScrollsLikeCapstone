using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stance Effect", menuName = "Effects/StanceEffect")]
public class StanceEffect : CardEffect
{
    public override void Effect()
    {
        base.Effect();
        CardGameManager.Instance.StanceResolved(_cardsData);
    }

}
