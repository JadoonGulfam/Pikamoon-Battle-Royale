using UnityEngine;
using System.Collections.Generic;

namespace Pikamoon
{
    [System.Serializable]
    public struct PlayerMPInfo
    {
        public string Name;
        public Sprite ProfileIcon;
        public int PositionInFriendList;
        public Transform PlayerTransform;

    }

    public class MultiplayerSingleplayerBridge : MonoBehaviour
    {

        public Sprite[] PointerIcons;
        [Space]
        public List<PlayerMPInfo> Team;

        public int MyCurrentPlayerIndex;

        Transform MyCurrentPlayer;

        bool isInitialized;
        private void Start()
        {
            Team = new List<PlayerMPInfo>();
        }

        public void Initialze(Transform CurrentPlayer)
        {
            MyCurrentPlayer = CurrentPlayer;
        }

        public void AddPlayer(PlayerMPInfo info)
        {
            if (info.PlayerTransform == MyCurrentPlayer)
            {
                MyCurrentPlayerIndex = Team.Count;
            }

            Team.Add(info);
            isInitialized = true;
        }

        private void Update()
        {
            UpdateAllTeamMembersOnMiniMap();
        }

        void UpdateAllTeamMembersOnMiniMap()
        {
            foreach (PlayerMPInfo info in Team)
            { 

            }
        }
    }

}