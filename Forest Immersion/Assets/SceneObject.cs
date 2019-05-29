using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{

    public Vector3 startLocation;

    
    void Awake()
    {
        // Fill the start location with the current location
        startLocation = transform.position;
    }
}
