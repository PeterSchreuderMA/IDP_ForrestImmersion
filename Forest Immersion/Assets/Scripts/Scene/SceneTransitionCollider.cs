using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SceneTransition
{
    public class SceneTransitionCollider : MonoBehaviour
    {

        [SerializeField]
        LayerMask detectionLayer;

        public bool collidedWPlayer = false;

        SceneManager sceneManager;
        // Start is called before the first frame update
        void Start()
        {
            sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
            sceneManager.SceneTransitionEvent += OnTriggerEnter;
        }

        // Update is called once per frame
        void Update()
        {

        }


        void OnTriggerEnter(Collider _col)
        {
            if (_col.CompareTag("Player"))//_col.gameObject.layer == detectionLayer && 
            {
                collidedWPlayer = true;
            }
        }


        public void ColliderToggle(bool _value)
        {
            GetComponent<Collider>().enabled = _value;
        }
    }
}

