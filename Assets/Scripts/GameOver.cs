using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;

    void Update()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(player.position);
        bool isOutOfView = screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;

        if (isOutOfView)
        {
            Debug.Log("Game Over");
            // Implement game over logic here (e.g., reload the scene, show a game over screen, etc.)
        }
    }
}
