using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, hotspot, cursorMode);
        }
        else
        {
            Debug.LogWarning("Brak kursora! Przypisz teksturê w Inspectorze.");
        }
    }
}