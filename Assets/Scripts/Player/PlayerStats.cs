
using UnityEngine;

using Scripts.Systems;
using Scripts.Interface;

namespace Scripts.Player
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        protected Element element;
        public Element Element => element;
        public int EntityId => entityId;

        [SerializeField] private int maxHealth = 100;
        private int entityId;
        private int health;
        LevelCanvas levelCanvas;

        private void Awake()
        {
            health = maxHealth;
            entityId = gameObject.GetComponent<PlayerEntity>().EntityId;
            
            DamageRouter.Instance?.Register(EntityId, ApplyDamage);
            
            UpdateUI();
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
            FindAnyObjectByType<LevelCanvas>()?.HealthUpdate(health);
        }

    }
}