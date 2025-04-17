using UnityEngine;
using Utility;

namespace User
{
    public class UserFreeMovement : MonoBehaviour
    {
        public float baseSpeed = 10f;
        public float maxSpeed = 50f;
        public float scrollSensitivity = 10f;
        public float lookSpeed = 2f;

        private float currentSpeed;

        void Start()
        {
            currentSpeed = baseSpeed;
            Cursor.lockState = CursorLockMode.None;
        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Settings.Instance.isMoving = true;
                
                Cursor.lockState = CursorLockMode.Confined;

                float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
                float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

                transform.eulerAngles += new Vector3(-mouseY, mouseX, 0);
                
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                float upDown = 0f;
                if (Input.GetKey(KeyCode.E)) upDown += 1f;
                if (Input.GetKey(KeyCode.Q)) upDown -= 1f;

                Vector3 move = new Vector3(horizontal, upDown, vertical);
                move = transform.TransformDirection(move);

                transform.position += move * currentSpeed * Time.deltaTime;

                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0)
                {
                    currentSpeed += scroll * scrollSensitivity;
                    currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);
                }
            }
            else
            {
                Settings.Instance.isMoving = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}