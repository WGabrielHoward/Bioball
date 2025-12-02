using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private Vector3 randPos;
    private float spawnRange = 9f;
    public float startDelay = 2f;
    public float waveDelay = 50f;
    public int waveCount = 5;
    public int increaseSize = 5;
    public int waveNumber = 1;
    public int enemyCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //InvokeRepeating("SpawnEnemy", 1f, 3f);
        //SpawnEnemyWave(3);
        //StartCoroutine(RepeatedSpawnWaves(startDelay, waveDelay, waveStartSize, waveCount, increaseSize));
    }

    public void TriggerWaves()
    {
        StartCoroutine(RepeatedSpawnWaves(startDelay, waveDelay, waveNumber, waveCount, increaseSize));
    }

    IEnumerator RepeatedSpawnWaves(float delayStart, float rate, int waveStartSize, int waveCount, int sizeIncrease)
    {
        yield return new WaitForSeconds(delayStart);
        //while (true)
        //{
        //    // one option for continuous
        //}
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
        Vector3 randPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<EnemyScript>(FindObjectsSortMode.None).Length;
        if (enemyCount == 0) 
        {
            Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
            SpawnEnemyWave(waveNumber*increaseSize); 
        }
    }
}
