using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCamera : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 movement;
    public  int cena;
    float maxX;
    float Py;

    void Start()
    {
        movement = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (cena)
        {
            case 1: 
            maxX = 2.9f;
            Py = 12.90f;
            transform.position = new Vector3(transform.position.x, Py,-10);
            break;
            case 2: 
            maxX = 7.8f;
            Py = 0f;
            transform.position = new Vector3(transform.position.x, Py,-10);
            break;
            case 3:
            maxX = 7.8f;
            Py = 25f;
            transform.position = new Vector3(transform.position.x, Py, -10);
            break;
            case 4:
            maxX = 7.8f;
            Py = 38.5f;
            transform.position = new Vector3(transform.position.x,Py, -10);
            break;
            default:
            maxX = 10f;
            break;
        } 
        transform.position = transform.position + movement * Time.deltaTime;

        if(transform.position.x > maxX){
            transform.position = new Vector3(maxX, Py, -10);
        }
        if(transform.position.x < -maxX){
            transform.position = new Vector3(-maxX, Py, -10);
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
