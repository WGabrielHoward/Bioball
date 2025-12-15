
using UnityEngine;


namespace Scripts.NPC
{
    public class EnemyBurns : Enemy
    {

        protected override void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            this.target = GameObject.Find("Player");
            SetEffect(EffectScript.Effect.burn);
        }
    }
}