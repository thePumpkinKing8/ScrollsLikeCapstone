using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyManager : Singleton<EnemyManager>
{
    private EnemyDeck _deck;
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
            if(_enemyHealth <= 0)
            {
                StartCoroutine(EndGame("Victory"));
            }
        }
    }
    private int _enemyHealth;
    [SerializeField] private TextMeshProUGUI _healthText;
    protected override void Awake()
    {
        base.Awake();
    }

    public void SetUp(EnemyDeck deck)
    {
        _deck = deck;
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
        EnemyCardData card = _deck.Deck[Random.Range(0,_deck.Deck.Count)];
        return card;
    }

    IEnumerator EndGame(string message)
    {
        _healthText.text = message;
        yield return new WaitForSeconds(4);
        GameManager.Instance.PlayerWins();
        yield return null;
    }
}
