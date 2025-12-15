
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rbPlayer;
        private float verticalInput;
        private float forwardSpeed = 0;

        [Header("Player View")]
        private GameObject focalPoint;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rbPlayer = this.gameObject.GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("FocalPoint");
            SetForwardSpeed(this.gameObject.GetComponent<PlayerStats>().GetForwardSpeed());
        }

        // Update is called once per frame
        void Update()
        {

            verticalInput = Input.GetAxis("Vertical");
            rbPlayer.AddForce(focalPoint.transform.forward * verticalInput * forwardSpeed);

        }

        private void SetForwardSpeed(float newSpeed)
        {
            forwardSpeed = newSpeed;
        }


    }
}