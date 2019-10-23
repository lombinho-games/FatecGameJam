using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlots : MonoBehaviour
{
    public GameObject NewSave;
    // Start is called before the first frame update
    void Start()
    {
        if(!SaveGameSystem.DoesSaveGameExist("slot0")){
             NewSave.gameObject.SetActive(true);
        }
        else
        {
            NewSave.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
