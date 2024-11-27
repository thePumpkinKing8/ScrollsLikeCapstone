using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyManager : Singleton<EnemyManager>
{
    private EnemyDeck _deck;
    public List<EnemyCardData> _cards = new List<EnemyCardData>(); 
    public int EnemyHealth
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;
        }
    }
    private int _enemyHealth;

    [HideInInspector] public int DamageMod;
    public int EnemyBlock { get; private set; }

    [SerializeField] private TextMeshProUGUI _blockText;

    public int Poison { get; private set; } = 0;

    [HideInInspector] public List<StanceTrigger> StatusEffects = new List<StanceTrigger>();

    //value tracking variables. IE variables that track momentary information some effects may care about
    public int DamageBlocked { get; private set; }

    protected override void Awake()
    {
        if (GameManager.Instance.State != GameState.CardGame)
        {
            Destroy(gameObject, 1);
        }
        base.Awake();
    }

    public void SetUp(EnemyDeck deck)
    {
        _deck = deck;
        foreach(EnemyCardData enemyCard in deck.Deck)
        {
            _cards.Add(enemyCard);
        }
        EnemyHealth = deck.Health;
    }
    // Update is called once per frame
    void Update()
    {
        _blockText.text = $"Enemy Block:{EnemyBlock}";
    }

    public void BlockHit(int damage)
    {
        DamageBlocked = damage >= EnemyBlock ? damage - (damage - EnemyBlock) : damage;
        EnemyBlock -= damage;
        if(EnemyBlock < 0)
            EnemyBlock = 0;
        CardGameManager.Instance.Events.EnemyBlocked.Invoke();
    }

    public void EnemyGainBlock(int value)
    {
        EnemyBlock += value;
    }

    public void GainPoison(int value)
    {
        Poison += value;
    }

    public void StatusActivate()
    {
        foreach(TimeSlot enemy in CardGameManager.Instance.EnemySlot)
        {
            if(enemy.Active)
            {
                enemy.PoisonDamage(Poison);
            }
        }       
    }

    public void ClearBlock()
    {
        EnemyBlock = 0;
        
        foreach(StanceTrigger trigger in StatusEffects)
        {
            if(trigger.Temp)
            {
                StartCoroutine(ClearStatus(trigger));
            }
        }
    }

    IEnumerator ClearStatus(StanceTrigger temp)
    {
        yield return new WaitForSeconds(1);
        StatusEffects.Remove(temp);
        yield return null;
    }

  
    public EnemyCardData PlayAbility()
    {
        System.Random rand = new(DateTime.Now.Millisecond);
        int seed = rand.Next(_cards.Count);
        EnemyCardData card = _cards[seed];
        _cards.Remove(card);
        return card;
    }

    public void AddCard(EnemyCardData card)
    {
        _cards.Add(card);
    }

    IEnumerator EndGame(string message)
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.PlayerWins();
        yield return null;
    }

    public void AddEffect(StanceTrigger stance)
    {
        StatusEffects.Add(stance);
        stance.Event.AddListener(delegate { TriggerStatus(stance); });
    }

    public void RemoveEffect(StanceTrigger stance)
    {
        StatusEffects.Remove(stance);
        stance.Event.RemoveListener(() => TriggerStatus(stance));
    }

    public void TriggerStatus(StanceTrigger stance)
    {
        EffectManager.Instance.ActivateEffect(stance.Effects);
        if(stance.Temp)
        {
            RemoveEffect(stance);
        }
    }    
}
