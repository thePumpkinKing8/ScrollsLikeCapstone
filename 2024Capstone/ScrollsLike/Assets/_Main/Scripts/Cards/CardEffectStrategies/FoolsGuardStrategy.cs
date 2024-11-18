using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoolsGuardStrategy", menuName = "SOs/CardStrategy/FoolsGuardStrategy")]
public class FoolsGuardStrategy : CardEffectStrategy
{
    [HideInInspector] public int Damage = 0; 
    public override void ApplyEffect(ICardEffectable target, CardData card)
    {
        if(Damage < 1)
        {
            Debug.Log("playing");
            CardGameManager.Instance.EffectDone();
            return;
        }
        target.ApplyEffect(CardEffectType.Damage, Damage, card);
        Debug.Log(Damage);
    }

    public void SetDamage(int value)
    {
        Damage = value;
    }
}
