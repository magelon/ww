using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float minX = 0f;
    public float maxX = 1280f;
    public float minY = 0f;
    public float maxY = 720f;

    private Camera mainCamera;
    private Vector3 defaultPosition;

    private Vector3 touchStartPos;
    private Vector3 previousTouchPos;

    private void Start()
    {
        mainCamera = Camera.main;
        defaultPosition = transform.position;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            previousTouchPos = touchStartPos;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 touchDelta = Input.mousePosition - previousTouchPos;
            Vector3 panDirection = new Vector3(-touchDelta.x, 0, 0);
            Vector3 newPosition = transform.position + panDirection * panSpeed * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.x, minX, maxX),
                transform.position.y,
                transform.position.z
            );
            previousTouchPos = Input.mousePosition;
        }

        // Handle zooming
        float zoomInput = -Input.GetAxis("Mouse ScrollWheel") + HandleTouchZoom();
        float newSize = mainCamera.orthographicSize - zoomInput * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }

    private float HandleTouchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            return currentMagnitude - prevMagnitude;
        }
        return 0;
    }
}
