
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rbPlayer;
        private float verticalInput;
        private float forwardSpeed;

        [Header("Player View")]
        private GameObject focalPoint;
        private PlayerScriptManager playSMan;

        private void Awake()
        {
            playSMan = gameObject.GetComponent<PlayerScriptManager>();
           
            
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            PullForwardSpeed();
            PullFocalPoint();
            PullRigidbody();
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            verticalInput = Input.GetAxis("Vertical");
            rbPlayer.AddForce(focalPoint.transform.forward * verticalInput * forwardSpeed);

        }
        public void SetForwardSpeed(float newSpeed)
        {
            forwardSpeed = newSpeed;
        }

        private void PullRigidbody()
        {
            rbPlayer = playSMan.GetRigidbody();
        }

        private void PullForwardSpeed()
        {
            forwardSpeed = playSMan.GetForwardSpeed();
        }

        private void PullFocalPoint()
        {
            focalPoint = playSMan.GetFocalPoint();
        }

    }
}