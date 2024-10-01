using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private PlayerDeck _enemyDeck;
    // Start is called before the first frame update
    void Start()
    {
        _enemyDeck.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CardData PlayAbility()
    {
        Debug.Log("fjahd");
        CardData card = _enemyDeck.Deck[0];//Random.Range(0, _enemyDeck.Deck.Count)];
        return card;
    }
}
