using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems
{

    public class BehaviorSystem : MonoBehaviour
    {
        public static BehaviorSystem Instance { get; private set; }

        private class BehaviorEntry
        {
            public int EntityId;
            public NPCData Data;
        }

        private readonly List<BehaviorEntry> entries = new();
        private readonly Dictionary<int, int> indexByEntity = new();


        [SerializeField] private float behaviorTickRate = 0.2f; // 5 Hz
        private float behaviorTimer;

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
            entries.Add(new BehaviorEntry
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
        

        void Update()
        {
            behaviorTimer -= Time.deltaTime;
            if (behaviorTimer > 0f)
                return;

            behaviorTimer = behaviorTickRate;
            RunBehavior();

            
        }

        private void RunBehavior()
        {
            for (int i = 0; i < entries.Count; i++)
            {
                ref NPCData npc = ref entries[i].Data;

                Vector3 toTarget = npc.TargetPosition - npc.Position;
                                
                // Decide intent
                if (toTarget.sqrMagnitude < npc.AggroRange * npc.AggroRange)
                {
                    npc.Intent = NPCIntent.Flee;
                    if (npc.NPCType == NPCType.Enemy && npc.Health > 2)
                    {
                        npc.Intent = NPCIntent.Chase;
                    }
                     
                }
                else
                {
                    npc.Intent = NPCIntent.Idle;
                }


                // Compute desired direction
                if (npc.Intent == NPCIntent.Flee)
                {
                    npc.DesiredDirection = -toTarget.normalized;
                }
                else if (npc.Intent == NPCIntent.Chase)
                {
                    npc.DesiredDirection = toTarget.normalized;
                }
                else
                {
                    npc.DesiredDirection = Vector3.zero;
                }
            }
        }

    }

}
