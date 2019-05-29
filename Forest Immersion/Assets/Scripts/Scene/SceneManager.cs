using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SceneTransition
{
    public class SceneManager : MonoBehaviour
    {
        public Action<Collider> SceneTransitionEvent;

        public GameObject[] scenes;

        int sceneCurrent = 0;

        PlayerMovement playerMovement;

        List<GameObject> colliders = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            //Get the player object with the movement script
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();


        }

        // Update is called once per frame
        void Update()
        {
            ChilderenCollidersCheck();
        }

        void GatherChilderenColliders()
        {
            foreach (GameObject _child in transform)
            {
                //Collider _col = _child.GetComponent<Collider>();

                colliders.Add(_child);
            }
        }

        void ChilderenCollidersCheck()
        {
            foreach (GameObject _childCol in colliders)
            {
                if (_childCol.GetComponent<SceneTransitionCollider>().collidedWPlayer)
                {
                    StartSceneTransition();
                }
            }
        }

        void SetupScenes()
        {
            for (int i = 1; i < scenes.Length; i++)
            {

            }
        }

        public void StartSceneTransition()
        {
            StartCoroutine(SceneTransition());
        }


        public IEnumerator SceneTransition()
        {
            float _mSpeed = playerMovement.moveSpeed;

            playerMovement.moveSpeed = Mathf.Lerp(_mSpeed, _mSpeed / 2f, 3f * Time.deltaTime);

            yield return new WaitForSeconds(2f);

            scenes[sceneCurrent].GetComponent<DissolveAnimation>().DissolveOut();


        }
    }

}
