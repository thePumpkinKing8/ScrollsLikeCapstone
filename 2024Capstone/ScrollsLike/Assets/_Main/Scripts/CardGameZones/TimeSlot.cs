using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlot : MonoBehaviour
{
    public GameCard PlayersCard { get; private set; }
    public GameCard EnemyCard { get; private set; } //placeholder for what the enemy will actually use

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolvePlayerEffects()
    {
        foreach(CardEffect effect in PlayersCard.ReferenceCardData.CardResolutionEffects)
        {
            //activate effect.
        }
    }

    public void ResolveEnemyEffects()
    {
        foreach (CardEffect effect in EnemyCard.ReferenceCardData.CardResolutionEffects)
        {
            //activate effect.
        }
    }
}
