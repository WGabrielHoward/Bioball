using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerStats : MonoBehaviour
    {

        [Header("Player Stats")]
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private int health = 100;

        LevelCanvas levelCanvas;

        private void Start()
        {
            levelCanvas = FindAnyObjectByType<LevelCanvas>();
        }

        public float GetForwardSpeed()
        {
            return forwardSpeed;
        }

        public void Hit(GameObject other)
        {
            int damage = other.GetComponent<NonPlayerCharacter>().GetDamage();
            StartCoroutine(TakeDamage(damage));
        }

        IEnumerator TakeDamage(int damage)
        {
            yield return new WaitForSeconds(.5f);
            health -= damage;
            UpdateHealthText();
            if (health <= 0)
            {
                LevelManager.ManInstance.GameOver();
            }
        }

        public void Damage(int damage)
        {
            health -= damage;
            UpdateHealthText();
            if (health <= 0)
            {
                LevelManager.ManInstance.GameOver();
            }
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