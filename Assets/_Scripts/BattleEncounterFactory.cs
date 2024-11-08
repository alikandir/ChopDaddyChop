using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BattleEncounterFactory : EncounterBase
{
   [SerializeField] private BattleEnemyGroupsSO[] _easyBattleGroups;
   [SerializeField] private BattleEnemyGroupsSO[] _mediumBattleGroups;
   [SerializeField] private BattleEnemyGroupsSO[] _hardBattleGroups;
   private BattleEnemyGroupsSO _currentBattleGroup;
   private void Start() {
        encounterType = EncounterType.Battle;
   }
   
    public BattleEnemyGroupsSO GetRandomBattleGroup(EncounterDifficulty difficulty)
    {
         
         switch (difficulty)
         {
              case EncounterDifficulty.Easy:
                _currentBattleGroup = (BattleEnemyGroupsSO) _easyBattleGroups[UnityEngine.Random.Range(0, _easyBattleGroups.Length)];
                break;
              case EncounterDifficulty.Medium:
                _currentBattleGroup = (BattleEnemyGroupsSO) _mediumBattleGroups[UnityEngine.Random.Range(0, _mediumBattleGroups.Length)];
                break;
              case EncounterDifficulty.Hard:
                _currentBattleGroup = (BattleEnemyGroupsSO) _hardBattleGroups[UnityEngine.Random.Range(0, _hardBattleGroups.Length)];
                break;
         }
         return _currentBattleGroup;
}
}
public enum EncounterDifficulty
{
    Easy,
    Medium,
    Hard
}
