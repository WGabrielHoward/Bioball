
using UnityEngine;

using Scripts.Systems;
using Scripts.Interface;

namespace Scripts.Player
{
    public class PlayerColliderAndTrigger : MonoBehaviour
    {

        private PlayerEffects playerEffects;

        //public void Initialize(PlayerEffects effects)
        //{
        //    this.playerEffects = effects;
        //}

        void Start()
        {
            this.playerEffects = gameObject.GetComponent<PlayerEffects>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageSource>(out var elementSource))
            {
                Effect effect = ElementRules.GetStatusForElement(elementSource.Element);
                playerEffects.EffectsSwitch(effect, true);
            }
            if (other.gameObject.CompareTag("Victory"))
            {
                GameStateSystem.Instance.TriggerVictory();
            }
        }


        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageSource>(out var elementSource))
            {
                Effect effect = ElementRules.GetStatusForElement(elementSource.Element);
                playerEffects.EffectsSwitch(effect, false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageSource>(out var elementSource))
            {
                Effect effect = ElementRules.GetStatusForElement(elementSource.Element);
                playerEffects.EffectsSwitch(effect, true);
            }
            if (other.gameObject.CompareTag("Victory"))
            {
                GameStateSystem.Instance.TriggerVictory();
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageSource>(out var elementSource))
            {
                Effect effect = ElementRules.GetStatusForElement(elementSource.Element);
                playerEffects.EffectsSwitch(effect, false);
            }
        }

    }
}