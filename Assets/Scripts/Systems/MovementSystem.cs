using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    
    public class MovementSystem : MonoBehaviour
    {
        private class MovementEntry
        {
            public int EntityId;
            public bool isPlayer;
            public NPCData NPC;
            public Rigidbody Body;
        }

        private readonly List<MovementEntry> entries = new();
        private readonly Dictionary<int, int> indexByEntity = new();

        public static MovementSystem Instance { get; private set; }



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

        private void FixedUpdate()
        {

            //for (int i = 0; i < entries.Count; i++)
            //{
            //    var entry = entries[i];
            //    var npc = entry.Data;

            //    if (npc.Intent == NPCIntent.Idle)
            //        continue;

            //    if (entry.Body == null)
            //        continue;
            //    entry.Body.AddForce(npc.DesiredDirection * npc.MoveForce);
            //}
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];

                if (entry.Body == null)
                    continue;
                // ───────── Player movement ─────────
                if (entry.isPlayer)
                {
                    var player = PlayerRegistry.GetByEntityId(entry.EntityId);

                    if (Mathf.Abs(player.MoveIntent) < 0.01f)
                        continue;

                    Vector3 forward = player.FocalPoint.forward;
                    forward.y = 0f;
                    forward.Normalize();

                    entry.Body.AddForce(forward * player.MoveIntent * 5f);
                }
                // ───────── NPC movement ─────────
                else
                {
                    var npc = entry.NPC;

                    if (npc.Intent == NPCIntent.Idle)
                        continue;

                    entry.Body.AddForce( npc.DesiredDirection * npc.MoveForce);
                }
                
                
            }

        }

        public void Register(int entityId, NPCData data, Rigidbody body)
        {
            if (indexByEntity.ContainsKey(entityId))
                return;

            indexByEntity[entityId] = entries.Count;
            entries.Add(new MovementEntry
            {
                EntityId = entityId,
                NPC = data,
                Body = body
            });
        }
        public void RegisterPlayer(PlayerData data)
        {
            if (indexByEntity.ContainsKey(data.EntityId))
                return;

            indexByEntity[data.EntityId] = entries.Count;

            entries.Add(new MovementEntry
            {
                EntityId = data.EntityId,
                isPlayer = true,
                Body = data.Rigidbody
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

    }
}
