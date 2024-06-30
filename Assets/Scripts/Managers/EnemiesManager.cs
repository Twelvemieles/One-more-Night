using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private int MaxEnemiesCount;
    [SerializeField] private float timerToCreateEnemy;
    private int _enemiesCount;
    
    public void OnGameStart()
    {
        CreateEnemy();
    }
    private IEnumerator DoCreateEnemies()
    {
        yield return new WaitForSeconds(timerToCreateEnemy);

        CreateEnemy();
    }

    private void CreateEnemy()
    {
        if(_enemiesCount < MaxEnemiesCount)
        {
            Instantiate(enemyPrefab, GetRandomSpawnPoint());
            _enemiesCount++;
        }

        StartCoroutine(DoCreateEnemies());
    }
    private Transform GetRandomSpawnPoint()
    {
        Transform transform = null;
        transform = spawnPoints[Random.Range(0, spawnPoints.Count)];
        return transform;
    }
    public void OnEnemyDies()
    {
        _enemiesCount--;
    }
}
