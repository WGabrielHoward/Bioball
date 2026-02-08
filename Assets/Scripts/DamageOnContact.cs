using Scripts.Systems;
using UnityEngine;

using Scripts.Interface;

    public class DamageOnContact : MonoBehaviour, IDamageSource
    {
        [SerializeField] private Element element;
        [SerializeField] private int damagePerTick;
        [SerializeField] private float tickRate;
        [SerializeField] private float lingerDuration;

        public Element Element => element;
        public int DamagePerTick => damagePerTick;
        public float TickRate => tickRate;
        //public GameObject Owner => this.gameObject;

        private DamageSystem damageSystem;

        private void Start()
        {
            damageSystem = FindAnyObjectByType<DamageSystem>();
        }

        private void HandleCollision(IDamageable target, GameObject other)
        {
            if (target.Element == Element) return; // same-element immunity

            damageSystem.ApplyDamage(
                target.EntityId,
                target.Element,
                damagePerTick,
                tickRate,
                lingerDuration
            );
        }

        private void HandleExit(IDamageable target)
        {
            damageSystem.StartLinger(target.EntityId, lingerDuration);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var target))
                HandleCollision(target,other.gameObject);
            
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var target))
                HandleExit(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var target))
                HandleCollision(target,other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var target))
                HandleExit(target);
        }
    }

