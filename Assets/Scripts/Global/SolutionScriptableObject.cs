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

    public List<Solution> GetResponse(List<ItemConnection> connections){

        List<Solution> resp = new List<Solution>();

        for(int i = 0; i < solutions.Length; i ++){ //Passa por todas as soluções
            Solution sol = solutions[i];

            bool verificaTudo = true; //Pra cada solução que existe, checa se as conexões da solução estão contidas dentro do grupo de
            //conexões selecionadas
            for(int j = 0; j < sol.conns.Length; j++){
                if(ContainsConnectionInList(sol.conns[j], connections) == null){
                    verificaTudo = false; //se existe uma conexão na solução que não esteja no grupo, já tá errado
                    break;
                }
            }

            //Isso significa que se o grupo de conexões selecionadas pelo jogador tenha todas as conexões de uma solução específica,
            //Mesmo que tenha mais soluções selecionadas, ele vai dar certo

            if(verificaTudo){
                resp.Add(sol);
            }
        }
        return resp;
    }

    public ItemConnection ContainsConnectionInList(ConnStruct st, List<ItemConnection> list){
        foreach(ItemConnection it in list){
            if(st.pista1 == it.GetPistaA().item.itemID && st.pista2 == it.GetPistaB().item.itemID){
                if(st.connection == it.connector){
                    return it;
                }
            }
            if(st.pista1 == it.GetPistaB().item.itemID && st.pista2 == it.GetPistaA().item.itemID){
                if(st.connection == it.connector){
                    return it;
                }
            }
        }
        return null;
    }

}
