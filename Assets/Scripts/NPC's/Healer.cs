
using UnityEngine;


namespace Scripts.NPC
{
    public class Healer : NonPlayerCharacter
    {

        public override Effect GetEffect()
        {
            return Effect.heal;
        }
    }
}