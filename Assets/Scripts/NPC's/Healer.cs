
using UnityEngine;


namespace Scripts.NPC
{
    public class Healer : NonPlayerCharacter
    {

        protected override void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            //this.target = GameObject.Find("Player");
            SetEffect(EffectScript.Effect.heal);
        }

    }
}