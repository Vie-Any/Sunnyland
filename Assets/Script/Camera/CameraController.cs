using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // The object of the player
    public Transform player;
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        // make the camera position same as the player so that the camera will follow the player.
        // because the scene is simple and no need to change the y position of the camera
        // so just let the camera position y 0 could lock the camera and provide an great user experience
        transform.position = new Vector3(player.position.x, 0, -10f);
    }
}
