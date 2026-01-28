
using UnityEngine;

using Scripts.NPC;
using Assets.Systems.Damage;
using Assets.Systems.GameState;
namespace Scripts.Player
{
    public class PlayerColliderAndTrigger : MonoBehaviour
    {

        private PlayerEffects playerEffects;
        private PlayerScriptManager playSMan;

        private void Awake()
        {
            playSMan = gameObject.GetComponent<PlayerScriptManager>();
            
        }
        private void Start()
        {
            playerEffects = playSMan.GetPlayerEffects();
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

        private void OnCollisionStay(Collision other)
        {
            
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

        private void OnTriggerStay(Collider other)
        {
           
            
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