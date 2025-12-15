
using UnityEngine;

namespace Scripts.NPC
{
    public class EnemyPoisons : Enemy
    {


        protected override void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            this.target = GameObject.Find("Player");
            SetEffect(EffectScript.Effect.poison);
        }
    }
}