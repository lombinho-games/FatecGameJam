using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSolution", menuName = "Game Solution")]

public class SolutionScriptableObject : ScriptableObject
{


    [System.Serializable]
    public class ConnStruct{
        public string pista1; //id da pista
        public string pista2;
        public string connection;
    }
    
    [System.Serializable]
    public class Solution{
        public string event_id;
        public ConnStruct[] conns;
        public TextData[] dialogo;
        public int correctness;
    }

    [SerializeField]
    public Solution[] solutions;

    public Solution GetResponse(List<ItemConnection> connections){
        for(int i = 0; i < solutions.Length; i ++){
            Solution sol = solutions[i];

            bool verificaTudo = true;
            for(int j = 0; j < sol.conns.Length; j++){
                if(ContainsConnectionInList(sol.conns[j], connections) == null){
                    verificaTudo = false;
                    break;
                }
            }

            if(verificaTudo){
                return sol;
            }
        }
        return null;
    }

    public ItemConnection ContainsConnectionInList(ConnStruct st, List<ItemConnection> list){
        foreach(ItemConnection it in list){
            PistaFrame pistaA = it.objectA.GetComponent<PistaPin>().pista.transform.parent.GetComponent<PistaFrame>();
            PistaFrame pistaB = it.objectB.GetComponent<PistaPin>().pista.transform.parent.GetComponent<PistaFrame>();

            if(st.pista1 == pistaA.item.itemID && st.pista2 == pistaB.item.itemID){
                if(st.connection == it.connector){
                    return it;
                }
            }

            if(st.pista1 == pistaB.item.itemID && st.pista2 == pistaA.item.itemID){
                if(st.connection == it.connector){
                    return it;
                }
            }

        }
        return null;
    }

}
