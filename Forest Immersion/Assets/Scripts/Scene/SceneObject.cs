using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{

    public Vector3 startLocation;
    public Transform sceneEndPosition;

    void GetSceneEndPosition()
    {
        foreach (Transform _child in transform)
        {
            if (_child.gameObject.CompareTag("SceneEndPosition"))
                sceneEndPosition = _child;
        }
    }

    void Awake()
    {
        // Fill the start location with the current location
        startLocation = transform.position;
        GetSceneEndPosition();
    }
}
