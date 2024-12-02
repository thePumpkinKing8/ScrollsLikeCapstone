using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public interface ICardEffectable
{
    void ApplyEffect(CardEffectType effectType, int value, CardData card);

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

    public int DamageMod { get; private set; }

    public DeathScreen deadManager;
    private bool isDead;

    [HideInInspector] public List<StanceTrigger> StatusEffects = new List<StanceTrigger>();
    [HideInInspector] public int PlayerBlock { get; private set; }

    [SerializeField] private Image _fillableBar;
    [SerializeField] private GameObject _blockContainer;
    //[HideInInspector] public bool EnemyBlock;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Energy = 0;
        PlayerHealth = _startingHealth;
        _statText = GetComponentInChildren<TextMeshProUGUI>();
        CardGameManager.Instance.Events.DrawPhaseEndEvent.AddListener(TriggerPoison);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth <= 0)
        {
            Wounds--;
            if (Wounds <= 0 && !isDead)
            {
                deadManager.onDead();
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

        if(PlayerBlock > 0)
        {
            _statText.text = PlayerBlock.ToString();
            _blockContainer.SetActive(true);
        }
        else
        {
            _blockContainer.SetActive(false);
            _fillableBar.fillAmount = ((float)PlayerHealth / (float)_startingHealth);
            _statText.text = PlayerHealth.ToString();
        }
        _poisonText.text = $"{Poison}";

        if(PlayerHealth > _startingHealth)
        {
            PlayerHealth = _startingHealth;
        }
        
    }
    //deals health damage to the player
    public void PlayerHit(int damage, CardData data)
    {
        PoolObject effect;
        if (data.CardType == CardType.Strike && data is EnemyCardData)
            damage += EnemyManager.Instance.DamageMod;
        if (PlayerBlock > 0)
        {
            if(damage > PlayerBlock)
            {
                int remainder = damage - PlayerBlock;
                effect = PoolManager.Instance.Spawn("BlockBreakEffect");
                effect.transform.SetAsLastSibling();
                effect.transform.SetParent(_fillableBar.transform);
                effect.transform.position = _statText.transform.position;
                PlayerBlock = 0;

                PlayerHealth -= remainder;
                CardGameManager.Instance.Events.PlayerHit.Invoke();
            }
            else
            {
                effect = PoolManager.Instance.Spawn("BlockHitEffect");
                effect.transform.SetAsLastSibling();
                effect.transform.SetParent(_fillableBar.transform);
                effect.transform.position = _statText.transform.position;
                
                PlayerBlock -= damage;
            }
        }
        else
        {
            effect = PoolManager.Instance.Spawn("AttackEffect");
            effect.transform.SetAsLastSibling();
            effect.transform.SetParent(_fillableBar.transform);
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
        effect.transform.SetParent(_fillableBar.transform);
        effect.transform.position = _statText.transform.position;
        PlayerBlock += amount;
    }

    public void GainHealth(int value)
    {
        var effect = PoolManager.Instance.Spawn("HealEffect");
        effect.transform.SetAsLastSibling();
        effect.transform.SetParent(_fillableBar.transform);
        PlayerHealth += value;
    }

    public void ChangeEnergy(int amount)
    {
        Energy += amount;
    }

    public void TriggerPoison()
    {
        Debug.Log(Poison);
        if(Poison > 0)
        {
            PlayerHealth -= Poison;
            Poison -= 1;

            var effect = PoolManager.Instance.Spawn("PoisonEffect");
            effect.transform.SetParent(_fillableBar.transform);
            effect.transform.SetAsLastSibling();           
        }
        
    }

    public void GainPoison(int value)
    {
        Poison += value;
        
        var effect = PoolManager.Instance.Spawn("PoisonEffect");
        effect.transform.SetAsLastSibling();
        effect.transform.SetParent(transform);
    }
    //Ends the game and returns to the adventure sections
    IEnumerator EndGame(string message)
    {
        //yield return new WaitForSeconds(4);
        GameManager.Instance.PlayerLoses();
        yield return null;
    }

    public void ApplyEffect(CardEffectType effectType, int value, CardData card)
    {
        switch (effectType)
        {
            case CardEffectType.Damage:
                PlayerHit(value, card);
                break;
            case CardEffectType.Heal:
                GainHealth(value);
                CardGameManager.Instance.EffectDone();
                break;
            case CardEffectType.Block:
                GainBlock(value);
                break;
            case CardEffectType.Draw:
                CardGameManager.Instance.DrawCard(value);
                CardGameManager.Instance.EffectDone();
                break;
            case CardEffectType.Poison:
                if(PlayerBlock >= 0)
                {
                    GainPoison(value);
                }
                break;
            case CardEffectType.UnblockPoison:               
                GainPoison(value);               
                break;
            case CardEffectType.DamageBuff:
                DamageMod += value;
                break;
            case CardEffectType.None:
                break;
        }
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
