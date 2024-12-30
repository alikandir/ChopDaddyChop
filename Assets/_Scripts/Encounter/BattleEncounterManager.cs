using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    private int _enemyDeathCount=0;
    [Header("Music")]
    [SerializeField] private AudioClip _battleMusicEasy;
    [SerializeField] private AudioClip _battleMusicMedium;
    [SerializeField] private AudioClip _battleMusicHard;
    [SerializeField] private ButtonImageHandler _buttonImageHandler;
    private int _comboCounter=0;
    
    [SerializeField] private TextMeshProUGUI _comboCounterText;
    private Player _player;
    public static BattleEncounterManager instance;
    private void Awake() {
        if(instance==null){instance=this;}
    }
    private void Start()
    {
        _FrameOfButtons.SetActive(false);
        StartCoroutine(InitiateBattleUI());
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        InitiateBattle();
    }
    public void InitiateBattle()
    {
        float animationSpeed = 1f;
        var diceRoll = Random.Range(0, 100);
        if (diceRoll < 50)
        {
            difficulty=EncounterDifficulty.Easy;
            animationSpeed = 1f;
        }
        else if (diceRoll >= 50 && diceRoll < 85)
        {
            difficulty=EncounterDifficulty.Medium;
            animationSpeed = 1.3f;
        }
        else
        {
            difficulty=EncounterDifficulty.Hard;
            animationSpeed = 1.7f;
        }
        _player.GetComponent<Animator>().speed = animationSpeed;
        currentBattleGroup = _battleEncounterFactory.GetRandomBattleGroup(difficulty);
        Vector3 offsetIncrement = Vector3.zero; 
        foreach (EnemyBase Enemy in currentBattleGroup.EnemyGroup)
        {
            var enemy = Instantiate(Enemy, _spawnLocation.position+offsetIncrement, Quaternion.identity);
            offsetIncrement += spawnLocationOffset;
            enemiesList.Add(enemy);
            enemy.GetComponent<Animator>().speed = animationSpeed;
            enemy.OnEnemyDied += HandleEnemyDeath;
        }
        _currentEnemy=enemiesList[0];
        
    }
    private void HandleEnemyDeath(){
        enemiesList.Remove(_currentEnemy);
        _player.AddItemToInventory(_currentEnemy.GetComponent<Vegetable>().GetVegetableType(), 1);
        
        _currentEnemy.GetComponent<Animator>().SetTrigger("Death");
        _enemyDeathCount++;
        _currentEnemy=null;

        foreach (EnemyBase enemy in enemiesList){
            enemy.transform.DOMove(enemy.transform.position-spawnLocationOffset, _buttonImageHandler.SecPerBeat);
        }
        
    }
    
    private IEnumerator InitiateBattleUI()
    {
        _GetReadyText.SetActive(true);
        yield return new WaitForSeconds(1f);
        _GetReadyText.SetActive(false);
        _FrameOfButtons.SetActive(true);
        StartBattle();

    }
    private void StartBattle()
    {
        
        switch (difficulty)
        {
            case EncounterDifficulty.Easy:
                RhytmConductor.instance.SetAudioClip(_battleMusicEasy, 80, 0, 44);
                break;
            case EncounterDifficulty.Medium:
                RhytmConductor.instance.SetAudioClip(_battleMusicMedium, 100, 0, 43.3f);
                break;
            case EncounterDifficulty.Hard:
                RhytmConductor.instance.SetAudioClip(_battleMusicHard, 120, 0, 44);
                break;
        }
        RhytmConductor.instance.StartSong();
        StartCoroutine(FightControl());
    }
    
    private IEnumerator FightControl()
    {
        int increment = 0;
        while (true){
            
            yield return new WaitForSeconds(_buttonImageHandler.SecPerBeat);
            if (_currentEnemy!=null){
                BattlePatternElement[] pattern = _currentEnemy.BattlePattern;
                increment=increment%(pattern.Length);
                _buttonImageHandler.SpawnButton(pattern[increment]);
                increment++;
                
            }
                
            else
            {
        
                if (TryGetNextEnemy(out EnemyBase nextEnemy))
                {
                    TransitionToNextEnemy(nextEnemy);
                }
                else
                {
                    BattleWon();
                    yield break;
                }
        
            }
        }
    }
    private void TransitionToNextEnemy(EnemyBase nextEnemy)
    {   
        
        _currentEnemy = nextEnemy;
    }
    private bool TryGetNextEnemy(out EnemyBase nextEnemy)
    {
        nextEnemy = null;

        if (enemiesList.Count > 0)
            {
                nextEnemy = enemiesList[0];
                return true;
                
            }
        return false;
    }
    public void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_buttonImageHandler.IsInCheckArea()){
                ResetCombo();
                return;
            }
            else
            {
               if(!_buttonImageHandler.CheckButtonToActionName(context.action.name)){
                    _buttonImageHandler.OnButtonFailed(context.action.name);
                    ResetCombo();
                    return;
               }
               else
               {
                   _comboCounter++;
                   _buttonImageHandler.OnButtonSuccess(context.action.name,_currentEnemy);
                   _comboCounterText.text = "COMBO:" + _comboCounter.ToString();
               }
            
            }
        }
    }   
    public void OnFailToDefend()
    {
        _player.TakeDamage(_currentEnemy.GetDamage);
        _player.GetComponent<Animator>().SetTrigger("Hurt");
        ResetCombo();
    }
    public void BattleWon()
    {
        _FrameOfButtons.SetActive(false);
        RhytmConductor.instance.StopSong();
        GameStateManager.instance.SetGameState(GameStateManager.GameState.BetweenEncounter);
        SceneManager.LoadScene("BetweenEncounterScene");
    }
    public void ResetCombo(){
        _comboCounter=0;
        _comboCounterText.text = "COMBO:" + _comboCounter.ToString();
    }
    public float GetComboDamageMultiplier(){
        return (_comboCounter/10)+1f;
    }
}
