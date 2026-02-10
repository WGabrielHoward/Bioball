using UnityEngine;
using Scripts.Systems;

namespace Scripts.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        public int EntityId { get; private set; }

        [Header("Unity Bridges")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform focalPoint;

        private void Awake()
        {
            EntityId = EntityIdGenerator.Next();

            rb = gameObject.GetComponent<Rigidbody>();
            focalPoint = rb.transform;

            PlayerRegistry.Register(new PlayerData
            {
                EntityId = EntityId,
                Rigidbody = rb,
                FocalPoint = focalPoint
            });
        }

        private void OnDestroy()
        {
            PlayerRegistry.Unregister(EntityId);
        }
    }
}
