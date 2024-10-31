using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;       // Assign your platform prefab here in the Inspector
    [SerializeField] private Transform player;                // Assign your player (astronaut) object here in the Inspector
    [SerializeField] private int initialPlatformCount = 5;    // Number of platforms to spawn initially
    [SerializeField] private float verticalSpacing = 3f;      // Distance between each platform vertically
    [SerializeField] private float horizontalSpacing = 5f;    // Distance between each platform horizontally
    [SerializeField] private float spawnDistance = 10f;       // Distance to start spawning additional platforms
    [SerializeField] private float minX = -3f;                // Minimum X position for the platforms
    [SerializeField] private float maxX = 3f;                 // Maximum X position for the platforms
    [SerializeField] private float platformHeight = 1f;       // Height of the platforms from the ground
    [SerializeField] private float startPlatformX = -7f;     // X position of the first platform (left side)
    [SerializeField] private Vector3 initialPlatformPosition = new Vector3(-7f, 0f, 0f); // Hard-coded position for the first platform

    private Vector3 lastSpawnPosition;

    void Start()
    {
        // Calculate camera height
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize * 2; // Height of the camera view in world units
        Instantiate(platformPrefab, initialPlatformPosition, Quaternion.identity);

        // Set the initial spawn position for the first platform to the left
        lastSpawnPosition = new Vector3(startPlatformX, -cameraHeight / 2 + platformHeight / 2, 0);

        // Spawn the initial platforms
        SpawnInitialPlatforms();

        // Position the astronaut on the first platform
        if (player != null)
        {
            // Ensure the player is positioned on top of the first platform
            player.position = new Vector3(initialPlatformPosition.x, initialPlatformPosition.y + (platformPrefab.transform.localScale.y / 2), player.position.z);
        }
        else
        {
            Debug.LogError("Player transform is not assigned in the PlatformSpawner!");
        }
    }

    void Update()
    {
        // Spawn new platforms as the player moves up
        if (Vector3.Distance(player.position, lastSpawnPosition) < spawnDistance) return;

        SpawnPlatform();
    }

    void SpawnInitialPlatforms()
    {
        // Create an initial horizontal line of platforms starting from the left
        for (int i = 0; i < initialPlatformCount; i++)
        {
            // Randomize the X position for variation within bounds
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPosition = new Vector3(randomX, lastSpawnPosition.y, 0);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Update last spawn position for the next platform
            lastSpawnPosition = new Vector3(spawnPosition.x + horizontalSpacing, spawnPosition.y + verticalSpacing, 0);
        }
    }

    void SpawnPlatform()
    {
        // Randomize the X position for the new platform
        float randomX = Random.Range(minX, maxX);
        lastSpawnPosition = new Vector3(randomX, lastSpawnPosition.y + verticalSpacing, 0);
        Instantiate(platformPrefab, lastSpawnPosition, Quaternion.identity);
    }
}
