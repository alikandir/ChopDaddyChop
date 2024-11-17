using System.Collections.Generic;
using UnityEngine;

public class BattleEncounterManager : MonoBehaviour
{
    [SerializeField] private BattleEncounterFactory _battleEncounterFactory;
    private EncounterDifficulty difficulty;
    private BattleEnemyGroupsSO currentBattleGroup;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Vector3 spawnLocationOffset;
    private List<EnemyBase> enemiesList = new List<EnemyBase>();
    private EnemyBase _currentEnemy;
    public void InitiateBattle()
    {
        var diceRoll = Random.Range(0, 100);
        if (diceRoll < 70)
        {
            difficulty=EncounterDifficulty.Easy;
        }
        else if (diceRoll >= 70 && diceRoll < 90)
        {
            difficulty=EncounterDifficulty.Medium;
        }
        else
        {
            difficulty=EncounterDifficulty.Hard;
        }
        currentBattleGroup = _battleEncounterFactory.GetRandomBattleGroup(difficulty);
        Vector3 offsetIncrement = Vector3.zero; 
        foreach (EnemyBase enemy in currentBattleGroup.EnemyGroup)
        {
            Instantiate(enemy, _spawnLocation.position+offsetIncrement, Quaternion.identity);
            offsetIncrement += spawnLocationOffset;
            enemiesList.Add(enemy);
        }
        _currentEnemy=enemiesList[0];
        StartBattle();
    }
    private void Start()
    {
        InitiateBattle();
    }
    private void StartBattle()
    {
        
    }

}
