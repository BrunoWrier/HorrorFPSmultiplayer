using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// This sets up the scene camera for the local player

namespace Mirror.Examples.MultipleAdditiveScenes
{
    public class PlayerCamera : NetworkBehaviour
    {
        public static bool cursorLocked = true;
        public Transform player;

        public float xSensitivity;
        public float ySensitivity;
        public float maxAngle;

        public Quaternion camCenter;

        Camera mainCam;


        void Awake()
        {
            mainCam = Camera.main;
        }

        public override void OnStartLocalPlayer()
        {
            if (mainCam != null)
            {
                // configure and make camera a child of player with 1st person offset
                mainCam.orthographic = false;
                mainCam.transform.SetParent(transform);
                //mainCam.transform.position = transform.position + new Vector3(0, 0.5f, 0.2f);
                mainCam.transform.localPosition = new Vector3(0f, 0.5f, 0.2f);
                mainCam.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
            }
        }

        public override void OnStopLocalPlayer()
        {
            if (mainCam != null)
            {
                mainCam.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(mainCam.gameObject, SceneManager.GetActiveScene());
                mainCam.orthographic = true;
                mainCam.transform.localPosition = new Vector3(0f, 70f, 0f);
                mainCam.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            }
        }
    }

}
