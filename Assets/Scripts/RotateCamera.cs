using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    private float horizontalInput;
    public float rotationSpeed;
    [SerializeField] private GameObject focalPoint;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = focalPoint.transform.position;

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput *rotationSpeed* Time.deltaTime);
    }
}
