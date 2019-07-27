using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{   
    public float speed = 0.25f;
    Vector3 target;
    Vector3 posI;
    public bool Holding;
      void Start () {
         target = transform.position;
         posI = target;
     }
    void OnMouseDrag() {
        if (Input.GetMouseButton(0)){
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            Holding = true;
        }
        
    }
    void Update()
    {
        if (!Input.GetMouseButton(0)){
        transform.position = Vector3.MoveTowards(transform.position, posI, speed);
        Holding = false;
        }
    }
        
        
     

 
}

        
        
    

