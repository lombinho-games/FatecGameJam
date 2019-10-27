using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class FullscreenDetector : MonoBehaviour
{
    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        int cont = 0;
        foreach(FullScreenMode f in Enum.GetValues(typeof(FullScreenMode))){

            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = f.ToString();
            dropdown.options.Add(data);

            if(Screen.fullScreenMode == f){
                dropdown.value = cont;
            }
            cont++;
        }
    }

    public void ChangeFullscreen(){
        FullScreenMode f = (FullScreenMode) Enum.GetValues(typeof(FullScreenMode)).GetValue(dropdown.value);
        Screen.SetResolution(Screen.width, Screen.height, f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
