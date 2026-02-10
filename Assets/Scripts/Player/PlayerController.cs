
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rbPlayer;
        private float forwardSpeed;
        private GameObject focalPoint;

        void Start()
        {
            rbPlayer = gameObject.GetComponent<Rigidbody>();
            forwardSpeed = 5f;
            focalPoint = GameObject.Find("FocalPoint");
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