
using UnityEngine;

namespace Scripts.NPC
{
    public class EnemyFreezes : Enemy
    {


        protected override void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            this.target = GameObject.Find("Player");
            SetEffect(EffectScript.Effect.freeze);
        }
    }
}