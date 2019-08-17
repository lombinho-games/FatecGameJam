using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
     public LupaButton lupa;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(lupa.pressed){
        Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        
    }
}
