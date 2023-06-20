using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    Vector2 resolution, resolutionInWorldUnits = new Vector2(17.8f, 10);
    private void Start()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }
    private void LateUpdate()
    {
        FollowMouse();
    }
    private void FollowMouse()
    {
        //Debug.Log(Input.mousePosition);
        transform.position = Input.mousePosition / resolution * resolutionInWorldUnits;
    }
}
