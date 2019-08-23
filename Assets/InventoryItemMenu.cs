using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemMenu : MonoBehaviour
{

    public PistaFrame selected;
    public GameObject pistaSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {

    }

    public void Remove() {

        //Destroi a conexão se existe alguma

        Destroy(selected.outerPin.gameObject);
        Destroy(selected.gameObject);
        pistaSlot.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        gameObject.SetActive(false);

    }
}
