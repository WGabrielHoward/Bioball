using Unity.VisualScripting;
using UnityEngine;


namespace Scripts.NPC
{
    public class EnemyBurns : Enemy
    {
        

        public override Effect GetEffect()
        {
            return Effect.burn;
        }
    }
}