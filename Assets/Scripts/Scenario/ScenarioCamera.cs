using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCamera : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 movement;
    Camera cam;
    int velocity = 70;

    void Start()
    {
        movement = new Vector3(0, 0, 0);
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        GetComponent<Rigidbody2D>().AddForce(movement);

    }

    public void GoLeft(){
        movement = new Vector3(-velocity, 0, 0);
    }

    public void GoRight(){
        movement = new Vector3(velocity, 0, 0);
    }

    public void StopMove(){
        movement = new Vector3(0, 0, 0);
    }
}
