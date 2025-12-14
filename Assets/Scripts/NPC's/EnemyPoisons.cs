using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.NPC
{
    public class EnemyPoisons : Enemy
    {

        public override Effect GetEffect()
        {
            return Effect.poison;
        }
    }
}