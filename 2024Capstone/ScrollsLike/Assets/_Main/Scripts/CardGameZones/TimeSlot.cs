using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlot : MonoBehaviour, ICardEffectable
{
   
    public EnemyCard EnemyData { get; private set; } 

    public bool Active{ get; private set; } // whether this slot can be targeted or not

    private int _maxHealth;
    public int SlotHealth {get; private set; }
    
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        Active = true;
    }
    public void ToggleActive()
    {
        Active = !Active;
        var color = GetComponent<Image>();
        if (Active)
            color.color = Color.gray;
        else
           color.color = Color.white;
    }

    public void SetUp(int health)
    {
        _maxHealth = health;
        text.text = _maxHealth.ToString();
        SlotHealth = health;

    }

    public void ClearSlot()
    {
        EnemyData.OnDeSpawn();
        EnemyData = null;
    }



    public void AddEnemyEffect(EnemyCardData card)
    {
        EnemyData =  PoolManager.Instance.Spawn("EnemyCard").GetComponent<EnemyCard>(); 
        EnemyData.transform.SetParent(transform);
        EnemyData.ReferenceCardData = card;
    }

    public void EnemyHit(int damage)
    {        
        PoolObject effect;
        if (EnemyManager.Instance.EnemyBlock > 0)
        {
            if (damage > EnemyManager.Instance.EnemyBlock)
            {
                int remainder = damage - EnemyManager.Instance.EnemyBlock;
                effect = PoolManager.Instance.Spawn("BlockBreakEffect");
                effect.transform.SetParent(transform);
                effect.transform.position = transform.position;
                EnemyManager.Instance.BlockHit(EnemyManager.Instance.EnemyBlock);
                SlotHealth -= remainder;
            }
            else
            {
                effect = PoolManager.Instance.Spawn("AttackEffect");
                effect.transform.SetParent(transform);
                effect.transform.position = transform.position;
                EnemyManager.Instance.BlockHit(damage);
            }
        }
        else
        {
            effect = PoolManager.Instance.Spawn("AttackEffect");
            effect.transform.SetParent(transform);
            effect.transform.position = transform.position;
            SlotHealth -= damage;
        }
        text.text = SlotHealth.ToString();
        if (SlotHealth <= 0)
        {
            ToggleActive();
            ClearSlot();
            text.text = "dead";
        }
    }
    
    public void GainBlock(int value)
    {
        var effect = PoolManager.Instance.Spawn("BlockGainEffect");
        effect.transform.SetParent(transform);
        effect.transform.position = transform.position;
        EnemyManager.Instance.EnemyGainBlock(value);
    }

    public void EnemyHeal(int value)
    {
        if(!Active)
        {
            ToggleActive();
        }
        SlotHealth += value;
        if(SlotHealth > _maxHealth)
        {
            SlotHealth = _maxHealth;
        }
        text.text = SlotHealth.ToString();
    }

    public void ResolveEnemyEffect()
    {
        EffectManager.Instance.ActivateEffect(EnemyData.ReferenceCardData.CardResolutionEffects);
    }

    public void ApplyEffect(CardEffectType effectType, int value, CardData card)
    {
        switch (effectType)
        {
            case CardEffectType.Damage:
                ApplyDamage(value);
                break;
            case CardEffectType.Heal:
                EnemyHeal(value);
                break;
            case CardEffectType.Block:
                GainBlock(value);
                break;
        }
    }

    public void ApplyDamage(int value)
    {
        EnemyHit(value);
    }
}
