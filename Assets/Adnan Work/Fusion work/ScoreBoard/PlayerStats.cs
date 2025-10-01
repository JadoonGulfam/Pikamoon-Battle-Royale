using Fusion;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    // Networked properties synced to all players
    [Networked] public string PlayerName { get; set; }
    [Networked] public int Kills { get; set; }
    [Networked] public int Rank { get; set; }

    public override void Spawned()
    {
        // Register this player in the leaderboard when spawned
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.RegisterPlayer(this);
        }

        // Only the local owner sets their own name
        if (Object.HasInputAuthority)
        {
            // You can replace this with a UI input from your GameManager
            PlayerName = "Player_" + Random.Range(1000, 9999);
        }
    }

    /// <summary>
    /// Call this when the player makes a kill.
    /// </summary>
    public void AddKill()
    {
        Kills++;
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.UpdateLeaderboard();
        }
    }

    /// <summary>
    /// Update player rank (can be called from leaderboard manager).
    /// </summary>
    public void SetRank(int newRank)
    {
        Rank = newRank;
    }
}
