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

    public void AddEffect(StanceTrigger stance);
    public void RemoveEffect(StanceTrigger stance);
}
//Should probably be changed to player manager
public class HealthManager : Singleton<HealthManager>, ICardEffectable
{
    [SerializeField] private int _startingHealth; //will be set by seperate script later
    public int Wounds = 3;
    public int PlayerHealth { get; set; }

    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _poisonText;
    public int Energy { get; private set; }

    public int Poison { get; private set; } = 0;

    [HideInInspector] public int PlayerBlock { get; private set; }
    //[HideInInspector] public bool EnemyBlock;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Energy = 0;
        PlayerHealth = _startingHealth;
        _statText = GetComponentInChildren<TextMeshProUGUI>();
        CardGameManager.Instance.Events.DrawPhaseStartEvent.AddListener(TriggerPoison);
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

        _statText.text = $"Health:{PlayerHealth.ToString()} \nWounds:{Wounds.ToString()} \nBlock:{PlayerBlock.ToString()} \nEnergy:{Energy.ToString()}";
        _poisonText.text = $"Poison:{Poison}";

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
                effect.transform.position = _statText.transform.position;
                PlayerBlock = 0;
                PlayerHealth -= remainder;
                CardGameManager.Instance.Events.PlayerHit.Invoke();
            }
            else
            {
                effect = PoolManager.Instance.Spawn("BlockHitEffect");
                effect.transform.SetAsLastSibling();
                effect.transform.SetParent(transform);
                effect.transform.position = _statText.transform.position;
                PlayerBlock -= damage;
            }
        }
        else
        {
            effect = PoolManager.Instance.Spawn("AttackEffect");
            effect.transform.SetAsLastSibling();
            effect.transform.SetParent(transform);
            effect.transform.position = _statText.transform.position;
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
        effect.transform.position = _statText.transform.position;
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

    public void TriggerPoison()
    {
        if(Poison > 0)
        {
            PlayerHealth -= Poison;
            Poison -= 1;
        }
        
    }

    public void GainPoison(int value)
    {
        Poison += value;
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
            case CardEffectType.Poison:
                if(PlayerBlock > 0)
                {
                    GainPoison(value);
                }
                break;
            case CardEffectType.UnblockPoison:               
                GainPoison(value);               
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
        Debug.Log(stance.Event.GetPersistentEventCount());
    }

    public void RemoveEffect(StanceTrigger stance)
    {
        StatusEffects.Remove(stance);
        stance.Event.RemoveListener(() => TriggerStatus(stance));
    }

    public void TriggerStatus(StanceTrigger stance)
    {
        EffectManager.Instance.ActivateEffect(stance.Effects) ;
        if (stance.Temp)
        {
            RemoveEffect(stance);
        }
    }
}
