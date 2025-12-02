using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rbEnemy;
    private GameObject player;
    public float enemySpeed=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbEnemy = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rbEnemy.AddForce(lookDirection * enemySpeed);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
