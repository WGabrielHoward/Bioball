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
        // Entity ↔ npcData mapping
        private readonly Dictionary<int, NPCData> entityData = new();

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
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.OnEntityDied += OnEntityDied;
                HealthSystem.Instance.OnHealthChanged += OnHealthChanged;
            }
            
        }

        private void OnDestroy()
        {
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.OnEntityDied -= OnEntityDied;
                HealthSystem.Instance.OnHealthChanged -= OnHealthChanged;
            }
        }

        public void RegisterNPC(int entityId, GameObject go, NPCData data, Rigidbody rb)
        {
            entities.Add(entityId, go);
            entityData.Add(entityId, data);

            BehaviorSystem.Instance?.Register(entityId, data);
            MovementSystem.Instance?.Register(entityId, data, rb);
            HealthSystem.Instance?.Register(entityId, data.Health);

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

            entityData.Remove(entityId);
            entities.Remove(entityId);            
            Destroy(go);
        }

        // only updating here for now. Later I may have behavior check the healthSystem instead
        private void OnHealthChanged(int entityId, int entityHealth)
        {
            
            if (!entityData.TryGetValue(entityId, out var npcData))
                return;
            // somehow update npcHealth here? Seems a waste to add an extra dictionary.
            npcData.Health = entityHealth;
        }


    }
}

