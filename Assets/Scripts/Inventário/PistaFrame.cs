using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistaFrame : MonoBehaviour
{
    public Quadro quadro;
    public GameObject outerPin;
    public GameObject originalSlot;
    public GameObject linhaGroup;
    public InventoryItem item;
    public bool draggin;

    public bool selected = false;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color = selected ? Color.blue : Color.white;
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
        if(quadro.creatingConnection != null) {// Se eu clico com conexão
            if(quadro.creatingConnection.objectA == outerPin) { //Conectando comigo msm
                Destroy(quadro.creatingConnection.gameObject);
                quadro.creatingConnection = null;
            }
            else {// Conectando com outro objeto

                bool exists = false; //pergunta se já existe um objeto com essa mesma conexão

                for (int i = 0; i < linhaGroup.transform.childCount; i++) {
                    ItemConnection linha = linhaGroup.transform.GetChild(i).gameObject.GetComponent<ItemConnection>();
                    if (linha == quadro.creatingConnection) continue;

                    if (linha.objectA == quadro.creatingConnection.objectA && linha.objectB == outerPin) {
                        exists = true;
                    }
                    else if (linha.objectA == outerPin && linha.objectB == quadro.creatingConnection.objectA) {
                        exists = true;
                    }
                }

                if (!exists) {
                    quadro.creatingConnection.objectB = outerPin;
                    quadro.creatingConnection.isOnMouse = false;
                    quadro.creatingConnection = null;
                }
                else {
                    Destroy(quadro.creatingConnection.gameObject);
                    quadro.creatingConnection = null;
                }
            }

        }
        else if (quadro.checkSelection){ //Se tá com seleção de check ativado
            selected = !selected;
        }
        else{
            if (!draggin) {
                quadro.OpenMenu(this, originalSlot);
            }
        }
    }

}
