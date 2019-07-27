using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interagir : MonoBehaviour
{
   public Move lupa;
   void OnMouseOver()
   {
       if (lupa.lig)
       {
           if(Input.GetMouseButtonDown(0))
           {
            Debug.Log("TA LIGADO E FUNCIONANDO");
           }
       }
   }
}
