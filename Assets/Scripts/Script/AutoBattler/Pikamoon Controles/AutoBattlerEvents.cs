using System;
using UnityEngine;

public static class AutoBattlerEvents
{

    public static event Action OnBattleStart;
    public static event Action OnBattleEnd;
    public static event Action OnPlacementComplete;
  
    public static void TriggerBattleStart()
    {
        OnBattleStart?.Invoke();
    }

    public static void TriggerBattleEnd()
    {
        OnBattleEnd?.Invoke();
    }

    public static void TriggerPlacementComplete()
    {
        OnPlacementComplete?.Invoke();
    }
}
