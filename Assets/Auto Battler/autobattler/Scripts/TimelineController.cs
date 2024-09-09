using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    public GameObject battleCamera;
    public GameObject playerTeamSelectionPanel;
    public void TimeLineCompleted()
    {
        battleCamera.SetActive(true);
        playerTeamSelectionPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
}
