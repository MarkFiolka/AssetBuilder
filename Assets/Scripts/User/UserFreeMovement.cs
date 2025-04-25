using UnityEngine;
using Utility;

namespace User
{
    public class UserFreeMovement : MonoBehaviour
    {
        public float baseSpeed = 10f;
        public float maxSpeed = 50f;
        public float scrollSensitivity = 10f;
        public float lookSpeed = 4f;
        public float dragThreshold = 5f;

        private float currentSpeed;
        private Vector3 initialMousePosition;
        private bool waitingForDrag = false;

        void Start()
        {
            currentSpeed = baseSpeed;
            Cursor.lockState = CursorLockMode.None;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                UnityEngine.Debug.Break();
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                initialMousePosition = Input.mousePosition;
                Settings.Instance.isDragging = false;
                Settings.Instance.isMoving = false;
                waitingForDrag = true;

                if (Settings.Instance.placeBlocks)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        bool hitCube = hit.collider != null && hit.collider.CompareTag("cube");
                        bool hitGrid = hit.collider != null && hit.collider.name.StartsWith("GridLine");
                        Settings.Instance.clickedOnBlock = hitCube || hitGrid;
                    }
                    else
                    {
                        Settings.Instance.clickedOnBlock = false;
                    }
                }
                else
                {
                    Settings.Instance.clickedOnBlock = false;
                }
            }

            if (Input.GetMouseButton(1))
            {
                if (Settings.Instance.placeBlocks && Settings.Instance.clickedOnBlock)
                    return;

                if (waitingForDrag)
                {
                    if ((Input.mousePosition - initialMousePosition).magnitude > dragThreshold)
                    {
                        waitingForDrag = false;
                        Settings.Instance.isDragging = true;
                        Settings.Instance.isMoving = true;
                        Cursor.lockState = CursorLockMode.Confined;
                    }
                }

                if (Settings.Instance.isDragging)
                {
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
            }

            if (Input.GetMouseButtonUp(1))
            {
                Cursor.lockState = CursorLockMode.None;
                Settings.Instance.isMoving = false;
                Settings.Instance.isDragging = false;
                waitingForDrag = false;
                Settings.Instance.clickedOnBlock = false;
            }
        }
    }
}
