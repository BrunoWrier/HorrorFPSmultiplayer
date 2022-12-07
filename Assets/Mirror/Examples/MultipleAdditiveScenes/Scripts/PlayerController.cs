using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mirror.Examples.MultipleAdditiveScenes
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : NetworkBehaviour
    {
        public CharacterController characterController;

        void OnValidate()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();

            characterController.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<NetworkTransform>().clientAuthority = true;
        }

        public override void OnStartLocalPlayer()
        {
            characterController.enabled = true;
        }

        [Header("Movement Settings")]
        public float moveSpeed = 8f;
        public float turnSensitivity = 5f;
        public float maxTurnSpeed = 100f;

        [Header("Diagnostics")]
        public float horizontal;
        public float vertical;
        public float turn;
        public float jumpSpeed;
        public bool isGrounded = true;
        public bool isFalling;
        public Vector3 velocity;

        // Sprint

        private float baseFOV;
        private float sprintFOVModifier = 1.5f;
        private bool is_sprintandjumping;
        private bool isSprinting;
        private bool sprint;
        public float sprintModifier;

        // Jump

        private bool jump;

        // Control sneak
        private bool sneak;
        private bool isSneaking;

        // C crouch
        private bool crouch;
        private bool isCrouching;

        // basic

        Camera mainCam;
        public Transform groundDetector;
        public LayerMask ground;
        private Rigidbody rig;

        // shake camera
        public Animator anim;

        private void Start()
        {

            mainCam = Camera.main;
            baseFOV = mainCam.fieldOfView;
            anim = mainCam.GetComponent<Animator>();
        }

        void Update()
        {
            if (!isLocalPlayer || characterController == null || !characterController.enabled)
                return;

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            // Q and E cancel each other out, reducing the turn to zero
            if (Input.GetKey(KeyCode.Q))
                turn = Mathf.MoveTowards(turn, -maxTurnSpeed, turnSensitivity);
            if (Input.GetKey(KeyCode.E))
                turn = Mathf.MoveTowards(turn, maxTurnSpeed, turnSensitivity);
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
                turn = Mathf.MoveTowards(turn, 0, turnSensitivity);
            if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
                turn = Mathf.MoveTowards(turn, 0, turnSensitivity);

            if (isGrounded)
                isFalling = false;

            if ((isGrounded || !isFalling) && jumpSpeed < 1f && Input.GetKey(KeyCode.Space))
            {
                jumpSpeed = Mathf.Lerp(jumpSpeed, 1f, 0.5f);
            }
            else if (!isGrounded)
            {
                isFalling = true;
                jumpSpeed = 0;
            }

            // States
            sprint = Input.GetKey(KeyCode.LeftShift);
            isSprinting = sprint && vertical > 0 && !isFalling && isGrounded;
            sneak = Input.GetKey(KeyCode.LeftControl);
            isSneaking = sneak && !sprint && !isFalling && isGrounded;

            // crouch
            crouch = Input.GetKey(KeyCode.C);
            isCrouching = crouch && !isFalling && isGrounded;

        }

        void FixedUpdate()
        {
            if (!isLocalPlayer || characterController == null || !characterController.enabled)
                return;

            if (isSprinting && moveSpeed < 16f) moveSpeed += sprintModifier;
            else if (moveSpeed > 8f && !sprint) moveSpeed -= sprintModifier;

            if (isSneaking || isCrouching) moveSpeed = 4f;
            else if (!isSprinting && isGrounded) moveSpeed = 8f;

            transform.Rotate(0f, turn * Time.fixedDeltaTime, 0f);

            Vector3 direction = new Vector3(horizontal, jumpSpeed, vertical);
            direction = Vector3.ClampMagnitude(direction, 1f);
            direction = transform.TransformDirection(direction);
            direction *= moveSpeed;

            if (jumpSpeed > 0)
                characterController.Move(direction * Time.fixedDeltaTime);
            else
                characterController.SimpleMove(direction);

            isGrounded = characterController.isGrounded;
            velocity = characterController.velocity;

            // Field of View
            if (isSprinting || moveSpeed > 8f)
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
                

                
            }
            else { 
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
                
            }

            // camera crouch
            if (isCrouching) mainCam.transform.localPosition = new Vector3(0f, 0f, 0.2f);
            else mainCam.transform.localPosition = new Vector3(0f, 0.5f, 0.2f);
        }
    }
}
