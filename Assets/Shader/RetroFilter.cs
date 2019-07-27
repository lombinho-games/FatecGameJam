using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetroFilter : MonoBehaviour
{
    public Material effectMaterial;
    [ExecuteInEditMode]
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void onRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination,effectMaterial);
    }
}
