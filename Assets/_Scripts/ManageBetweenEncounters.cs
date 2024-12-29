using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageBetweenEncounters : MonoBehaviour
{
    public void OnKeepSearchingButtonPressed(){
        GameStateManager.instance.SetGameState(GameStateManager.GameState.InEncounter);
        SceneManager.LoadScene("BattleEncounterScene");
        
    }
     public void OnGoBackTownButtonPressed(){
        GameStateManager.instance.SetGameState(GameStateManager.GameState.InTown);
        SceneManager.LoadScene("TownScene");
        
    }
}
