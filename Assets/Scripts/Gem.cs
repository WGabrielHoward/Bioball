
using Scripts.Systems;
using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{

    [Header("Gem Value: 1, 2, 5, or 10")]
    [SerializeField] private int pointValue;

    private void Collect()
    {
        UnityEngine.Debug.Log($"Collect {pointValue}");
        ScoreSystem.Instance.AddScore(pointValue);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Collect();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Collect();
    }


}
