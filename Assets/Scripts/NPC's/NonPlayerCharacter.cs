
using UnityEngine;

namespace Scripts.NPC
{
    public enum Effect
    {
        burn,
        freeze,
        poison,
        heal,
        unnassigned
    }

    public class NonPlayerCharacter : MonoBehaviour
    {
        protected Rigidbody rbThis;
        [SerializeField] public GameObject target;
        [SerializeField] protected float forceToTarget = 1;
        //public virtual Effect effectType;
        [SerializeField] protected int health = 10;
        [SerializeField] protected int damage = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            //if (target == null)
            //{
            //    target = this.gameObject;
            //}
            
        }

        // Update is called once per frame
        void Update()
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
            return Effect.unnassigned;
        }

        public virtual int GetDamage()
        {
            return damage;
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
