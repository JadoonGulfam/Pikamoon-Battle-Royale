using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private Transform leaderboardContainer;
    [SerializeField] private GameObject leaderboardEntryPrefab;

    private readonly List<PlayerStats> players = new List<PlayerStats>();
    private readonly Dictionary<PlayerStats, GameObject> entryMap = new Dictionary<PlayerStats, GameObject>();

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

        leaderboardPanel.SetActive(false); // start hidden
    }

    private void Update()
    {
        // Toggle leaderboard with Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            leaderboardPanel.SetActive(true);
            UpdateLeaderboard();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            leaderboardPanel.SetActive(false);
        }
    }

    public void RegisterPlayer(PlayerStats player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);

            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
            entryMap[player] = entry;
        }

        UpdateLeaderboard();
    }

    public void UnregisterPlayer(PlayerStats player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);

            if (entryMap.TryGetValue(player, out var entry))
            {
                Destroy(entry);
                entryMap.Remove(player);
            }
        }

        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        // Order players by kills (desc), then by name
        var orderedPlayers = players
            .OrderByDescending(p => p.Kills)
            .ThenBy(p => p.PlayerName)
            .ToList();

        for (int i = 0; i < orderedPlayers.Count; i++)
        {
            var player = orderedPlayers[i];

            // Assign rank (starts from 1)
            player.SetRank(i + 1);

            // Update UI entry
            if (entryMap.TryGetValue(player, out var entry))
            {
                TMP_Text[] texts = entry.GetComponentsInChildren<TMP_Text>();
                if (texts.Length >= 3)
                {
                    texts[0].text = player.PlayerName;
                    texts[1].text = player.Kills.ToString();
                    texts[2].text = player.Rank.ToString();
                }
            }
        }
    }
}
