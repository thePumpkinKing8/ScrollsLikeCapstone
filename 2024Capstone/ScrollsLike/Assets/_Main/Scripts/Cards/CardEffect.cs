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

    [SerializeField] private bool _isAOE;
    public bool IsAOE { get { return _isAOE; } }

    public void GetData(CardData data)
    {
        _cardsData = data;
    }
    public void Effect(HealthManager player, TimeSlot target = null)
    {


        foreach (CardEffector effect in CardEffectors)
        {
            ICardEffectable effected;
            if (effect.TargetSelf)
            {
                effected = (ICardEffectable)player;
            }
            else
            {
                if(_requireTarget)
                {
                    effected = (ICardEffectable)target;
                }
                else
                {
                    if(_isAOE)
                    {
                        foreach (TimeSlot slot in CardGameManager.Instance.EnemySlot)
                        {
                            if (slot.Active)
                            {
                                effected = (ICardEffectable)slot;
                                effected.ApplyEffect(effect.Type, effect.EffectValue, _cardsData);
                                effect.Strategy?.ApplyEffect(effected, _cardsData);
                            }
                        }
                        continue;
                    }
                    else
                    {
                        effected = (ICardEffectable)player;
                        effected.ApplyEffect(effect.Type, effect.EffectValue, _cardsData);
                        effect.Strategy?.ApplyEffect(effected, _cardsData);
                    }
                    
                }
            }
            
            effected.ApplyEffect(effect.Type, effect.EffectValue, _cardsData);
            effect.Strategy?.ApplyEffect(effected, _cardsData);
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
