
using UnityEngine;

namespace Scripts.NPC
{
  
    public class NonPlayerCharacter : MonoBehaviour
    {
        protected Rigidbody rbThis;
        [SerializeField] public GameObject target;
        [SerializeField] protected float forceToTarget = 1;
        [SerializeField] protected int health = 10;
        [SerializeField] protected int damage = 0;
        [SerializeField] protected int damagePerTick = 0;
        [SerializeField] protected float tickRate = 1f;
        [SerializeField] protected EffectScript thisEffect;
        [SerializeField] protected DamageOverTime damageOverTime;

        void Awake()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            thisEffect = gameObject.AddComponent<EffectScript>();
            damageOverTime = gameObject.AddComponent<DamageOverTime>();
            damageOverTime.SetDamagePerTick(damagePerTick);
            damageOverTime.SetTickRate(tickRate);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (target)
            {
                Vector3 lookDirection = (target.transform.position - transform.position).normalized;
                rbThis.AddForce(lookDirection * forceToTarget);
            }

            if (transform.position.y < -10)
            {
                Death();
            }
        }

        public virtual void SetTarget(GameObject newTarget)
        {
            if (newTarget == null)
            {
                target = GameObject.Find("Player");
            }
            else target = newTarget;

        }


        protected virtual void Death()
        {
            Destroy(gameObject);
        }

        public virtual Effect GetEffect()
        {
            return thisEffect.GetEffect();
        }
        public virtual void SetEffect(Effect newEffect)
        {
            thisEffect.SetEffect(newEffect);
        }

        public virtual int GetDamage()
        {
            return damage;
        }

        public virtual DamageOverTime GetDamageOverTime()
        {
            return damageOverTime;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Death();
            }
        }

    }

}
