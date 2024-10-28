using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeckData", menuName = "Enemies/EnemyDeckData")]
public class EnemyDeck : ScriptableObject
{
    public List<EnemyCardData> Deck { get { return _deck; } }
    [SerializeField] private List<EnemyCardData> _deck;
    [SerializeField] private int _enemyDifficulty; // meaningless number for now representing the difficulty sclaing. 
}
