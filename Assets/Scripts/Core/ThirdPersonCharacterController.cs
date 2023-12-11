using UnityEngine;

namespace Core
{
    public class ThirdPersonCharacterController : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private GridManager gridManager;
        
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
        
        public void SetupCharacterController()
        {
            _controller = GetComponent<CharacterController>();
            
            // Spawn the Player at the center of the Grid
            transform.position = gridManager.Width / 2f * Vector3.right + gridManager.Height / 2f * Vector3.forward;
        }
        
        private void Update()
        {
            HandleMovement();
        }
        
        private void LateUpdate()
        {
            RotateWithMouse();
        }
        
        private void HandleMovement()
        {
            var inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            
            var movementVector = Vector3.zero;
            if (inputs.magnitude >= 0.1f)
            {
                var transformReference = transform;
                var moveDirection = transformReference.forward * inputs.z + transformReference.right * inputs.x;
                movementVector = moveDirection * (speed * Time.deltaTime);
            }
            
            var jumpVector = CalculateGravityAndJump();
            var finalVector = movementVector + jumpVector;
            
            _controller.Move(finalVector);
        }

        private Vector3 CalculateGravityAndJump()
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
            return Vector3.up * (_velocityY * Time.deltaTime);
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