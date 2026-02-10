using Scripts.Interface;
using Scripts.Systems;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(PlayerEffects))]
    public class PlayerColliderAndTrigger : MonoBehaviour
    {
        private PlayerEffects playerEffects;

        private void Awake()
        {
            playerEffects = GetComponent<PlayerEffects>();
        }

        private void OnCollisionEnter(Collision other)
        {
            HandleEnter(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleEnter(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            HandleExit(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            HandleExit(other.gameObject);
        }

        private void HandleEnter(GameObject obj)
        {
            if (obj.TryGetComponent<IDamageSource>(out var source))
            {
                var effect = ElementRules.GetStatusForElement(source.Element);
                playerEffects.EffectsSwitch(effect, true);
            }

            if (obj.CompareTag("Victory"))
            {
                GameStateSystem.Instance.TriggerVictory();
            }
        }

        private void HandleExit(GameObject obj)
        {
            if (obj.TryGetComponent<IDamageSource>(out var source))
            {
                var effect = ElementRules.GetStatusForElement(source.Element);
                playerEffects.EffectsSwitch(effect, false);
            }
        }
    }
}
