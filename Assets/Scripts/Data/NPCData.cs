
using UnityEngine;

namespace Scripts.Data
{
    public class NPCData
    {
        // relatively static data
        public int EntityId;
        public NPCType NPCType;
        public float AggroRange;

        // slowly changing data
        public NPCIntent Intent;
        public float MoveForce;
        public int Health;

        // fast changing (volatile) data
        public Vector3 Position;
        public Vector3 DesiredDirection;
        public Vector3 TargetPosition;
    }

    public enum NPCIntent
    {
        Idle,
        Chase,
        Flee,
        Patrol,
        Guard
    }

    public enum NPCType
    {
        Enemy,
        Healer,
        Prey
    }
}
