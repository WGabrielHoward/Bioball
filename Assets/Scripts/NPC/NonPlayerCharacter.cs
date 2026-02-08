
using UnityEngine;
using Scripts.Interface;
using Scripts.Data;
using Scripts.Systems;
using Scripts;

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
        public int EntityId => npcData.EntityId;

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

            NPCManager.Instance.RegisterNPC( npcData.EntityId, gameObject, npcData, rbThis );

        }

       
        // Update is called once per frame
        void FixedUpdate()
        {
            npcData.Position = this.transform.position;
            npcData.TargetPosition = target.transform.position;
            
            // I should get rid of this and move the z-kill to a global rule-set
            if (transform.position.y < -10)
            {
                HealthSystem.Instance.ApplyDamage(npcData.EntityId, health + 1);
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
           
               
    }

}
