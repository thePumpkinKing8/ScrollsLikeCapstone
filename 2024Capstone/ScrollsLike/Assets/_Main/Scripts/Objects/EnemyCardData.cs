using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCardData", menuName = "Enemies/EnemyCard")]
public class EnemyCardData : CardData
{
    [Tooltip("number of times card can be played by enemy")]
    [SerializeField] private int _cardUses;
    public int CardUses { get { return _cardUses; }}
    private int _remainingUses;
    private void Awake()
    {
        _remainingUses = CardUses;
    }
}
