using System;
using UnityEngine;

public static class AutoBattlerEvents
{

    public static event Action OnBattleStart;
    public static event Action<bool> OnBattleEnd;
    public static event Action OnPlacementComplete;
  
    public static void TriggerBattleStart()
    {
        OnBattleStart?.Invoke();
    }

    public static void TriggerBattleEnd(bool isPlayerWin)
    {
        OnBattleEnd?.Invoke(isPlayerWin);
    }

    public static void TriggerPlacementComplete()
    {
        OnPlacementComplete?.Invoke();
    }
}
