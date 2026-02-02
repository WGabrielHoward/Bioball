
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class NPCData
    {
        public int EntityId;
        public NPCIntent Intent;
        public float MoveForce;
        public int Health;
        public NPCType NPCType;

        public float AggroRange;
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
