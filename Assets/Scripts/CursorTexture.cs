using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTexture : MonoBehaviour
{
    
    public Texture2D cursor;

    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    /*

    void OnGUI() {
        Vector3 mousePos = Input.mousePosition;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        mousePos.x = Mathf.Clamp(mousePos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        mousePos.y = Mathf.Clamp(mousePos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        Input.mousePosition = mousePos;
    }

    */

}
