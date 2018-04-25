using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To type the next 4 lines, start by typing /// and then Tab
/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera at [0,0,0].
/// </summary>
public class BoundsCheck : MonoBehaviour {
    [Header("Set in Inspector")]  //a                                     Because this is intended to be a reusable piece of code, adding some internal documentation to it is useful. the lines above the class declaration that all begin with /// are part of C#;s built in documentaiton system. after you've typed this, it interprets the text between the <summary> tags as a summary of what the class is used for. After typing it, hover your mouse over the name BoundsCheck on the line marked //a and you should see a popup with this class summary.
    public float radius = 1f;
    public bool keepOnScreen = true;


    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;       //b                              Camera.main gives you access to the first camera with the tag MainCamera in your scene. Then, if the camera is orthographic, .orthographicSize gives you the size number from the camera inspector (which is 40 in this case). this makes camHeight the distance from the origin of the world (position [0,0,0]) to the top or bottom edge of the screen in world coordinates.
        camWidth = camHeight * Camera.main.aspect;     //c                                 Camera.main.aspect is the aspect ratio of the camera in width/height as defined by the aspect ratio of the Game pane (currently set to Portrait (3:4)). by multiplying camHeight by .aspect, you can get the distance from the origin to the left or right edge of the screen.
    }

    private void LateUpdate()               // d                                                 LateUpdate() is called every frame after Update() has been called on all GameObjects. if this code were in the Update function it might happen either before or after the update call on the hero script. putting this code in LateUpdate avoids causing a race condition between the two update functions and ensures that Hero.Update() moves the _Hero GameObject to the new position each frame before this function is called and bounds _Hero to the screen.
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if(keepOnScreen && !isOnScreen) 
        {
            transform.position = pos;                  
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
                             
        }
        
    }

    //Draw the bounds in the Scene pane using OnDrawGismos()
    private void OnDrawGizmos() //e                         OnDrawGizmos() is a built in monobehaviour method that can draw to the scene pane
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
