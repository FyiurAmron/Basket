using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float baseSpeed = 10.0f;
    public float turboSpeed = 50.0f;
    public float sensitivity = 0.25f;
    public bool controllerEnabled = true;

    private readonly Dictionary<KeyCode, Vector3> baseVects = new Dictionary<KeyCode, Vector3> {
        [KeyCode.W] = new Vector3( 0, 0, 1 ),
        [KeyCode.S] = new Vector3( 0, 0, -1 ),
        [KeyCode.D] = new Vector3( 1, 0, 0 ),
        [KeyCode.A] = new Vector3( -1, 0, 0 ),
        [KeyCode.Q] = new Vector3( 0, 1, 0 ),
        [KeyCode.E] = new Vector3( 0, -1, 0 )
    };

    private Vector3 lastMouse = Vector3.zero;

    public void Start() {
        Cursor.visible = false;

        lastMouse = Input.mousePosition;
        lastMouse.y = 0.5f * Screen.height;
    }

    public void Update() {
        if ( Input.GetKeyDown( KeyCode.F1 ) ) {
            transform.eulerAngles = Vector3.zero;
        }

        if ( Input.GetKeyDown( KeyCode.Escape ) ) {
            controllerEnabled ^= true;
        }

        if ( !controllerEnabled ) {
            return;
        }

        Vector3 curMouse = Input.mousePosition;
        Vector3 v = ( lastMouse - curMouse ) * sensitivity;
        transform.eulerAngles += new Vector3( v.y, -v.x, 0 );
        lastMouse = curMouse;

        Vector3 p = new Vector3();
        foreach ( var kvp in baseVects ) {
            if ( Input.GetKey( kvp.Key ) ) {
                p += kvp.Value;
            }
        }

        p *= Time.deltaTime * ( Input.GetKey( KeyCode.LeftShift ) ? turboSpeed : baseSpeed );

        transform.Translate( p );
    }
}
