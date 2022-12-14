using UnityEngine;

public class FPS_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private ulong enemyMax;
    [SerializeField] private ulong increaseMaxPerWave;
    private ulong enemyCount = 0;
    private GameObject[] spawnLocations;


    // Find all spawn points present in the world
    private void Awake() => spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");

    // Spawn
    void Update() => Spawn();

    // Spawn method
    private void Spawn()
    {
        // Calculate the number of enemies
        enemyCount = (ulong)GameObject.FindGameObjectsWithTag("EnemyUwU").Length;

        // If the wave if not over, return
        if (enemyCount != 0) return;

        // Increase max enemies every wave
        enemyMax += increaseMaxPerWave;

        while (enemyCount < enemyMax) {
            // Randomly choose a spawn location
            int loc = Random.Range(0, spawnLocations.Length);

            // Spawn a new enemy
            Instantiate(enemy, spawnLocations[loc].transform.position, spawnLocations[loc].transform.rotation);

            // Increase count
            enemyCount++;
            
            print($"Created {spawnLocations[loc].transform.position} at {spawnLocations[loc].transform.rotation}!");
            print(spawnLocations.Length);
        }
    }
}
