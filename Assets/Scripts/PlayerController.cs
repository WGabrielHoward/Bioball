using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private float verticalInput;

    [Header("Player View")]
    [SerializeField] private GameObject focalPoint;
    [SerializeField] private TMPro.TextMeshProUGUI healthObj;

    [Header("Player Stats")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private int health = 100;

    [Header("Particle Effects")]
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject frost;
    [SerializeField] private GameObject healGlow;
    [SerializeField] private GameObject poisonDrops;

    private bool healing;
    private bool burning;
    private bool freezing;
    private bool poisoned;
    
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
       
        //healthObj.text = "Health: " + health;
        //powerupIndicator.transform.position = transform.position + pIoffset;
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    spawnManager.GetComponent<SpawnManagerScript>().TriggerWaves();
        //}

    }
    
    void SetEffects()
    {
        healGlow.SetActive(healing);
        smoke.SetActive(burning);
        frost.SetActive(freezing);
        poisonDrops.SetActive(poisoned);

    }

    void EffectsSwitch(GameObject hitObj, bool setting)
    {
        Effect tmpEffect = hitObj.GetComponent<NonPlayerCharacter>().GetEffect();
        switch (tmpEffect)
        {
            case Effect.heal:
                healing = setting;
                break;
            case Effect.poison:
                poisoned = setting;
                break;
            case Effect.burn:
                burning = setting;
                break;
            case Effect.freeze:
                freezing = setting;
                break;
        }
    }

    private void Hit(GameObject other)
    {
        int damage = other.GetComponent<NonPlayerCharacter>().GetDamage();
        StartCoroutine(TakeDamage(damage));
    }

    IEnumerator TakeDamage(int damage)
    {
        yield return new WaitForSeconds(.5f);
        health -= damage;
        healthObj.text = $"Health: {health}";
        if (health <= 0)
        {
            MainManager.ManInstance.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") )
        {
            EffectsSwitch(collision.gameObject, true);
            //Hit(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(collision.gameObject, true);
            Hit(collision.gameObject);
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
            //Hit(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EffectsSwitch(other.gameObject, true);
            Hit(other.gameObject);
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
