using Unity;
using UnityEngine;
using UnityEngine.Events;
public abstract class CardEffectStrategy : ScriptableObject
{
    public UnityEvent Event { get { return _event; } }
    protected UnityEvent _event;
    public virtual void ApplyEffect(ICardEffectable target, CardData card)
    {

        //whatever logic you have for applying an effect
    }
}