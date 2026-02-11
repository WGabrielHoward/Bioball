using Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    public class HealthSystem : MonoBehaviour
    {
        public static HealthSystem Instance { get; private set; }

        private readonly List<HealthComponent> entries = new();
        private readonly Dictionary<int, int> indexByEntity = new();

        // Pure signals — no gameplay logic
        public event Action<int, int> OnHealthChanged;
        public event Action<int> OnEntityDied;

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

        // ---------------------- Registration ----------------------

        public void Register(int entityId, int maxHealth)
        {
            if (indexByEntity.ContainsKey(entityId)) return;

            var health = new HealthComponent
            {
                EntityId = entityId,
                Current = maxHealth,
                Max = maxHealth
            };

            indexByEntity[entityId] = entries.Count;
            entries.Add(health);
        }

        public void Unregister(int entityId)
        {
            if (!indexByEntity.TryGetValue(entityId, out int index)) return;

            int lastIndex = entries.Count - 1;

            entries[index] = entries[lastIndex];
            indexByEntity[entries[index].EntityId] = index;

            entries.RemoveAt(lastIndex);
            indexByEntity.Remove(entityId);
        }

        // ---------------------- Damage ----------------------

        public void ApplyDamage(int entityId, int amount)
        {
            if (!indexByEntity.TryGetValue(entityId, out int index)) return;

            var health = entries[index];

            health.Current -= amount;

            // in case of healing via negative damage
            if (health.Current > health.Max)
                health.Current = health.Max;


            entries[index] = health;

            OnHealthChanged?.Invoke(entityId, health.Current);

            if (health.Current <= 0)
                OnEntityDied?.Invoke(entityId);
        }

        public void Heal(int entityId, int amount)
        {
            if (!indexByEntity.TryGetValue(entityId, out int index)) return;

            var health = entries[index];

            health.Current += amount;

            if (health.Current > health.Max)
                health.Current = health.Max;

            entries[index] = health;

            OnHealthChanged?.Invoke(entityId, health.Current);
        }

        // ---------------------- Queries ----------------------

        public int GetCurrentHealth(int entityId)
        {
            if (!indexByEntity.TryGetValue(entityId, out int index)) return 0;
            return entries[index].Current;
        }

        public int GetMaxHealth(int entityId)
        {
            if (!indexByEntity.TryGetValue(entityId, out int index)) return 0;
            return entries[index].Max;
        }
    }
}

