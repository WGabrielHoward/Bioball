using UnityEngine;

namespace Scripts.Systems
{
    public struct PlayerData
    {
        public int EntityId;

        // Unity bridges (temporary, Phase 2 only)
        public Rigidbody Rigidbody;
        public Transform FocalPoint;

        // Intent (used later)
        public float MoveIntent;
        //public float RotateIntent;
    }
}
