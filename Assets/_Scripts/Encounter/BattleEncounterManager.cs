using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BattleEncounterManager : MonoBehaviour
{
    [SerializeField] private BattleEncounterFactory _battleEncounterFactory;
    [SerializeField] private GameObject _GetReadyText;
    private EncounterDifficulty difficulty;
    private BattleEnemyGroupsSO currentBattleGroup;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Vector3 spawnLocationOffset;
    [SerializeField] private GameObject _FrameOfButtons;
    private List<EnemyBase> enemiesList = new List<EnemyBase>();
    private EnemyBase _currentEnemy;
    [Header("Music")]
    [SerializeField] private AudioClip _battleMusicEasy;
    [SerializeField] private AudioClip _battleMusicMedium;
    [SerializeField] private AudioClip _battleMusicHard;
    [SerializeField] private ButtonImageHandler _buttonImageHandler;
    private int _comboCounter=0;
    [SerializeField] private TextMeshProUGUI _comboCounterText;
    private void Start()
    {
        InitiateBattle();
        _FrameOfButtons.SetActive(false);
        StartCoroutine(InitiateBattleUI());
        
    }
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
    }
    private IEnumerator InitiateBattleUI()
    {
        _GetReadyText.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _GetReadyText.SetActive(false);
        _FrameOfButtons.SetActive(true);
        StartBattle();

    }
    private void StartBattle()
    {
        switch (difficulty)
        {
            case EncounterDifficulty.Easy:
                RhytmConductor.instance.SetAudioClip(_battleMusicEasy, 100, 0, 16);
                break;
            case EncounterDifficulty.Medium:
                RhytmConductor.instance.SetAudioClip(_battleMusicMedium, 120, 0, 16);
                break;
            case EncounterDifficulty.Hard:
                RhytmConductor.instance.SetAudioClip(_battleMusicHard, 140, 0, 16);
                break;
        }
        RhytmConductor.instance.StartSong();
        StartCoroutine(FightControl());
    }
    
    private IEnumerator FightControl()
    {
        while (_currentEnemy.IsAlive){
            
            BattlePatternElement[] pattern = _currentEnemy.BattlePattern;
            foreach (BattlePatternElement element in pattern)
            {
                _buttonImageHandler.SpawnButton(element);
                yield return new WaitForSeconds(_buttonImageHandler.SecPerBeat);
            }
        }
    }
    public void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_buttonImageHandler.IsInCheckArea()){
                _comboCounter=0;
                _comboCounterText.text = "COMBO:" + _comboCounter.ToString();
                return;
            }
            else
            {
               if(!_buttonImageHandler.CheckButtonToActionName(context.action.name)){
                    _buttonImageHandler.OnButtonFailed(context.action.name);
                    _comboCounter=0;
                    _comboCounterText.text = "COMBO:" + _comboCounter.ToString();
                    return;
               }
               else
               {
                   _comboCounter++;
                   _buttonImageHandler.OnButtonSuccess(context.action.name);
                   _comboCounterText.text = "COMBO:" + _comboCounter.ToString();
               }
            
            }
        }
    }   
    

}
