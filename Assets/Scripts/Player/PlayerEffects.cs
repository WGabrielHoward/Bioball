using UnityEngine;

namespace Scripts.Player
{
    public class PlayerEffects : MonoBehaviour
    {
        [Header("Particle Effects")]
        [SerializeField] private GameObject smoke;
        [SerializeField] private GameObject frost;
        [SerializeField] private GameObject healGlow;
        [SerializeField] private GameObject poisonDrops;

        private bool healing;
        private bool burning;
        private bool freezing;
        private bool poisoned;

        private void Awake()
        {
            SetEffects();
        }

        private void SetEffects()
        {
            healGlow.SetActive(healing);
            smoke.SetActive(burning);
            frost.SetActive(freezing);
            poisonDrops.SetActive(poisoned);
        }

        public void EffectsSwitch(Effect effect, bool setting)
        {
            switch (effect)
            {
                case Effect.Heal: healing = setting; break;
                case Effect.Poison: poisoned = setting; break;
                case Effect.Burn: burning = setting; break;
                case Effect.Freeze: freezing = setting; break;
            }

            SetEffects();
        }
    }
}
