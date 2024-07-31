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
    //
}
