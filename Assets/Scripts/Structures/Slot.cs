using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public int id;
    public int scenario;
    public long date;
    public long gameTime;

    public Slot(int id, int scenario, DateTime date, DateTime gameTime){
        this.id = id;
        this.scenario = scenario;
        this.date = date.Ticks;
        this.gameTime = gameTime.Ticks;

    }
}
