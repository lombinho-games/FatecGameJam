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
        
        if(Selection.activeGameObject == null) return;

        SpeechableCharacter personagem = (SpeechableCharacter) Selection.activeGameObject.GetComponent<SpeechableCharacter>();
        if(personagem != null)
            OpenCharacterDialogEditor(personagem);

        PistaItem pista = (PistaItem) Selection.activeGameObject.GetComponent<PistaItem>();
        if(pista != null)
            OpenPistaDialogEditor(pista);
        
         
        BeginWindows();
        modalWindows.Draw();
        EndWindows();
    }

    public void OpenPistaDialogEditor(PistaItem pista){
        pista.lupa = (LupaButton) EditorGUILayout.ObjectField("Lupa", pista.lupa, typeof(LupaButton), true);
        pista.speech = (SpeechManager) EditorGUILayout.ObjectField("Speech Canvas", pista.speech, typeof(SpeechManager), true);
        pista.displayName = EditorGUILayout.TextField("Nome Display", pista.displayName);
        pista.itemId = EditorGUILayout.TextField("ID do item", pista.itemId);

        if(GUILayout.Button("Editar diálogo")){                
                //Modal para criar o diálogo
                var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), "CreateDialog", (w) =>
                {
                    pista.dialogo.enabled = EditorGUILayout.Toggle("Destravado", pista.dialogo.enabled);
                        if(GUILayout.Button("Adicionar Texto")){
                            pista.dialogo.texts.Add(new TextData());
                        }

                        for(int j = 0; j < pista.dialogo.texts.Count; j ++){
                            GUILayout.Label("Texto " + j, EditorStyles.boldLabel);
                            pista.dialogo.texts[j].texto = EditorGUILayout.TextArea(pista.dialogo.texts[j].texto);
                            GUILayout.BeginHorizontal("box");
                            pista.dialogo.texts[j].owner = EditorGUILayout.TextField("Quem tá falando", pista.dialogo.texts[j].owner);
                            pista.dialogo.texts[j].image = ((Sprite)EditorGUILayout.ObjectField(pista.dialogo.texts[j].image, typeof(Sprite), false));
                            Debug.Log("Atualizando imagem " + pista.dialogo.texts[j].image);
                            GUILayout.EndHorizontal();
                            GUILayout.Space(10);
                        }

                        if(GUILayout.Button("Fechar")) w.Close();

                        GUI.DragWindow();
                });
                modalWindows.Add(win);
            }
    
    }

    public void OpenCharacterDialogEditor(SpeechableCharacter personagem){
            personagem.speechCanvas = (Canvas) EditorGUILayout.ObjectField("Speech Canvas", personagem.speechCanvas, typeof(Canvas), true);
            personagem.manager = (InspectionManager) EditorGUILayout.ObjectField("Manager", personagem.manager, typeof(InspectionManager), true);
            personagem.lupa = (LupaButton) EditorGUILayout.ObjectField("Lupa", personagem.lupa, typeof(LupaButton), true);
            personagem.headBob = (Sprite) EditorGUILayout.ObjectField("Head Bob", personagem.headBob, typeof(Sprite), true);
            personagem.defaultImage = (Sprite) EditorGUILayout.ObjectField("Pose padrão", personagem.defaultImage, typeof(Sprite), true);

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
                            dig.texts[j].image = ((Sprite)EditorGUILayout.ObjectField(dig.texts[j].image, typeof(Sprite), false));
                            Debug.Log("Atualizando imagem " + dig.texts[j].image);

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

}
