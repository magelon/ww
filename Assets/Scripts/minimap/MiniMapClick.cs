using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapClick : MonoBehaviour, IPointerClickHandler {
    public Camera minimapCamera;  // The mini-map camera
    public RectTransform minimapRectTransform;  // The UI RectTransform for the minimap
    public Camera mainCamera;  // The main camera that will move
    public Vector2 worldSize;  // The size of the world in world units
    public float mainCameraHeight = 10f;  // Height of the main camera when moved

    public void OnPointerClick(PointerEventData eventData) {
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRectTransform, eventData.position, eventData.pressEventCamera, out localCursor)&&mainCamera!=null) {
            return;
        }

        Vector2 normalizedPosition = new Vector2((localCursor.x + minimapRectTransform.rect.width / 2) / minimapRectTransform.rect.width,
                                                 (localCursor.y + minimapRectTransform.rect.height / 2) / minimapRectTransform.rect.height);

        float mappedX = Mathf.Lerp(-1.5f, 1.5f, normalizedPosition.x);

        // Use normalized y for world position (you can also clamp or map it similarly if needed)
        Vector3 worldPosition = new Vector3(mappedX * worldSize.x, 0, normalizedPosition.y * worldSize.y);

        // Move the main camera to the clicked position
        mainCamera.transform.position = new Vector3(worldPosition.x, mainCameraHeight, -1);
    }
}
