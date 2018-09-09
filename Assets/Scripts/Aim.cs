using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

 public Texture2D mouseTexture;

    void Awake ()
    {
       Cursor.visible = false;
    }

    void OnGUI ()
    {
        Vector2 m = Event.current.mousePosition;
        GUI.Label(new Rect(m.x - 13,m.y - 14,mouseTexture.width,mouseTexture.height),mouseTexture);
    }
}
