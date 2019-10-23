using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDetector : MonoBehaviour
{

    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        int cont = 0;
        foreach(Resolution res in Screen.resolutions){
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = res.width + "x" + res.height;
            dropdown.options.Add(data);

            if(Screen.currentResolution.Equals(res)){
                dropdown.value = cont;
            }
            cont++;
        }
    }

    public void ChangeResolution(){
        Resolution res = Screen.resolutions[dropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
