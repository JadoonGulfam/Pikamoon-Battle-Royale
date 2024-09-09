using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private void Start()
    {
        AutoBattlerEvents.OnPlacementComplete += StartBattle;
        AutoBattlerEvents.OnBattleEnd += EndBattle;
    }

    private void OnDestroy()
    {
        AutoBattlerEvents.OnPlacementComplete -= StartBattle;
        AutoBattlerEvents.OnBattleEnd -= EndBattle;
    }

    private void StartBattle()
    {
        AutoBattlerEvents.TriggerBattleStart();
        print("battle started");
        
    }

    private void EndBattle()
    {
        AutoBattlerEvents.TriggerBattleEnd();
        
    }

   
}
