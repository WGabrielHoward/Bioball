
using UnityEngine;


namespace Scripts.NPC
{
    public class Enemy : NonPlayerCharacter
    {
        protected override void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            this.target = GameObject.Find("Player");
        }
    }
}