using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCamera : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 movement;

    public float maxX;

    void Start()
    {
        movement = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + movement * Time.deltaTime;

        if(transform.position.x > maxX){
            transform.position = new Vector3(maxX, 0, -10);
        }
        if(transform.position.x < -maxX){
            transform.position = new Vector3(-maxX, 0, -10);
        }
    }

    public void GoLeft(){
        movement = new Vector3(-10, 0, 0);
    }

    public void GoRight(){
        movement = new Vector3(10, 0, 0);
    }

    public void StopMove(){
        movement = new Vector3(0, 0, 0);
    }
}
