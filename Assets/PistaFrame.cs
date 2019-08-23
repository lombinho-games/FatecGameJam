using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaFrame : MonoBehaviour
{

    public GameObject outerPin;
    public bool draggin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FrameDrag()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        transform.position = mousePos;
        draggin = true;

    }

    public void EndDrag()
    {
        draggin = false;
    }

}
