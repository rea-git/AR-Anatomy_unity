using UnityEngine;

public class TouchAndMouseRotateZoom : MonoBehaviour
{
    public float autoRotationSpeed = 20f;
    private float touchRotationSpeed = 0.2f;
    private float mouseRotationSpeed = 5f;
    private float zoomSpeed = 0.01f;
    private float mouseZoomSpeed = 0.5f;

    private float minScale = 0.5f;
    private float maxScale = 2f;

    private bool isTouching = false;

    void Update()
    {
        // --- TOUCH CONTROLS ---
        if (Input.touchCount > 0)
        {
            isTouching = true;

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    float rotX = touch.deltaPosition.y * touchRotationSpeed;
                    float rotY = -touch.deltaPosition.x * touchRotationSpeed;
                    transform.Rotate(rotX, rotY, 0, Space.World);
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch t0 = Input.GetTouch(0);
                Touch t1 = Input.GetTouch(1);

                Vector2 prevT0 = t0.position - t0.deltaPosition;
                Vector2 prevT1 = t1.position - t1.deltaPosition;

                float prevDist = (prevT0 - prevT1).magnitude;
                float currDist = (t0.position - t1.position).magnitude;

                float delta = currDist - prevDist;
                float scaleFactor = delta * zoomSpeed;

                Zoom(scaleFactor);
            }
        }
        else
        {
            isTouching = false;

            // --- MOUSE CONTROLS FOR EDITOR ---
            if (Input.GetMouseButton(0)) // Left click to rotate
            {
                float rotX = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
                float rotY = -Input.GetAxis("Mouse X") * mouseRotationSpeed;
                transform.Rotate(rotX, rotY, 0, Space.World);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                Zoom(scroll * mouseZoomSpeed);
            }
        }

        // --- AUTO ROTATION when idle ---
        if (!isTouching && !Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up * autoRotationSpeed * Time.deltaTime, Space.World);
        }
    }

    void Zoom(float factor)
    {
        Vector3 newScale = transform.localScale + Vector3.one * factor;
        newScale = new Vector3(
            Mathf.Clamp(newScale.x, minScale, maxScale),
            Mathf.Clamp(newScale.y, minScale, maxScale),
            Mathf.Clamp(newScale.z, minScale, maxScale)
        );
        transform.localScale = newScale;
    }
}
