
using UnityEngine;
using Scripts.Systems;
using Scripts.Interface;

namespace Scripts.NPC
{


    public class NonPlayerCharacter : MonoBehaviour, IDamageable
    {
        protected Rigidbody rbThis;
        [SerializeField] public GameObject target;
        [SerializeField] protected float forceToTarget = 1;
        [SerializeField] protected int health = 10;
        [SerializeField] private Element element = Element.None;
        public Element Element => element;

        void Awake()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
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


        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Death();
            }
        }

        public void ApplyDamage(int amount)
        {
            TakeDamage(amount);
        }

    }

}
