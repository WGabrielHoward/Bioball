using System.Collections.Generic;
using Scripts.Player;

namespace Scripts.Systems
{
    public static class PlayerRegistry
    {
        private static List<PlayerData> players = new();

        public static int Count => players.Count;

        public static PlayerData Get(int index) => players[index];

        public static void SetMoveIntent(int index, float move)
        {
            var p = players[index];
            p.MoveIntent = move;
            players[index] = p;
        }
        public static PlayerData GetByEntityId(int entityId)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].EntityId == entityId)
                {
                    return players[i];
                }
            }

            return default;
        }


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
