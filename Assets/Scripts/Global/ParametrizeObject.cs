using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametrizeObject : MonoBehaviour
{
    //Essa classe serve em situações que você queira somente adicionar um parâmetro a um game object, sem adicionar codigo nenhum
    //E não quer ter que criar uma classe vazia com 1 parametro só pra isso
    public Dictionary<string, object> parameters;

    public object getParameter(string param){
        if(parameters == null)
            parameters = new Dictionary<string, object>();

        if(parameters.ContainsKey(param))
            return parameters[param];

        return null;
    }

    public void setParameter(string param, object obj){
        if(parameters == null)
            parameters = new Dictionary<string, object>();

        parameters[param] = obj;
    }
}
