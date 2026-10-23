using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public event Action OnVictory;

    [Header("Spawn Settings")]
    [SerializeField] private SOEnemyConfig[] enemyConfigs;
    [SerializeField] private int maxWaves = 15;
    [SerializeField] private float spawnInterval = 3f;
    private int currentWave = 0;
    private int activeEnemiesCount = 0;

    [Header("Lanes Settings")]
    [SerializeField] private int leftLanes = 6;
    [SerializeField] private int rightLanes = 6;
    [SerializeField] private float laneSpacing = 0.9f;

    [Header("Enemy Count")]
    [SerializeField] private int minEnemiesPerWave = 1;
    [SerializeField] private int maxEnemiesPerWave = 4;

    private List<float> availableLanes = new List<float>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GenerateLanes();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private void GenerateLanes()
    {
        availableLanes.Clear();

        availableLanes.Add(0f);
        for (int i = 1; i <= leftLanes; i++)
        {
            availableLanes.Add(-i * laneSpacing);
        }
        for (int i = 1; i <= rightLanes; i++)
        {
            availableLanes.Add(i * laneSpacing);
        }
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentWave >= maxWaves) yield break;

            SpawnWave();
            currentWave++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void SpawnWave()
    {
        if (enemyConfigs == null || enemyConfigs.Length == 0) return;

        int spawnCount = UnityEngine.Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);
        spawnCount = Mathf.Min(spawnCount, availableLanes.Count);

        List<float> shuffledLanes = new List<float>(availableLanes);
        for (int i = 0; i < shuffledLanes.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(i, shuffledLanes.Count);
            float temp = shuffledLanes[rnd];
            shuffledLanes[rnd] = shuffledLanes[i];
            shuffledLanes[i] = temp;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            SOEnemyConfig config = enemyConfigs[UnityEngine.Random.Range(0, enemyConfigs.Length)];
            if (config.prefab == null) continue;

            float randomX = shuffledLanes[i];
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

            PoolManager.Instance.Spawn(config.prefab, spawnPosition, Quaternion.identity);
            activeEnemiesCount++;
        }
    }
    public void OnEnemyDeath()
    {
        activeEnemiesCount--;
        if (activeEnemiesCount <= 0 && currentWave >= maxWaves)
        {
            OnVictory?.Invoke();
        }
    }
}
