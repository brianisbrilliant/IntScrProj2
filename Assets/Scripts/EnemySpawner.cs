using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform player;
    public int totalDesiredEnemies = 10;
    public float spawnInterval = 3;

    int enemiesCreated = 0;

    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while(enemiesCreated < totalDesiredEnemies) {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy() {
        Enemy enemy = Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
        enemy.transform.Translate(0, 1, 0);
        enemy.player = player;
        enemiesCreated += 1;
    }
    
}
