using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public PlayerDeck OpponentsDeck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CardData PlayAbility()
    {
        Debug.Log("fjahd");
        CardData card = OpponentsDeck.Deck[Random.Range(0,OpponentsDeck.Deck.Count)];
        return card;
    }
}
