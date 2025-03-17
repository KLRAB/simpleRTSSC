using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float edgeScrollThreshold = 20f;
    public bool edgeScrolling = true;

    public float zoomSpeed = 5f;
    public float minZoom = 10f;
    public float maxZoom = 50f;

    private Vector2 moveInput;
    private Camera cam;

    void Start()
    {
        cam = Camera.main; 
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>(); 
        }
    }

    void Update()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (edgeScrolling)
        {
            if (mousePos.x >= Screen.width - edgeScrollThreshold) moveInput.x += 1;
            if (mousePos.x <= edgeScrollThreshold) moveInput.x -= 1;
            if (mousePos.y >= Screen.height - edgeScrollThreshold) moveInput.y += 1;
            if (mousePos.y <= edgeScrollThreshold) moveInput.y -= 1;
        }

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += move.normalized * moveSpeed * Time.deltaTime;

        HandleZoom();
    }

    void HandleZoom()
    {
        if (cam == null) return; // NullReferenceException

        float scroll = Mouse.current.scroll.ReadValue().y; //scroll
        if (scroll != 0)
        {
            float zoomAmount = scroll > 0 ? -1f : 1f; 
            cam.orthographicSize += zoomAmount * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
