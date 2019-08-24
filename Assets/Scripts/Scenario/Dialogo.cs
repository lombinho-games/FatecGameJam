using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogo
{
    [SerializeField]
   public List<TextData> texts;
   public string pergunta;
   public string message;
   public bool enabled;
   public bool read;

    public Dialogo(List<TextData> texts, string pergunta, string message, bool enabled, bool read){
        this.texts = texts;
        this.pergunta = pergunta;
        this.message = message;
        this.enabled = enabled;
        this.read = read;
    }

}
