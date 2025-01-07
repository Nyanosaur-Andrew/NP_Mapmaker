using UnityEngine;
using UnityEngine.EventSystems;

public class Camera2D : MonoBehaviour
{
    public float scrollSpeed;
    public float zoomSpeed;

    Camera cam;

    private Vector3 initialMousePos;
    private Vector3 prevMousePos;

    public bool isDraggig = false;

    private void Start() {
        cam = Camera.main;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        if (Input.GetMouseButton(0)) {
            //Debug.Log(Input.mousePositionDelta);
            if(Input.mousePositionDelta.magnitude > 0f) {
                isDraggig = true;
            }
            transform.position -= Input.mousePositionDelta * scrollSpeed * (cam.orthographicSize*0.1f);
        }
        if (Input.GetMouseButtonUp(0)) {
            isDraggig = false;
        }

        float newSize = Mathf.Max((cam.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed), 1f);
        cam.orthographicSize = newSize;
    }
}
