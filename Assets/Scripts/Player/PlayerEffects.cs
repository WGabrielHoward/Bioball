
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerEffects : MonoBehaviour
    {

        [Header("Particle Effects")]
        private GameObject smoke;
        private GameObject frost;
        private GameObject healGlow;
        private GameObject poisonDrops;

        private bool healing;
        private bool burning;
        private bool freezing;
        private bool poisoned;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            smoke = GameObject.Find("SmokeTrail");
            frost = GameObject.Find("Frost");
            healGlow = GameObject.Find("HealingGlow");
            poisonDrops = GameObject.Find("PoisonDrops");
        }

        // Update is called once per frame
        void LateUpdate()
        {
            SetEffects();
        }

        void SetEffects()
        {
            healGlow.SetActive(healing);
            smoke.SetActive(burning);
            frost.SetActive(freezing);
            poisonDrops.SetActive(poisoned);

        }

        public void EffectsSwitch(GameObject hitObj, bool setting)  
        {
            EffectScript.Effect tmpEffect = hitObj.GetComponent<NonPlayerCharacter>().GetEffect();  //Restructure so it works for any hit object not just NPC's
            switch (tmpEffect)
            {
                case EffectScript.Effect.heal:
                    healing = setting;
                    break;
                case EffectScript.Effect.poison:
                    poisoned = setting;
                    break;
                case EffectScript.Effect.burn:
                    burning = setting;
                    break;
                case EffectScript.Effect.freeze:
                    freezing = setting;
                    break;
            }
        }


    }
}