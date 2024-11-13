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
        }
    }
    private int _enemyHealth;

    public int EnemyBlock { get; private set; }
    
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

    public void BlockHit(int damage)
    {
        EnemyBlock -= damage;
    }

    public void EnemyGainBlock(int value)
    {
        EnemyBlock += value;
    }

    public EnemyCardData PlayAbility()
    {
        EnemyCardData card = _deck.Deck[Random.Range(0,_deck.Deck.Count)];
        return card;
    }

    IEnumerator EndGame(string message)
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.PlayerWins();
        yield return null;
    }
}
