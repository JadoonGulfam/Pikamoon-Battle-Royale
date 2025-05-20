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
    private GameObject PikamoomSelection;
    public void StartAITeamSelection()
    {
        placementSystem.StartPlacement();
    }
    public void StartPlayerTeamSelection()
    {
        print("active");
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
        print("disable"); 
        battleSystemInPogress.SetActive(true);
        PikamoomSelection.SetActive(false);
       
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    //
}
