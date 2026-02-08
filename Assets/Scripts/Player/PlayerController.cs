
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rbPlayer;
        private float forwardSpeed;
        private GameObject focalPoint;

        public void Initialize(Rigidbody rb, GameObject focalPoint, float speed)
        {
            this.rbPlayer = rb;
            this.focalPoint = focalPoint;
            this.forwardSpeed = speed;
        }


        // Update is called once per frame
        void FixedUpdate()
        {            
            float vertInput = Input.GetAxis("Vertical");
            rbPlayer.AddForce(focalPoint.transform.forward * vertInput * forwardSpeed);

        }
        public void SetForwardSpeed(float newSpeed)
        {
            forwardSpeed = newSpeed;
        }

    }
}