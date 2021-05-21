using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // The camera
    public Transform camera;

    // range of the background movement
    public float moveRate;

    // the inital position
    private float startPointX, startPointY;

    // for extention lock y movement or not
    public bool lockX = false;

    // for extention lock y movement or not
    public bool lockY = true;

    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            lockX ? transform.position.x : (startPointX + camera.transform.position.x * moveRate),
            lockY ? transform.position.y : (startPointY + camera.transform.position.y * moveRate));
    }
}