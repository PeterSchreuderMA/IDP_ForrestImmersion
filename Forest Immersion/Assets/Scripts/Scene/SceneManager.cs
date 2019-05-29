using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Allemaal vieze en onopgemaakte code, zal veranderen maar voor nu werkt het
/// </summary>


// STC stands for "Scene Transition Collider"

namespace SceneTransition
{
    public class SceneManager : MonoBehaviour
    {
        public Action<Collider> SceneTransitionEvent;

        public GameObject[] scenes;

        [SerializeField]
        int sceneCurrent = 0;

        PlayerMovement playerMovement;
        GameObject playerObject;

        List<GameObject> STCObjects = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            //Get the player object with the movement script
            playerObject = GameObject.FindGameObjectWithTag("Player");

            playerMovement = playerObject.GetComponent<PlayerMovement>();
            

            STCGather();

            SetupScenes();
        }


        // Update is called once per frame
        void Update()
        {
            STCCheck();
        }


        /// ----------------
        /// --- Funtions ---
        /// ----------------


        void SetupScenes()
        {
            for (int i = 1; i < scenes.Length; i++)
            {
                scenes[i].GetComponent<DissolveAnimation>().DissolveInstantly(true);
            }
        }


        // --- STC (Scene Transition Collider) Related ---


        /// <summary>
        /// Gathers all the "Scene Transition Colliders" in the scene
        /// </summary>
        void STCGather()
        {
            STCObjects.AddRange(GameObject.FindGameObjectsWithTag("SceneTransitionCollider"));
        }


        /// <summary>
        /// Check if one of the STC's are triggered
        /// </summary>
        void STCCheck()
        {
            foreach (GameObject _STC in STCObjects)
            {
                if (_STC.GetComponent<SceneTransitionCollider>().collidedWPlayer)
                {
                    print("Player collided, starting transition");

                    _STC.GetComponent<SceneTransitionCollider>().collidedWPlayer = false;
                    StartSceneTransition();
                }
            }
        }


        // --- Scene Transition ---


        public void StartSceneTransition()
        {
            StartCoroutine(SceneTransition());
        }


        public IEnumerator SceneTransition()
        {
            float _velZ;

            float _mSpeed = playerMovement.moveSpeed;// Save the current movement speed
            float _transSpeed = 3f * Time.deltaTime;// Transition time of the movement speed

            int _sceneAmount = scenes.Length;


            // Slow down the player
            playerMovement.moveSpeed = Mathf.Lerp(_mSpeed, _mSpeed / 2f, _transSpeed);

            // Slow it down by velocity to or else player player will move the same speed
            _velZ = playerMovement.gameObject.GetComponent<Rigidbody>().velocity.z;
            playerObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, _velZ / 2);


            // Dissolve the current scene
            scenes[sceneCurrent].GetComponent<DissolveAnimation>().DissolveOut();

            sceneCurrent++;


            // Spawn the player to the start if it reaches the end (This will be changed to that the scenes will change position in the world)
            if (sceneCurrent == scenes.Length)
                playerObject.transform.position = new Vector3(0, 0, 0);


            // Loop the sceneCurrent variable
            sceneCurrent = sceneCurrent % scenes.Length;

            // Dissolve back the current scene
            scenes[sceneCurrent].GetComponent<DissolveAnimation>().DissolveIn();

            // Wait on the transition
            yield return new WaitForSeconds(2f);

            // Set the player's movement speed back to normal
            playerMovement.moveSpeed = Mathf.Lerp(playerMovement.moveSpeed, _mSpeed, _transSpeed);


        }
    }

}
