using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    bool lig = false;
    public float speed = 0.25f;
     private Vector3 target;
     private Vector3 posI;
 
     void Start () {
         target = transform.position;
         posI = target;
     }
     
     void Update () {
         if(Input.GetMouseButtonDown(0))
         {
             if (lig){
                 lig = false;
             }
             else
             {
                 lig = true;
             }
         }
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        if(lig){
        transform.position = Vector3.MoveTowards(transform.position, target, speed/2);
        }
        else
        {
        transform.position = Vector3.MoveTowards(transform.position, posI, speed/2);
        }
     }    
 }