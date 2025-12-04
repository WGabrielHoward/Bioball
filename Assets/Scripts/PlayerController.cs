using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private float verticalInput;
    public float forwardSpeed = 5f;
    [SerializeField] private GameObject focalPoint;

    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject frost;
    [SerializeField] private GameObject healGlow;
    [SerializeField] private GameObject poisonDrops;

    private bool burning;
    private bool freezing;
    private bool poisoned;
    private bool healing;

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
        SetEffects();
        //powerupIndicator.transform.position = transform.position + pIoffset;
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    spawnManager.GetComponent<SpawnManagerScript>().TriggerWaves();
        //}

    }
    
    void SetEffects()
    {
        smoke.SetActive(burning);
        frost.SetActive(freezing);
        healGlow.SetActive(healing);
        poisonDrops.SetActive(poisoned);

    }

    void EffectsSwitch(GameObject hitObj, bool setting)
    {
        Effect tmpEffect = hitObj.GetComponent<NonPlayerCharacter>().GetEffect();
        switch (tmpEffect)
        {
            case Effect.poison:
                poisoned = setting;
                break;
            case Effect.heal:
                healing = setting;
                break;
            case Effect.burn:
                burning = setting;
                break;
            case Effect.freeze:
                freezing = setting;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") )
        {
            EffectsSwitch(collision.gameObject, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(collision.gameObject, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(collision.gameObject, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(other.gameObject, true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(other.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(other.gameObject, false);
        }
    }

}
