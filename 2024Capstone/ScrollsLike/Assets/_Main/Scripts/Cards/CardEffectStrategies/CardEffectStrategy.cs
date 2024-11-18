using Unity;
using UnityEngine;

public abstract class CardEffectStrategy : ScriptableObject
{
    public virtual void ApplyEffect(ICardEffectable target, CardData card)
    {

        //whatever logic you have for applying an effect
    }
}