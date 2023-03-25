using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Com.Bruno.NextBots
{
    public class Move : NetworkBehaviour
    {
        // Speed and basic fisics

        public float speed;
        public float sprintModifier;
        public float jumpForce;
        Camera mainCam;
        public Transform groundDetector;
        public LayerMask ground;

        private Rigidbody rig;

        // Sprint

        private float baseFOV;
        private float sprintFOVModifier = 1.5f;
        private bool is_sprintandjumping;
        private bool isSprinting;
        private bool sprint;

        // Jump

        private bool jump;
        private bool isJumping;
        private bool isGrounded;

        // C crouch
        private bool crouch;
        private bool isCrouching;

        // Axies

        private float t_hmove;
        private float t_vmove;


        private void Start()
        {

            mainCam = Camera.main;
            baseFOV = mainCam.fieldOfView;
            rig = GetComponent<Rigidbody>();
            rig.isKinematic = false;

            is_sprintandjumping = false;

        }

        private void Update()
        {
            if (isLocalPlayer == false)
                return;

            // Axies
            t_hmove = Input.GetAxisRaw("Horizontal");
            t_vmove = Input.GetAxisRaw("Vertical");

            // Controls
            sprint = Input.GetKey(KeyCode.LeftShift);
            jump = Input.GetKeyDown(KeyCode.Space);
            crouch = Input.GetKey(KeyCode.C);

            // States
            isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
            isJumping = jump && isGrounded && !crouch;
            isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;


            // Jumping


            if (isJumping && !sprint)
            {
                rig.AddForce(Vector3.up * jumpForce);
            }
            else if (isJumping && sprint)
            {
                rig.AddForce(Vector3.up * jumpForce);
                is_sprintandjumping = true;
            }
            if (!sprint && isGrounded || t_vmove <= 0) is_sprintandjumping = false;

            // crouch
            isCrouching = crouch && !isJumping && isGrounded;


        }

        private void FixedUpdate()
        {
            

            // Movement
            Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
            t_direction.Normalize();

            float t_adjustedSpeed = speed;
            if (isSprinting) t_adjustedSpeed *= sprintModifier;
            else if (is_sprintandjumping) t_adjustedSpeed *= sprintModifier;

            Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.deltaTime;
            t_targetVelocity.y = rig.velocity.y;
            rig.velocity = t_targetVelocity;


            // Field of View
            if (isSprinting)
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
            }
            else if (is_sprintandjumping)
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
            }
            else { mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }

            // camera crouch
            if (isCrouching) mainCam.transform.localPosition = new Vector3(0f, 0f, 0.2f);
            else mainCam.transform.localPosition = new Vector3(0f, 0.5f, 0.2f);
        }






        //void FixedUpdate() // Fixed update to keep fixed and a player not be faster than the other cuz their computer runs different
        //{
        //    // Axies
        //    float t_hmove = Input.GetAxisRaw("Horizontal");
        //    float t_vmove = Input.GetAxisRaw("Vertical");

        //    // Controls
        //    bool sprint = Input.GetKey(KeyCode.LeftShift);
        //    bool jump = Input.GetKeyDown(KeyCode.Space);

        //    // States
        //    bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        //    bool isJumping = jump && isGrounded;
        //    bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;

        //    // Jumping
        //    if(isJumping)
        //    {
        //        rig.AddForce(Vector3.up * jumpForce);
        //    }

        //    // Movement
        //    Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
        //    t_direction.Normalize();

        //    float t_adjustedSpeed = speed;
        //    if (isSprinting) t_adjustedSpeed *= sprintModifier;

        //    Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.deltaTime;
        //    t_targetVelocity.y = rig.velocity.y;
        //    rig.velocity = t_targetVelocity;


        //    // Field of View
        //    if (isSprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f); }
        //    else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }
        //}
    }
}

