using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HideButtonOnFlip : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = transform.lossyScale.x > 0;
        button.GetComponentInChildren<Text>().color = new Color(
            1, 1, 1, button.interactable ? 1 : 0
        );
    }
}
