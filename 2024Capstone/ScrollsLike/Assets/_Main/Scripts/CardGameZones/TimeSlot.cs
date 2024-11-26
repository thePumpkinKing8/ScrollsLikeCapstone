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
    
    [SerializeField] private TextMeshProUGUI _text;

   

    private void Awake()
    {
        Active = true;
        SlotHealth = 1;
    }
    public void ToggleActive(bool activate)
    {
        
        var color = GetComponent<Image>();
        if (activate)
        {
            color.color = Color.gray;
            Active = true;
        }
            
        else
        {
            color.color = Color.white;
            Active = false;
            CardGameManager.Instance.CheckGameEnd();
        }          
    }


    private void Update()
    {
        //_text.text = SlotHealth.ToString();
        if(SlotHealth <= 0)
        {
            ToggleActive(false);
            ClearSlot();
            _text.text = "dead";
        }
    }
    public void SetUp(int health)
    {
        _maxHealth = health;
        _text.text = _maxHealth.ToString();
        SlotHealth = health;

    }

    public void ClearSlot()
    {
        EnemyData?.OnDeSpawn();
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
        damage += HealthManager.Instance.DamageMod;
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
        _text.text = SlotHealth.ToString();
        if (SlotHealth <= 0)
        {

        }
    }

    public void PoisonDamage(int value)
    {
        SlotHealth -= value;
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
            ToggleActive(true);
        }
        SlotHealth += value;
        if(SlotHealth > _maxHealth)
        {
            SlotHealth = _maxHealth;
        }
        _text.text = SlotHealth.ToString();
    }

    public void ResolveEnemyEffect()
    {
        EffectManager.Instance.ActivateEffect(EnemyData.ReferenceCardData.CardResolutionEffects, this);
        Debug.Log("enemy effect");
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
            case CardEffectType.Poison:
                if(EnemyManager.Instance.EnemyBlock <= 0)
                {
                    EnemyManager.Instance.GainPoison(value);
                }
                break;
            case CardEffectType.DamageBuff:
                EnemyManager.Instance.DamageMod += value;
                CardGameManager.Instance.EffectDone();
                break;
        }
    }

    public void ApplyDamage(int value)
    {
        EnemyHit(value);
    }

    public void AddEffect(StanceTrigger stance)
    {
        stance.Event.AddListener(delegate { TriggerStatus(stance); });
    }

    public void RemoveEffect(StanceTrigger stance)
    {
        stance.Event.RemoveListener(() => TriggerStatus(stance));
    }

    public void TriggerStatus(StanceTrigger stance)
    {
        EffectManager.Instance.ActivateEffect(stance.Effects);
    }
}
