using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public interface ICardEffectable
{
    void ApplyEffect(CardEffectType effectType, int value, CardData card);
    void ApplyDamage(int value);
}
//Should probably be changed to player manager
public class HealthManager : Singleton<HealthManager>, ICardEffectable
{
    [SerializeField] private int _startingHealth; //will be set by seperate script later
    public int Wounds = 3;
    public int PlayerHealth { get; set; }

    private TextMeshProUGUI _text;
    public int Energy { get; private set; }

    [HideInInspector] public int PlayerBlock { get; private set; }
    //[HideInInspector] public bool EnemyBlock;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Energy = 0;
        PlayerHealth = _startingHealth;
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    [HideInInspector] public List<StanceTrigger> StatusEffects = new List<StanceTrigger>(); 

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth <= 0)
        {
            Wounds--;
            if (Wounds <= 0)
            {
                Debug.Log("Lose");
                StartCoroutine(EndGame("Lose"));
            }
            else
            {
                var overDamage = PlayerHealth; //the damage that knocked the player below 0
                PlayerHealth = _startingHealth;
                PlayerHealth -= overDamage;
            }
                
        }

        _text.text = $"Health:{PlayerHealth.ToString()} \nWounds:{Wounds.ToString()} \nBlock:{PlayerBlock.ToString()} \nEnergy:{Energy.ToString()}";

        if(PlayerHealth > _startingHealth)
        {
            PlayerHealth = _startingHealth;
        }
        
    }
    //deals health damage to the player
    public void PlayerHit(int damage)
    {
        PoolObject effect;
        if(PlayerBlock > 0)
        {
            if(damage > PlayerBlock)
            {
                int remainder = damage - PlayerBlock;
                effect = PoolManager.Instance.Spawn("BlockBreakEffect");
                effect.transform.SetAsLastSibling();
                effect.transform.SetParent(transform);
                effect.transform.position = _text.transform.position;
                PlayerBlock = 0;
                PlayerHealth -= remainder;
                CardGameManager.Instance.Events.PlayerHit.Invoke();
            }
            else
            {
                effect = PoolManager.Instance.Spawn("BlockHitEffect");
                effect.transform.SetAsLastSibling();
                effect.transform.SetParent(transform);
                effect.transform.position = _text.transform.position;
                PlayerBlock -= damage;
            }
        }
        else
        {
            effect = PoolManager.Instance.Spawn("AttackEffect");
            effect.transform.SetAsLastSibling();
            effect.transform.SetParent(transform);
            effect.transform.position = _text.transform.position;
            PlayerHealth -= damage;
            Debug.Log("playerHit");
            CardGameManager.Instance.Events.PlayerHit.Invoke();
        }
        
        
    }

    public void GainBlock(int amount)
    {
        var effect = PoolManager.Instance.Spawn("BlockGainEffect");
        effect.transform.SetAsLastSibling();
        effect.transform.SetParent(transform);
        effect.transform.position = _text.transform.position;
        PlayerBlock += amount;
    }

    public void GainHealth(int value)
    {
        PlayerHealth += value;
    }

    public void ChangeEnergy(int amount)
    {
        Energy += amount;
    }
    //Ends the game and returns to the adventure sections
    IEnumerator EndGame(string message)
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.PlayerLoses();
        yield return null;
    }

    public void ApplyEffect(CardEffectType effectType, int value, CardData card)
    {
        switch (effectType)
        {
            case CardEffectType.Damage:
                PlayerHit(value);
                break;
            case CardEffectType.Heal:
                GainHealth(value);
                break;
            case CardEffectType.Block:
                GainBlock(value);
                break;
            case CardEffectType.Draw:
                CardGameManager.Instance.DrawCard(value);
                break;
            case CardEffectType.None:
                break;
        }
    }

    public void ApplyDamage(int value)
    {
        PlayerHit(value);
       // throw new System.NotImplementedException();
    }

    public void AddEffect(StanceTrigger stance)
    {
        StatusEffects.Add(stance);
        stance.Event.AddListener(delegate { TriggerStatus(stance); });
        if(stance.Event == CardGameManager.Instance.Events.PlayerHit)
        {
            Debug.Log("same");
        }
    }

    public void RemoveEffect(StanceTrigger stance)
    {
        StatusEffects.Remove(stance);
        stance.Event.RemoveListener(() => TriggerStatus(stance));
    }

    public void TriggerStatus(StanceTrigger stance)
    {
        Debug.Log("it worked");
        EffectManager.Instance.ActivateEffect(stance.Effects) ;
    }
}
