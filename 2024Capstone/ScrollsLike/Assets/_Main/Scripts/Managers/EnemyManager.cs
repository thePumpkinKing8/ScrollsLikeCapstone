using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public EnemyDeck OpponentsDeck;
    public int EnemyHealth { get; private set; }

    private void Awake()
    {
        CardGameManager.Instance.Events.EnemyHit.AddListener(EnemyHit);
        CardGameManager.Instance.Events.EnergyGain.AddListener(EnemyHeal);
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
