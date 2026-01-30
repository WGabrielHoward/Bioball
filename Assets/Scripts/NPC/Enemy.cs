
using UnityEngine;

namespace Scripts.NPC
{

    public class Enemy : NonPlayerCharacter
    {
        protected override void Start()
        {
            this.target = GameObject.Find("Player");
        }
    }
}