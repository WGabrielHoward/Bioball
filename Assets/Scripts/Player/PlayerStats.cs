
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

        private void Awake()
        {
            health = maxHealth;
            entityId = gameObject.GetComponent<PlayerEntity>().EntityId;

            HealthSystem.Instance.Register(EntityId, maxHealth);
            DamageRouter.Instance?.Register(EntityId, HealthSystem.Instance.ApplyDamage);

            UpdateUI();
        }
        private void OnEnable()
        {
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.OnHealthChanged += HealthChanged;
                HealthSystem.Instance.OnEntityDied += EntityDied;
            }
        }

        private void OnDisable()
        {
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.OnHealthChanged -= HealthChanged;
                HealthSystem.Instance.OnEntityDied -= EntityDied;
            }
        }

        public void HealthChanged(int nEntityId, int currentHealth)
        {
            if (nEntityId == entityId)
            {
                health = currentHealth;
                UpdateUI();
            }
            
        }

        public void EntityDied(int deadEntityId)
        {
            if (deadEntityId == entityId)
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