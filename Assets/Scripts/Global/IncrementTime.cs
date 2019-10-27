using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IncrementTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalProfile.Slot != -1){
            DateTime dt = new DateTime(GlobalProfile.gameSlots.Slots[GlobalProfile.Slot].gameTime);
            dt = dt.AddSeconds(Time.unscaledDeltaTime);
            GlobalProfile.gameSlots.Slots[GlobalProfile.Slot].gameTime = dt.Ticks;
        }
    }
}
