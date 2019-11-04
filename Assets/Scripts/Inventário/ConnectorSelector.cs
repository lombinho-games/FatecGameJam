using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConnectorSelector : MonoBehaviour
{

    [HideInInspector]
    public ItemConnection connection;

    // Start is called before the first frame update
    void Start()
    {

        GameObject template = transform.GetChild(0).gameObject;


        foreach(string conn in ItemConnection.connectors){

            GameObject obj = Instantiate(template);
            obj.transform.position = gameObject.transform.position + new Vector3(0, (transform.childCount-1) * -100, 0);
            RectTransform rect = obj.GetComponent<RectTransform>();
            Text text = obj.GetComponent<Text>();
            text.text = conn;

            EventTrigger et = obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>( (BaseEventData data) => {
                transform.parent.gameObject.SetActive(false);
                connection.connector = conn;
            }));

            et.triggers.Add(entry);

            obj.transform.SetParent(gameObject.transform, false);
        }

        Destroy(template);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
