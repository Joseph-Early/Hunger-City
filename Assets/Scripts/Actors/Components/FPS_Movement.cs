using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Player
{
    public class FPS_Movement : MonoBehaviour
    {
        [SerializeField] private float walkSpeed; // Speed of movement
        [SerializeField] private float jumpHeight; // How far the player can jump
        [SerializeField] private float dashSpeedMultiplier;

        private byte jumpCount; // Number of times jumping
        [SerializeField] byte jumpCountMax = 2; // Max number of jumps allowed
        private const float gravity = -9.81f; // Gravity
        private CharacterController controller; // Character controller reference
        private Vector3 velocity; // Player velocity
        private Vector3 moveDirection; // Direction in which to move

        void Awake()
        {
            controller = GetComponent<CharacterController>(); // Cache a reference to the controller
        }

        void Update()
        {
            if (FPS_Pause.paused) return; // Do nothing if paused


            // Jump must be in the update method not fixed update due to key presses not always registering and failing to jump
            Jump();

            if (controller.isGrounded) jumpCount = 0; // Reset jump count to zero when grounded
        }
        void FixedUpdate()
        {
            if (FPS_Pause.paused) return; // Do nothing if paused
            
            // Used fixed update so that frame timings are consistent and speeds are not variable
            Walking();
        }

        void Walking()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal"); // Get H axis
            float moveVertical = Input.GetAxisRaw("Vertical"); // Get V axis
            moveDirection = (moveHorizontal * transform.right + moveVertical * transform.forward); // Calculate movement
            controller.Move(moveDirection * (Input.GetKey(KeyCode.LeftShift) ? walkSpeed * dashSpeedMultiplier : walkSpeed) * Time.deltaTime); // Move the controller

            velocity.y += gravity * Time.deltaTime; // Calculate gravity
            controller.Move(velocity * Time.deltaTime); // Apply gravity
        }
        void Jump()
        {
            // Jump
            if ((controller.isGrounded || jumpCount < jumpCountMax) && (Input.GetButtonDown("Jump")))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpCount++; // Increase jump count
            }
        }
    }

}