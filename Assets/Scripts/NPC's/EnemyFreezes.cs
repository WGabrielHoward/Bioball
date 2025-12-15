
using UnityEngine;

namespace Scripts.NPC
{
    public class EnemyFreezes : Enemy
    {
        
        public override Effect GetEffect()
        {
            return Effect.freeze;
        }
    }
}