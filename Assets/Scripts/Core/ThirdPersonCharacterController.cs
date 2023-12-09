using UnityEngine;

namespace Core
{
    public class ThirdPersonCharacterController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 5f;
        private CharacterController _controller;
        
        [Header("Gravity")]
        [SerializeField] private float gravity = 9.8f;
        [SerializeField] private float gravityMultiplier = 2;
        [SerializeField] private float groundedGravity = -0.5f;
        [SerializeField] private float jumpHeight = 3f;
        private float _velocityY;
        
        [Header("Camera")] 
        [SerializeField] private Transform cam;
        [SerializeField] private float mouseSensitivity = 300f;
        private float _cameraVerticalAngle;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }
        
        private void Update()
        {
            HandleMovement();
            HandleGravityAndJump();
        }
        
        private void LateUpdate()
        {
            RotateWithMouse();
        }

        private void HandleMovement()
        {
            var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            
            if (movement.magnitude >= 0.1f)
            {
                var transformReference = transform;
                var moveDirection = transformReference.forward * movement.z + transformReference.right * movement.x;
                _controller.Move(moveDirection * (speed * Time.deltaTime));
            }
        }

        private void HandleGravityAndJump()
        {
            // Apply groundedGravity when the Player is Grounded
            if (_controller.isGrounded && _velocityY < 0f)
                _velocityY = groundedGravity;
            
            // When Grounded and Jump Button is Pressed, set velocityY with the formula below
            if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                _velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            
            // Applying gravity when Player is not grounded
            _velocityY -= gravity * gravityMultiplier * Time.deltaTime;
            _controller.Move(Vector3.up * (_velocityY * Time.deltaTime));
        }

        private void RotateWithMouse()
        {
            // Horizontal rotation with mouse
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
            
            // Vertical rotation with mouse
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            _cameraVerticalAngle -= mouseY;
            _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);
            
            // Adjust camera rotation
            cam.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0f, 0f);
        }
    }
}