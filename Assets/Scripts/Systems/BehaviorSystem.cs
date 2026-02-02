using Assets.Scripts.Data;
using Scripts.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class BehaviorSystem : MonoBehaviour
    {
        public static BehaviorSystem Instance { get; private set; }

        private readonly List<NPCData> npcs = new();
        [SerializeField] private float behaviorTickRate = 0.2f; // 5 Hz
        private float behaviorTimer;

        //public BehaviorSystem(List<NPCData> npcs)
        //{
        //    this.npcs = npcs;
        //}
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

        public void RegisterNPC(NPCData npcData)
        {
            //UnityEngine.Debug.Log("npc registered");
            if (!npcs.Contains(npcData))
                npcs.Add(npcData);
        }
       

        public void UnregisterNPC(NPCData npcData)
        {
            //UnityEngine.Debug.Log("npc Unregistered");
            npcs.Remove(npcData);
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
            Vector3 goalVec;
            foreach (var npc in npcs)
            {
                if (npc.TargetPosition == null) continue;

                goalVec = npc.TargetPosition - npc.Position;

                // Decide intent
                if (goalVec.sqrMagnitude < npc.AggroRange*npc.AggroRange)
                {
                    if (npc.NPCType == NPCType.Enemy)
                    {
                        npc.Intent = NPCIntent.Chase;
                    }
                    else if (npc.NPCType == NPCType.Healer || npc.NPCType == NPCType.Prey)
                    {
                        npc.Intent = NPCIntent.Flee;
                    }
                }

                // Low health override
                if (npc.Health < 2)
                {
                    npc.Intent = NPCIntent.Flee;
                }

                // Determine movement direction
                if (npc.Intent == NPCIntent.Flee)
                {
                    npc.DesiredDirection = (-goalVec).normalized;
                }
                else if (npc.Intent == NPCIntent.Chase)
                {
                    npc.DesiredDirection = (goalVec).normalized;
                }
                else
                {
                    npc.DesiredDirection = Vector3.zero;
                }

            }
        }
    }

}
