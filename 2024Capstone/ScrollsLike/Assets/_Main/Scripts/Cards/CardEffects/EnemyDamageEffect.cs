using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "Enemies/Effects/EnemyAttack")]
public class EnemyDamageEffect : CardEffect
{
    [SerializeField] private int _damage;

}
