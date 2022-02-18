using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace fpscamera
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSCamera : MonoBehaviour
    {
        public float turnSpeed = 2f;
        public float moveSpeed = 3f;
        public bool flipYAxis = true;
        public float angleLock = 45;

        public PlayerInputEvent OnLook;
        public PlayerInputEvent OnMove;
        private CharacterController _characteController;

        private float deltaY;
        private float deltaX;
        private Vector3 move;
        private float gravityValue = -9.81f;

        private Vector3 playerDownwardVelocity;
        private float _inputAxisX;
        private float _inputAxisY;


        // Start is called before the first frame update
        void Start()
        {
            OnLook.OnPlayerInput += OnLookEvent;
            OnMove.OnPlayerInput += OnMoveEvent;
            _characteController = GetComponent<CharacterController>();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void Update()
        {
            transform.RotateAround(transform.position, transform.TransformVector(Vector3.right), deltaY);
            transform.RotateAround(transform.position, Vector3.up, deltaX);

            var move = transform.TransformDirection(new Vector3(Mathf.Round(_inputAxisX), 0, Mathf.Round(_inputAxisY)));

            _characteController.Move(move * Time.deltaTime * moveSpeed);

            if (_characteController.isGrounded && playerDownwardVelocity.y < 0)
            {
                playerDownwardVelocity.y = 0f;
            }
            playerDownwardVelocity.y += gravityValue * Time.deltaTime;
            _characteController.Move(playerDownwardVelocity * Time.deltaTime);
        }

        private void OnMoveEvent(InputValue inputValue)
        {
            var value = inputValue.Get<Vector2>();
            
            _inputAxisX = value.x;
            _inputAxisY = value.y;
        }

        private void OnLookEvent(InputValue inputValue)
        {
            CalculateDeltaXForRotation(inputValue);
            CalculateDeltaYForRotation(inputValue);
            ClampVerticalAngle();
        }

        private void CalculateDeltaYForRotation(InputValue inputValue)
        {
            deltaY = inputValue.Get<Vector2>().y * turnSpeed * Time.deltaTime * (flipYAxis ? -1 : 1);
        }

        private void CalculateDeltaXForRotation(InputValue inputValue)
        {
            deltaX = inputValue.Get<Vector2>().x * turnSpeed * Time.deltaTime;
        }

        private void ClampVerticalAngle()
        {
            var rotation = transform.rotation.eulerAngles;

            if (IsLookingUp())
            {
                rotation.x = Mathf.Clamp(rotation.x, 360 - angleLock, 360 + angleLock);
            }
            else
            {
                rotation.x = Mathf.Clamp(rotation.x, -angleLock, angleLock);
            }
            rotation.z = 0;
            transform.rotation = Quaternion.Euler(rotation);
        }

        private bool IsLookingUp()
        {
            return transform.rotation.eulerAngles.x > 300;
        }
    }

}