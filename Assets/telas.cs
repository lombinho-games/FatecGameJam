﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class telas : MonoBehaviour
{
    public ScenarioCamera scenarioCamera;
    public AudioSource audioComponent;
    public AudioClip musicToChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Entrar(int cena){
        scenarioCamera.cena = cena;
        Camera.main.transform.position = new Vector3(
            0, Camera.main.transform.position.y,
            Camera.main.transform.position.z
        );
        if(musicToChange != null){
            audioComponent.Stop();
            audioComponent.clip = musicToChange;
            audioComponent.Play();
        }

    }
    
}
