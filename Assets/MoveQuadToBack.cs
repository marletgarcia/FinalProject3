using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveQuadToBack : MonoBehaviour
{
    public GameObject quad; // Reference to the quad object
    public float zPosition = -20f; // Desired z position for the quad

    void Start()
    {
        if (quad != null)
        {
            Vector3 newPosition = quad.transform.position;
            newPosition.z = zPosition;
            quad.transform.position = newPosition;
        }
        else
        {
            Debug.LogError("Quad object is not assigned.");
        }
    }
}
