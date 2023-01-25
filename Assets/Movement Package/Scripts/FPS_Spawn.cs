using UnityEngine;

public class FPS_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private ulong enemyMax;
    [SerializeField] private ulong increaseMaxPerWave;

    [SerializeField] private bool isWave;

    [SerializeField] [WSWhitehouse.TagSelector.TagSelectorAttribute] string spawnedObjectTag = "Untagged";
    [SerializeField] [WSWhitehouse.TagSelector.TagSelectorAttribute] string SpawnPointTag = "Untagged";
    private ulong spawnCount = 0;
    private bool spawnedOnce = false;
    private GameObject[] spawnLocations;


    // Find all spawn points present in the world
    private void Awake() => spawnLocations = GameObject.FindGameObjectsWithTag(SpawnPointTag);

    // Spawn
    void Update() => Spawn();

    // Spawn method
    private void Spawn()
    {
        
        // Calculate the number of enemies
        spawnCount = (ulong)GameObject.FindGameObjectsWithTag(spawnedObjectTag).Length;

        // If the wave if not over, return
        if (spawnCount != 0) return;

        // Increase max enemies every wave
        enemyMax += increaseMaxPerWave;

        while (spawnCount < enemyMax && (!spawnedOnce)) {
            // Randomly choose a spawn location
            int loc = Random.Range(0, spawnLocations.Length);

            // Spawn a new enemy
            Instantiate(enemy, spawnLocations[loc].transform.position, spawnLocations[loc].transform.rotation);

            // Increase count
            spawnCount++;

            print($"Created {spawnLocations[loc].transform.position} at {spawnLocations[loc].transform.rotation}!");
            print(spawnLocations.Length);
        }

        // Set spawned once to true
        if (!isWave) spawnedOnce = true;
    }
}
