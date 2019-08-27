using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogo
{
    [SerializeField]
   public List<TextData> texts;
   public string pergunta;
   public string id;
   public string unlock_message;
   public bool enabled;
   public bool read;

    public Dialogo(List<TextData> texts, string pergunta, string id, bool enabled, bool read, string unlock_message){
        this.texts = texts;
        this.pergunta = pergunta;
        this.id = id;
        this.enabled = enabled;
        this.read = read;
        this.unlock_message = unlock_message;
    }

}
