using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Com.Bruno.NextBots
{
    public class Look : NetworkBehaviour
    {
        public static bool cursorLocked = true;
        public Transform player;
        //public Transform cams;
        Camera mainCam;

        public float xSensitivity;
        public float ySensitivity;
        public float maxAngle;

        public Quaternion camCenter;

        // look

        void Start()
        {
            mainCam = Camera.main;
            camCenter = mainCam.transform.localRotation; // set rotation origin for cameras to camCenter
        }

        void Update()
        {
            
        }

        private void LateUpdate()
        {
            if (mainCam != null && isLocalPlayer != false)
            {
                SetY();
                SetX();

                UpdateCursorLock();
            }
        }

        void SetY()
        {
            float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
            Quaternion t_delta = mainCam.transform.localRotation * t_adj;

            if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
            {
                mainCam.transform.localRotation = t_delta;
            }
        }

        void SetX()
        {
            float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
            Quaternion t_delta = player.localRotation * t_adj;
            player.localRotation = t_delta;

        }

        void UpdateCursorLock()
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = true;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }
}