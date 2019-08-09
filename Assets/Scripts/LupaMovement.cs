using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LupaMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
        transform.position = new Vector3(transform.position.x+.45f, transform.position.y-.5f, -7);
    }
}
