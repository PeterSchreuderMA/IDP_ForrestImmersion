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

        private float playerVelMax = 2f;

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
            //print(playerMovement.gameObject.GetComponent<Rigidbody>().velocity.z);
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
            print("Starting transition");

            float _velZ;

            float _mSpeed = playerMovement.moveSpeed;// Save the current movement speed
            float _transSpeed = 3f * Time.deltaTime;// Transition time of the movement speed

            int _sceneAmount = scenes.Length;


            // Slow down the player
            iTween.ValueTo(gameObject, iTween.Hash("from", _mSpeed, "to", _mSpeed / 2f, "time", _transSpeed, "onupdate", "TweenProgress"));

            // - Setup -
            playerMovement.isEnabled = false;

            //playerMovement.moveSpeed = Mathf.Lerp(_mSpeed, _mSpeed / 2f, _transSpeed);

            // Slow the player down by velocity to or else the player will be moving at the same speed
            _velZ = playerMovement.gameObject.GetComponent<Rigidbody>().velocity.x;
            playerObject.GetComponent<Rigidbody>().velocity = new Vector3(playerVelMax / 2, 0, 0);


            // Dissolve the current scene
            scenes[sceneCurrent].GetComponent<DissolveAnimation>().DissolveOut();

            int _prevSceneInt = sceneCurrent; 

            sceneCurrent++;

            print("Scene lenth:" + scenes.Length);

            //int _prevSceneInt = (sceneCurrent - 1) % scenes.Length;
            int _endSceneInt = (sceneCurrent + (scenes.Length + 1)) % scenes.Length;


            sceneCurrent %= scenes.Length;

            // Loop the sceneCurrent variable
            //sceneCurrent = sceneCurrent % scenes.Length;

            // Dissolve back the current scene
            scenes[sceneCurrent].GetComponent<DissolveAnimation>().DissolveIn();

            // Wait on the transition
            yield return new WaitForSeconds(2f);

            // Set the player's movement speed back to normal
            iTween.ValueTo(gameObject, iTween.Hash("from", playerMovement.moveSpeed, "to", _mSpeed, "time", _transSpeed, "onupdate", "TweenProgress"));

            playerMovement.isEnabled = true;
            
            print("prev: " + _prevSceneInt);
            print("end: " + _endSceneInt);

            yield return new WaitForSeconds(2f);

            scenes[_prevSceneInt].transform.position = scenes[_endSceneInt].GetComponent<SceneObject>().sceneEndPosition.position;
            //playerMovement.moveSpeed = Mathf.Lerp(playerMovement.moveSpeed, _mSpeed, _transSpeed);

            yield return false;
        }
    }

}
