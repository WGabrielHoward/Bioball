using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private float verticalInput;
    public float forwardSpeed = 5f;
    public GameObject focalPoint;
    //private bool hasPowerup = false;
    //public float powerImpact = 50f;
    //public GameObject powerupIndicator;
    //private Vector3 pIoffset;
    //public GameObject spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbPlayer = this.gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        //pIoffset = powerupIndicator.transform.position - transform.position;
        //spawnManager = GameObject.Find("SpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        rbPlayer.AddForce(focalPoint.transform.forward * verticalInput * forwardSpeed);
        //powerupIndicator.transform.position = transform.position + pIoffset;
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    spawnManager.GetComponent<SpawnManagerScript>().TriggerWaves();
        //}

    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Powerup"))
        //{
        //    hasPowerup = true;
        //    Destroy(other.gameObject);

        //    StartCoroutine(PowerupCountdownRoutine());
        //    powerupIndicator.gameObject.SetActive(true);
        //}
    }

    //IEnumerator PowerupCountdownRoutine()
    //{
    //    yield return new WaitForSeconds(8);
    //    hasPowerup = false;
    //    //powerupIndicator.SetActive(false);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
    //    {
    //        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
    //        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

    //        enemyRigidbody.AddForce(awayFromPlayer * powerImpact, ForceMode.Impulse);
    //    }
    //}
}
