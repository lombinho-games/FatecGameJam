using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaFrame : MonoBehaviour
{
    public Quadro quadro;
    public GameObject outerPin;
    public GameObject originalSlot;
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
        if (quadro.creatingConnection == null) {
            quadro.CloseMenu();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            transform.position = mousePos;
            draggin = true;
        }

    }

    public void EndDrag()
    {
        draggin = false;
    }

    public void Click()
    {
        if(quadro.creatingConnection != null) {
            if(quadro.creatingConnection.objectA == gameObject) {
                Destroy(quadro.creatingConnection.gameObject);
                quadro.creatingConnection = null;
            }
            else {
                quadro.creatingConnection.objectB = outerPin;
                quadro.creatingConnection.isOnMouse = false;
                quadro.creatingConnection = null;

                return;
            }

        }

        if (!draggin && !quadro.menu.activeInHierarchy) {
            quadro.OpenMenu(this, originalSlot);
        }

        
    }

}
