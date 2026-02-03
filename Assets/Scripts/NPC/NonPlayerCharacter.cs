
using UnityEngine;
using Scripts.Interface;
using Assets.Scripts.Data;
using Assets.Scripts.Systems;
using Assets.Scripts;

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
                EntityId = EntityIdGenerator.Next(),
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
            npcData.TargetPosition = target.transform.position;

        }

        protected virtual void Death()
        {
            SelfUnRegister();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            SelfUnRegister();
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

        public void SelfRegister()
        {
            if (BehaviorSystem.Instance != null)
            {
                BehaviorSystem.Instance.Register(this.npcData.EntityId, npcData);
            }
            if (MovementSystem.Instance != null)
            {
                MovementSystem.Instance.Register(this.npcData.EntityId, npcData, this.rbThis);
            }

        }

        public void SelfUnRegister()
        {
            if (BehaviorSystem.Instance != null)
            {
                BehaviorSystem.Instance.Unregister(this.npcData.EntityId);
            }
            if (MovementSystem.Instance != null)
            {
                MovementSystem.Instance.Unregister(this.npcData.EntityId);
            }
        }
    }

}
