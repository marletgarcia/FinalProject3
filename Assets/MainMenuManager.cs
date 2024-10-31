using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SceneManager sceneManager;
    // Start is called before the first frame update
    public void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Level1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
