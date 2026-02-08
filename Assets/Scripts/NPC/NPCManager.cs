using System.Collections.Generic;
using UnityEngine;

using Scripts.Data;
using Scripts.Systems;

namespace Scripts.NPC
{
    public class NPCManager : MonoBehaviour
    {
        public static NPCManager Instance { get; private set; }

        // Entity ↔ GameObject mapping
        private readonly Dictionary<int, GameObject> entities = new();

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

        private void Start()
        {
            HealthSystem.Instance.OnEntityDied += OnEntityDied;
        }

        private void OnDestroy()
        {
            if (HealthSystem.Instance != null)
                HealthSystem.Instance.OnEntityDied -= OnEntityDied;
        }

        public void RegisterNPC(int entityId, GameObject go, NPCData data, Rigidbody rb)
        {
            entities.Add(entityId, go);

            BehaviorSystem.Instance?.Register(entityId, data);
            MovementSystem.Instance?.Register(entityId, data, rb);
            HealthSystem.Instance?.Register(entityId, data);

            DamageRouter.Instance.Register( entityId, HealthSystem.Instance.ApplyDamage );

        }

        private void OnEntityDied(int entityId)
        {
            if (!entities.TryGetValue(entityId, out var go))
                return;

            BehaviorSystem.Instance?.Unregister(entityId);
            MovementSystem.Instance?.Unregister(entityId);
            HealthSystem.Instance?.Unregister(entityId);
            DamageRouter.Instance?.Unregister(entityId);

            entities.Remove(entityId);
            Destroy(go);
        }
    }
}

