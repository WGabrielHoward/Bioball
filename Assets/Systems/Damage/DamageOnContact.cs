using Assets.Systems.Damage;
using UnityEngine;

namespace Assets.Systems.Damage
{


    public class DamageOnContact : MonoBehaviour , IDamageSource
    {
        [SerializeField] private Element element;
        [SerializeField] private int damagePerTick;
        [SerializeField] private float tickRate;
        [SerializeField] private float lingerDuration; // how long DOT continues after exit
        
        public Element Element => element;
        public int DamagePerTick => damagePerTick;
        public float TickRate => tickRate;
        public GameObject Owner => this.gameObject;

        private DamageSystem damageSystem;

        void Start()
        {
            damageSystem = FindAnyObjectByType<DamageSystem>();
        }

        void OnCollisionEnter(Collision other)
        {

            if (!other.gameObject.TryGetComponent<IDamageable>(out var target))
            {
                return;
            }

            if (!TryGetComponent<IDamageSource>(out var source))
            {
                return;
            }

            // Commented this out because it is applying a rule, which it shouldn't know. 
            // DOC doesn't need to know who can hurt who, it just needs to send info.
            // ... I put it back in because the refactor for damage-rules would be getting ahead of myself
            //// Same-element immunity
            if (target.Element == source.Element)
            {
                return;
            }

            damageSystem.ApplyDamageOverTime( target, source.DamagePerTick, source.TickRate, lingerDuration);
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var target))
            {
                damageSystem.StartLinger(target, lingerDuration);
            }
        }


        void OnTriggerEnter(Collider other)
        {

            if (!other.gameObject.TryGetComponent<IDamageable>(out var target))
            {
                return;
            }

            if (!TryGetComponent<IDamageSource>(out var source))
            {
                return;
            }

            // Same-element immunity
            if (target.Element == source.Element)
            {
                return;
            }

            damageSystem.ApplyDamageOverTime(target, source.DamagePerTick, source.TickRate, lingerDuration);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var target))
            {
                damageSystem.StartLinger(target, lingerDuration);
            }
        }


    }
}
