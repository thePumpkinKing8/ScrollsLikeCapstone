using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeckData", menuName = "Enemies/EnemyDeckData")]
public class EnemyDeck : ScriptableObject
{
    [SerializeField] private List<CardData> _startingCards; //the cards the player will start with at the beginning of the game
    public List<EnemyCardData> Deck { get { return _deck; } }
    private List<EnemyCardData> _deck = new List<EnemyCardData>();
    [SerializeField] private int _enemyDifficulty; // meaningless number for now representing the difficulty sclaing. 
}
