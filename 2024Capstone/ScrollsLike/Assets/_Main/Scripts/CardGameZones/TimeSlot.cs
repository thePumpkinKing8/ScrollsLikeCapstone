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


    #region UI 
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _fillableBar;
    [SerializeField] private GameObject _shield;
    #endregion


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
        if(!Active)
        {
            _text.text = 0.ToString();
            _shield.SetActive(false);
            return;
        }

        if (EnemyManager.Instance.EnemyBlock > 0)
        {
            _text.text = EnemyManager.Instance.EnemyBlock.ToString();
            _shield.SetActive(true);
        }
        else
        {
            _shield.SetActive(false);
            _fillableBar.fillAmount = ((float)SlotHealth / (float)_maxHealth);
            _text.text = SlotHealth.ToString();
        }

        if (SlotHealth > _maxHealth)
        {
            SlotHealth = _maxHealth;
        }

        //_text.text = SlotHealth.ToString();
        if (SlotHealth <= 0 && EnemyData != null)
        {
            ClearSlot();
            ToggleActive(false);
            _text.text = "dead";
            SlotHealth = 0;
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
        EnemyManager.Instance.AddCard(EnemyData?.ReferenceCardData);
        EnemyData?.OnDeSpawn();
        EnemyData = null;
    }



    public void AddEnemyEffect(EnemyCardData card)
    {
        EnemyData =  PoolManager.Instance.Spawn("EnemyCard").GetComponent<EnemyCard>(); 
        EnemyData.transform.SetParent(transform);
        EnemyData.ReferenceCardData = card;
        EnemyData.CardSetUp();
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

        if (SlotHealth <= 0)
        {

        }
    }

    public void PoisonDamage(int value)
    {
        SlotHealth -= value;
        var effect = PoolManager.Instance.Spawn("PoisonEffect");
        effect.transform.SetParent(transform);
        effect.transform.position = transform.position;
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
        var effect = PoolManager.Instance.Spawn("HealEffect");
        effect.transform.SetParent(transform);
        effect.transform.position = transform.position;
        _text.text = SlotHealth.ToString();
    }

    public void ResolveEnemyEffect()
    {
        if(EnemyData == null)
            return;

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
