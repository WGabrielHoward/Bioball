
using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    // Player isn't ready for this and DamageSystem isn't ready to split between different health pools...
    // so this isn't really doing anything for now but registering the npcs
    public class HealthSystem : MonoBehaviour
    {
        public static HealthSystem Instance { get; private set; }

        private class HealthEntry
        {
            public int EntityId;
            public NPCData Data;
        }

        private readonly List<HealthEntry> entries = new();
        private readonly Dictionary<int, int> indexByEntity = new();

        public event System.Action<int> OnEntityDied;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Register(int entityId, NPCData data)
        {
            if (indexByEntity.ContainsKey(entityId))
                return;

            indexByEntity[entityId] = entries.Count;
            entries.Add(new HealthEntry
            {
                EntityId = entityId,
                Data = data
            });
        }

        public void Unregister(int entityId)
        {
            if (!indexByEntity.TryGetValue(entityId, out int index))
                return;

            int last = entries.Count - 1;

            entries[index] = entries[last];
            indexByEntity[entries[index].EntityId] = index;

            entries.RemoveAt(last);
            indexByEntity.Remove(entityId);
        }

        public void ApplyDamage(int entityId, int amount)
        {
            
            if (!indexByEntity.TryGetValue(entityId, out int index))
                return;

            NPCData npc = entries[index].Data;
            UnityEngine.Debug.Log($"HealthSystem applied damage {amount} to {entityId}.");
                        
            npc.Health -= amount;

            if (npc.Health <= 0)
            {
                HandleDeath(entityId);
                return;
            }
        }

        private void HandleDeath(int entityId)
        {
            UnityEngine.Debug.Log($"HealthSystem HandleDeath of {entityId}.");
            OnEntityDied?.Invoke(entityId);
        }
    }
}


