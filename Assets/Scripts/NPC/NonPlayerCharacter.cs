
using UnityEngine;
using Scripts.Systems;
using Scripts.Interface;
using Assets.Scripts.Data;
using Assets.Scripts.Systems;
using NUnit.Framework;

namespace Scripts.NPC
{


    public class NonPlayerCharacter : MonoBehaviour, IDamageable
    {
        protected Rigidbody rbThis;
        [SerializeField] protected float forceToTarget = 1;
        [SerializeField] protected int health = 10;
        [SerializeField] private float aggroRange;
        [SerializeField] private NPCType npcType;
        [SerializeField] private Element element = Element.None;
        public Element Element => element;

        [SerializeField] private GameObject target;


        public NPCData npcData { get; private set; }

        void Awake()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();

            npcData = new NPCData
            {
                Health = health,
                MoveForce = forceToTarget,
                Position = transform.position,
                AggroRange = aggroRange,
                NPCType = npcType
            };

            SetTarget(null);
            SelfRegister();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            npcData.Position = this.transform.position;
            npcData.TargetPosition = target.transform.position;
            if (npcData.Intent != NPCIntent.Idle)
            {
                ApplyMovement();
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
                //npcData.Target = GameObject.Find("Player").transform.position;
            }
            //else npcData.Target = newTarget.transform.position;
            else target = newTarget;
            npcData.TargetPosition = target.transform.position;

        }

        private void SetByType()
        {
            // set info by type


        }

        protected virtual void Death()
        {
            SelfUnRegister();
            Destroy(gameObject);
        }


        public void TakeDamage(int damage)
        {
            npcData.Health -= damage;
            if (npcData.Health <= 0)
            {
                Death();
            }
        }

        public void ApplyDamage(int amount)
        {
            TakeDamage(amount);
        }


        private void ApplyMovement()
        {
            rbThis.AddForce(npcData.DesiredDirection * npcData.MoveForce);
        }

        public void SelfRegister()
        {
            BehaviorSystem.Instance.RegisterNPC(this.npcData);
        }

        public void SelfUnRegister()
        {
            BehaviorSystem.Instance.UnregisterNPC(this.npcData);
        }
    }

}
