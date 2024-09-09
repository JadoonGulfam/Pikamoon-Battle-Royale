using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class AutoBattlerUIManager : MonoBehaviour
{
    [SerializeField]
    private PlacementSystem placementSystem;

    [SerializeField]
    private GameObject playerTeamSelectionUI;

    [SerializeField]
    private GameObject battleSystemInPogress;
   
    [SerializeField]
    private GameObject victoryPannel;
   
    [SerializeField]
    private GameObject defetPanel;

    private void Awake()
    {
        AutoBattlerEvents.OnBattleEnd += BattleFinished;
        AutoBattlerEvents.OnBattleStart += TeamSelectionCompleted;
    }
    private void BattleFinished(bool isAiWin)
    {
        if(isAiWin)
        {
            EnableDefetPanel();
          
        }
        else
        {
            EnableVictoryPanel();
        }
    }
    public void StartAITeamSelection()
    {
        placementSystem.StartPlacement();
    }
    public void StartPlayerTeamSelection()
    {
        playerTeamSelectionUI.SetActive(true);
        
    }
    public void TeamSelectionCompleted()
    {
        print(1111111);
        playerTeamSelectionUI.SetActive(false);
    }
    public void StartStartBattel()
    {
        placementSystem.StartPlacement();
    }
    public void BattleInProgressPanel()
    {
        battleSystemInPogress.SetActive(true);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void EnableVictoryPanel()
    {
        victoryPannel.SetActive(true);
    }
    public void EnableDefetPanel()
    {
        defetPanel.SetActive(true);
    }
    //
}
