using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class DialogWindow : EditorWindow
{

    ModalSystem modalWindows = new ModalSystem();

    [MenuItem("Window/Dialogs")]
    public static void ShowWindow(){
        DialogWindow window = GetWindow<DialogWindow>("Editor de Diálogos");
    }

    void OnSelectionChange()
    {
        Repaint();
    }

    void OpenPopup(string aTitle)
     {
         var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), aTitle, (w) =>
         {
             if (GUILayout.Button("Open another popup"))
                 OpenPopup(aTitle + "->subwindow");
             if (GUILayout.Button("Close"))
                 w.Close();
             GUI.DragWindow();
         });
         modalWindows.Add(win);
     }

    void OnGUI(){
        GUI.enabled = !modalWindows.IsWindowOpen;
         
        SpeechableCharacter personagem = (SpeechableCharacter) Selection.activeGameObject.GetComponent<SpeechableCharacter>();

        if(personagem != null){
            personagem.speechCanvas = (Canvas) EditorGUILayout.ObjectField("Speech Canvas", personagem.speechCanvas, typeof(Canvas), true);
            personagem.manager = (InspectionManager) EditorGUILayout.ObjectField("Manager", personagem.manager, typeof(InspectionManager), true);
            personagem.lupa = (LupaButton) EditorGUILayout.ObjectField("Lupa", personagem.lupa, typeof(LupaButton), true);
            personagem.personagens = (GameObject) EditorGUILayout.ObjectField("Personagens", personagem.personagens, typeof(GameObject), true);
            personagem.gui = (GameObject) EditorGUILayout.ObjectField("GUI", personagem.gui, typeof(GameObject), true);
            personagem.headBob = (Sprite) EditorGUILayout.ObjectField("Head Bob", personagem.headBob, typeof(Sprite), true);
        
            GUILayout.Label("Diálogos:");
            if(GUILayout.Button("Adicionar diálogo")){
                Dialogo nd = new Dialogo(new List<TextData>(), "", "", true);
                
                //Modal para criar o diálogo
                var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), "CreateDialog", (w) =>
                {
                    nd.message = EditorGUILayout.TextField("ID da Mensagem", nd.message);
                    nd.pergunta = EditorGUILayout.TextField("Pergunta", nd.pergunta);
                    nd.enabled = EditorGUILayout.Toggle("Destravado", nd.enabled);
                    if(GUILayout.Button("OK")){
                        personagem.dialogos.Add(nd);
                        w.Close();
                    } 
                    if(GUILayout.Button("Cancel")) w.Close();
                    GUI.DragWindow();
                });
                modalWindows.Add(win);
            }

            if(personagem.dialogos == null){
                personagem.dialogos = new List<Dialogo>();
            }
            for(int i = 0; i < personagem.dialogos.Count; i ++){
                GUILayout.BeginHorizontal("box");
                if(personagem.dialogos[i] == null) personagem.dialogos[i] = new Dialogo(new List<TextData>(), "", "", true);
                GUILayout.Label("Pergunta");
                personagem.dialogos[i].pergunta = EditorGUILayout.TextField(personagem.dialogos[i].pergunta);
                GUILayout.Label("ID da Mensagem");
                personagem.dialogos[i].message = EditorGUILayout.TextField(personagem.dialogos[i].message);
                if(GUILayout.Button("Editar")){

                    //Modal pra editar cada texto do diálogo:
                    Dialogo dig = personagem.dialogos[i];
                    var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), "Edit Dialog", (w) =>
                    {
                        dig.enabled = EditorGUILayout.Toggle("Destravado", dig.enabled);
                        if(GUILayout.Button("Adicionar Texto")){
                            dig.texts.Add(new TextData());
                        }

                        for(int j = 0; j < dig.texts.Count; j ++){
                            GUILayout.Label("Texto " + j, EditorStyles.boldLabel);
                            dig.texts[j].texto = EditorGUILayout.TextArea(dig.texts[j].texto);
                            GUILayout.BeginHorizontal("box");
                            dig.texts[j].owner = EditorGUILayout.TextField("Quem tá falando", dig.texts[j].owner);
                            dig.texts[j].image = (Sprite)EditorGUILayout.ObjectField(dig.texts[j].image, typeof(Sprite), false);
                            GUILayout.EndHorizontal();
                            GUILayout.Space(10);
                        }

                        if(GUILayout.Button("Fechar")) w.Close();

                        GUI.DragWindow();
                    });
                    modalWindows.Add(win);

                }
                if(GUILayout.Button("Remover")){
                    personagem.dialogos.Remove(personagem.dialogos[i]);
                }
                GUILayout.EndHorizontal();
            }
        }
         
        BeginWindows();
        modalWindows.Draw();
        EndWindows();
    }

}
