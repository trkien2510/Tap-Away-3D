using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 0.5f;
    private Vector3 lastMousePosition;
    private bool isDragging = false;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(45, 45, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            float rotX = delta.y * rotationSpeed;
            float rotY = -delta.x * rotationSpeed;

            transform.Rotate(Camera.main.transform.right, rotX, Space.World);
            transform.Rotate(Vector3.up, rotY, Space.World);

            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}