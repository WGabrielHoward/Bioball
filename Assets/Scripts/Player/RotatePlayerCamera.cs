using UnityEngine;

namespace Scripts.Player
{
    public class RotatePlayerCamera : MonoBehaviour
    {

        private float horizontalInput;
        [SerializeField] private float rotationSpeed=200;
        private GameObject focalPoint;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (focalPoint == null)
            {
                focalPoint = GameObject.Find("Player");
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = focalPoint.transform.position;

            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
}