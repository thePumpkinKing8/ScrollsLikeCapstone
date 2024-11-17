using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "Base Effect", menuName = "Effects/Base Effect")]
public class CardEffect : ScriptableObject
{
    [field: SerializeField]
    public List<CardEffector> CardEffectors = new List<CardEffector>();
    protected CardData _cardsData;

    [SerializeField] private bool _requireTarget;
    public bool RequiresTarget { get { return _requireTarget; } }
    
    public void GetData(CardData data)
    {
        _cardsData = data;
    }
    public void Effect(HealthManager player, TimeSlot target = null)
    {
        foreach(CardEffector effect in CardEffectors)
        {
            Debug.Log(target);
            var effected = effect.TargetSelf ? (ICardEffectable) player : (ICardEffectable)target;
            effected.ApplyEffect(effect.Type, effect.EffectValue);
            effect.Strategy?.ApplyEffect(effected);
            
        }
    }
}
public enum CardEffectType
{
    Damage, Heal, Block, Draw, None
}
[Serializable]
public struct CardEffector
{
    public CardEffectType Type;
    public int EffectValue;
    public bool TargetSelf;
    public CardEffectStrategy Strategy;
}
