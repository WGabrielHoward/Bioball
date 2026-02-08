
using UnityEngine;

using Scripts.Systems;
using Scripts.Interface;

namespace Scripts.Player
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        protected Element element;
        public Element Element => element;
        public int EntityId { get; private set; }
        private int health;
        LevelCanvas levelCanvas;

        public void Initialize(int startingHealth)
        {
            health = startingHealth;
            UpdateUI();
        }

        private void Awake()
        {
            EntityId = EntityIdGenerator.Next();
            if (DamageRouter.Instance != null)
            {
                DamageRouter.Instance.Register(EntityId, ApplyDamage);
            }

        }
                      

        public void ApplyDamage(int entityId, int damage)
        {
            health -= damage;
            UpdateUI();
            if (health <= 0)
            {
                GameStateSystem.Instance.TriggerDefeat();
            }
        }

        private void UpdateUI()
        {
            levelCanvas = FindAnyObjectByType<LevelCanvas>();
            if (levelCanvas)
            {
                levelCanvas.HealthUpdate(health);
            }
        }

    }
}