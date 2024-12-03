using Unity;
using UnityEngine;
using UnityEngine.Events;
public abstract class CardEffectStrategy : ScriptableObject
{

    [SerializeField] protected CardEventData _cardEventData;
    public UnityEvent Event { get { return _event; } }
    protected UnityEvent _event;
    public virtual void ApplyEffect(ICardEffectable target,int value , CardData card)
    {
        //whatever logic you have for applying an effect
    }
}