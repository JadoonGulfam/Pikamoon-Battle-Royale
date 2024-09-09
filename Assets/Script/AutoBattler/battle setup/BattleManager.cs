using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public  bool isBattleStarted;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
        isBattleStarted = true;
       // AutoBattlerEvents.TriggerBattleStart();
        print("111111");
        
    }

    private void EndBattle(bool isPlayerWin)
    {
        isBattleStarted = false;
        print("IsAiWin" + isPlayerWin);
        //AutoBattlerEvents.TriggerBattleEnd();
        print("EndBattle");
        
        
    }

   
}
