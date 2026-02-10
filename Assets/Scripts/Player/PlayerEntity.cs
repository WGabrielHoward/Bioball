using UnityEngine;
using Scripts.Systems;

namespace Scripts.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        public int EntityId { get; private set; }

        [Header("Unity Bridges")]
        private Rigidbody rb;
        private Transform focalPoint;

        private void Awake()
        {
            EntityId = EntityIdGenerator.Next();

            rb = gameObject.GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("FocalPoint").transform;
            PlayerData playerData = new PlayerData{
                             EntityId = EntityId,
                             Rigidbody = rb,
                             FocalPoint = focalPoint
                         };
            MovementSystem.Instance.RegisterPlayer(playerData);
            PlayerRegistry.Register(playerData);
        }

        private void OnDestroy()
        {
            MovementSystem.Instance.Unregister(EntityId);
            PlayerRegistry.Unregister(EntityId);
        }
    }
}
