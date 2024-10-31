using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int platformsPerLevel = 10;
    private int platformsSpawned = 0;

    // Singleton instance
    public static LevelManager instance;

    private void Awake()
    {
        // Ensure there is only one instance of LevelManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This makes the object persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanSpawnPlatform()
    {
        if (platformsSpawned < platformsPerLevel)
        {
            platformsSpawned++;
            Debug.Log("Platforms Created: " + platformsSpawned); // Debug message to show current count

            return true;
        }
        else
        {
        Debug.Log("Max platforms reached. Proceeding to next level.");
            NextLevel(); // Call NextLevel when the maximum number of platforms is reached
            return false;
        }
        
    }

    public void NextLevel()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player.currentEnergy >= 50)  // Adjust the required energy to pass the level
        {
            currentLevel++;
            platformsPerLevel += 10;  // Increase the number of platforms for the new level
            platformsSpawned = 0;  // Reset the platform count for the new level
            player.currentEnergy -= 50;  // Deduct the required energy to pass the level
            player.UpdateEnergyUI();
            Debug.Log("Level advanced to: " + currentLevel);
             // Load the next level scene
            LoadNextLevel();
 

        }
        else
        {
            // Optionally, display a message indicating the player does not have enough energy
            Debug.Log("Not enough energy to pass the level!");
        }
    }
    private void LoadNextLevel()
    {
        // Load the next scene based on currentLevel
        // Assuming your level scenes are named "Level1", "Level2", etc.
        string nextLevelSceneName = "Level" + currentLevel; // Ensure your scenes are named properly
        SceneManager.LoadScene(nextLevelSceneName);
    }
}
