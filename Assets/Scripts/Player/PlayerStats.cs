using System.Collections;
using UnityEngine;

using Scripts.NPC;
using Scripts.Systems;
using Unity.Properties;
using Scripts.Interface;

namespace Scripts.Player
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        private PlayerScriptManager playSMan;
        private PlayerController playerController;
        protected Element element;
        public Element Element => element;

        [Header("Player Stats")]
        private float forwardSpeed;
        private int health;

        LevelCanvas levelCanvas;

        private void Awake()
        {
            playSMan = gameObject.GetComponent<PlayerScriptManager>();
            
        }
        private void Start()
        {
            levelCanvas = FindAnyObjectByType<LevelCanvas>();
            playerController = playSMan.GetPlayerController();
            PullForwardSpeed();
            PullHealth();
        }

        public float GetForwardSpeed()
        {
            return forwardSpeed;
        }

        public void SetForwardSpeed(float newSpeed)
        {
            forwardSpeed = newSpeed;
            playerController.SetForwardSpeed(forwardSpeed);
        }

        private void PullForwardSpeed()
        {
            forwardSpeed = playSMan.GetForwardSpeed();
        }

        public Element GetElement()
        {
            return this.element;
        }

        protected void SetElement(Element newElement)
        {
            this.element = newElement;
        }

        private void PullHealth()
        {
            health = playSMan.GetHealth();
        }


        public void TakeDamage(int damage)
        {
            // PlayerStats.TakeDamage
            Debug.Log("PlayerStats took damage: " + health);
            health -= damage;
            UpdateHealthText();
            if (health <= 0)
            {
                GameStateSystem.Instance.TriggerDefeat();
            }
        }

        public void ApplyDamage(int amount)
        {
            // PlayerStats.ApplyDamage
            Debug.Log("PlayerStats Apply damage: " + health);
            TakeDamage(amount);
        }

        private void UpdateHealthText()
        {
            if (levelCanvas)
            {
                levelCanvas.HealthUpdate(health);
            }
        }

    }
}