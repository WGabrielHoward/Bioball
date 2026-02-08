
using System.Collections.Generic;
using Scripts.Interface;
using Scripts.Systems;
using UnityEngine;

namespace Scripts
{
    public class DamageRouter : MonoBehaviour
    {
        public delegate void DamageSink(int entityId, int amount);

        public static DamageRouter Instance { get; private set; }
        private static readonly Dictionary<int, DamageSink> routes = new();

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

        public void Register(int entityId, DamageSink sink)
        {
            routes[entityId] = sink;
        }

        public void Unregister(int entityId)
        {
            routes.Remove(entityId);
        }

        public void ApplyDamage(int entityId, int amount)
        {
            if (!routes.TryGetValue(entityId, out var sink))
                return;

            sink(entityId, amount);
        }
    }

}
