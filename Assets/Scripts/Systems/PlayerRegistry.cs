using System.Collections.Generic;
using Scripts.Player;

namespace Scripts.Systems
{
    public static class PlayerRegistry
    {
        private static readonly List<PlayerData> players = new();

        public static IReadOnlyList<PlayerData> Players => players;

        public static void Register(PlayerData data)
        {
            players.Add(data);
        }

        public static void Unregister(int entityId)
        {
            players.RemoveAll(p => p.EntityId == entityId);
        }
    }
}
