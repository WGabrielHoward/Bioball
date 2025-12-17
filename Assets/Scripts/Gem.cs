using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    // onDestroy useful to use as listener if Spawner placed
    //public UnityEvent<int> onDestroyed;

    [Header("Gem Value: 1, 2, 5, or 10")]
    [SerializeField] private int pointValue; // 1.Red, 2.Green, 5.Silver, 10.Gold


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //onDestroyed.Invoke(pointValue);
            LevelManager.ManInstance.AddPoints(pointValue);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //onDestroyed.Invoke(pointValue);
            LevelManager.ManInstance.AddPoints(pointValue);
            Destroy(gameObject);
        }

    }



}
