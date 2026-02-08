
using UnityEngine;

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
        void FixedUpdate()
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


        public void EffectsSwitch(Effect tmpEffect, bool setting)  
        {
            //Effect tmpEffect = hitObj.GetComponent<EffectScript>().GetEffect();
            switch (tmpEffect)
            {
                case Effect.Heal:
                    healing = setting;
                    break;
                case Effect.Poison:
                    poisoned = setting;
                    break;
                case Effect.Burn:
                    burning = setting;
                    break;
                case Effect.Freeze:
                    freezing = setting;
                    break;
            }

        }


    }
}