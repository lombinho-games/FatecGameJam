using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LupaMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        float oz = position.z;
        position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = oz;
        transform.position = position;
    }
}
