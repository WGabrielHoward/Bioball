using System.Collections;
using UnityEditor;
using UnityEngine;

using Scripts.NPC;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    //public GameObject powerupPrefab;

    private Vector3 randPos;
    private float spawnRange = 40f;
    public float startDelay = 2f;
    public float waveDelay = 50f;
    public int waveCount = 5;
    public int increaseSize = 5;
    private int waveNumber = 1;
    private int enemyCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void TriggerWaves()
    {
        StartCoroutine(RepeatedSpawnWaves(startDelay, waveDelay, waveNumber, waveCount, increaseSize));
    }

    IEnumerator RepeatedSpawnWaves(float delayStart, float rate, int waveStartSize, int waveCount, int sizeIncrease)
    {
        yield return new WaitForSeconds(delayStart);
        
        for(int i = 0; i < waveCount; i++)
        {
            SpawnEnemyWave(waveStartSize*sizeIncrease);
            yield return new WaitForSeconds(rate);
        }
        
    }

    private void SpawnEnemyWave(int waveSize)
    {
        for(int i = 0; i < waveSize; i++)
        {
            randPos = GenerateRandomPosition();
            Instantiate(enemyPrefab, randPos, enemyPrefab.transform.rotation);
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        randPos = GenerateRandomPosition();
        Instantiate(enemyPrefab, randPos, enemyPrefab.transform.rotation);
    }
    
    private Vector3 GenerateRandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randPos = new Vector3(spawnPosX, 3, spawnPosZ);
        return randPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            TriggerWaves();
        }
    }
}
