using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveMouse : MonoBehaviour
{
    Vector3 mousePos;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 PosI = transform.position;
    }
    /* void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            if (!atv)
            {
                atv = true;
                Debug.Log("Ativo");
            }
            else
            {
                atv = false;
                Debug.Log("Desligado");
            }
        }
    }  */
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)){
            Debug.Log("Ok");
        }
       /* mousePos = Input.mousePosition;
        if(atv)
        {

        }*/
    }
}
