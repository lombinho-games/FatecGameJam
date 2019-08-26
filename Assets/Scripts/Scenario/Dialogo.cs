using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogo
{
    [SerializeField]
   public List<TextData> texts;
   public string pergunta;
   public string unlock_message;
   public string message;
   public bool enabled;
   public bool read;

    public Dialogo(List<TextData> texts, string pergunta, string unlock_message, bool enabled, bool read, string message){
        this.texts = texts;
        this.pergunta = pergunta;
        this.unlock_message = unlock_message;
        this.enabled = enabled;
        this.read = read;
        this.message = message;
    }

}
