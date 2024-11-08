using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BattleEnemyGroup", menuName = "New BattleEnemyGroup")]
public class BattleEnemyGroupsSO : ScriptableObject
{
    [SerializeField] private List<EnemyBase> _enemyGroup;
    public List<EnemyBase> EnemyGroup => _enemyGroup;
    
    
}
