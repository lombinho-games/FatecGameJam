using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buto : MonoBehaviour
{
    public Move lupa;

    //Funciona, que felicidade :3
        void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            
            if (lupa.lig){
                 lupa.lig = false;
             }
             else
             {
                 lupa.lig = true;
             }
        }
    }
}
