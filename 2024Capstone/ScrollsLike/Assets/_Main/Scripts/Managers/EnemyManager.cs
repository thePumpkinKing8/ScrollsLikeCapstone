using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyManager : Singleton<EnemyManager>
{
    public EnemyDeck OpponentsDeck;
    public int EnemyHealth
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;
            _healthText.text = $"health {_enemyHealth}";
        }
    }
    private int _enemyHealth = 14;
    [SerializeField] private TextMeshProUGUI _healthText;
    protected override void Awake()
    {
        base.Awake();
        CardGameManager.Instance.Events.EnemyHit.AddListener(EnemyHit);
        CardGameManager.Instance.Events.EnemyHeal.AddListener(EnemyHeal);
    }

    public void SetUp(EnemyDeck deck)
    {
        OpponentsDeck = deck;
        EnemyHealth = deck.Health;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHit(int damage)
    {
        EnemyHealth -= damage;
    }

    public void EnemyHeal(int value)
    {
        EnemyHealth += value;
    }

    public EnemyCardData PlayAbility()
    {
        Debug.Log("fjahd");
        EnemyCardData card = OpponentsDeck.Deck[Random.Range(0,OpponentsDeck.Deck.Count)];
        return card;
    }
}
