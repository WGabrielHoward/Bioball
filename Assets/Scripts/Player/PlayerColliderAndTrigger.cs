
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerColliderAndTrigger : MonoBehaviour
    {

        private PlayerEffects playerEffects;

        private void Start()
        {
            playerEffects = gameObject.GetComponent<PlayerEffects>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, true);
                //Hit(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Victory"))
            {
                LevelManager.ManInstance.Victory();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, true);
                int damage = collision.gameObject.GetComponent<NonPlayerCharacter>().GetDamage();
                this.GetComponent<PlayerStats>().Damage(damage);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                playerEffects.EffectsSwitch(other.gameObject, true);
                //Hit(other.gameObject);
            }
            if (other.gameObject.CompareTag("Victory"))
            {
                LevelManager.ManInstance.Victory();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                playerEffects.EffectsSwitch(other.gameObject, true);
                int damage = other.gameObject.GetComponent<NonPlayerCharacter>().GetDamage();
                this.GetComponent<PlayerStats>().Damage(damage);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                playerEffects.EffectsSwitch(other.gameObject, false);
            }
        }

    }
}