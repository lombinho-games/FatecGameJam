﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

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

        if(GUI.changed){
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        BeginWindows();
        modalWindows.Draw();
        EndWindows();
    }

    public void OpenPistaDialogEditor(PistaItem pista){
        pista.lupa = (LupaButton) EditorGUILayout.ObjectField("Lupa", pista.lupa, typeof(LupaButton), true);
        pista.speech = (SpeechManager) EditorGUILayout.ObjectField("Speech Canvas", pista.speech, typeof(SpeechManager), true);
        pista.data.displayName = EditorGUILayout.TextField("Nome Display", pista.data.displayName);
        pista.data.itemId = EditorGUILayout.TextField("ID do item", pista.data.itemId);

        EditorGUILayout.LabelField("Descrição do item");
        pista.data.itemDescription = EditorGUILayout.TextArea(pista.data.itemDescription, GUILayout.Height(50));

        string[] pistaKeys = pista.manager.textureManager.getPistaKeys();
        string[] posesKeys = pista.manager.textureManager.getPoseKeys();

        int imageValue = EditorGUILayout.Popup("Ícone pista", pista.manager.textureManager.getIndexByKeyPista(pista.data.image), pistaKeys);
        pista.data.image = pistaKeys[imageValue];
        GUI.DrawTexture(new Rect(10, 160, 100, 100), pista.manager.textureManager.pistas[imageValue].sprite.texture);
        GUILayout.Space(120);

        if(GUILayout.Button("Editar diálogo")){                
                //Modal para criar o diálogo
                var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), "CreateDialog", (w) =>
                {
                    pista.data.dialogo.enabled = EditorGUILayout.Toggle("Destravado", pista.data.dialogo.enabled);
                        if(GUILayout.Button("Adicionar Texto")){
                            pista.data.dialogo.texts.Add(new TextData());
                        }

                        for(int j = 0; j < pista.data.dialogo.texts.Count; j ++){
                            GUILayout.Label("Texto " + j, EditorStyles.boldLabel);
                            pista.data.dialogo.texts[j].texto = EditorGUILayout.TextArea(pista.data.dialogo.texts[j].texto);
                            GUILayout.BeginHorizontal("box");
                            pista.data.dialogo.texts[j].owner = EditorGUILayout.TextField("Quem tá falando", pista.data.dialogo.texts[j].owner);
                            
                            int index = pista.manager.textureManager.getIndexByKeyPose( pista.data.dialogo.texts[j].image);
                            if(index == -1) index = 0;
                            int digValue = EditorGUILayout.Popup("Imagem", index, posesKeys);
                             pista.data.dialogo.texts[j].image = posesKeys[digValue];

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
            personagem.speechCanvas = (SpeechManager) EditorGUILayout.ObjectField("Speech Canvas", personagem.speechCanvas, typeof(SpeechManager), true);
            personagem.manager = (InspectionManager) EditorGUILayout.ObjectField("Manager", personagem.manager, typeof(InspectionManager), true);
            personagem.lupa = (LupaButton) EditorGUILayout.ObjectField("Lupa", personagem.lupa, typeof(LupaButton), true);
            //Busca a lista
            string[] pistaKeys = personagem.manager.textureManager.getPistaKeys();
            string[] poseKeys = personagem.manager.textureManager.getPoseKeys();

            //Icone
            int bobValue = EditorGUILayout.Popup("Ícone", personagem.manager.textureManager.getIndexByKeyPista(personagem.data.headBob), pistaKeys);
            personagem.data.headBob = pistaKeys[bobValue];
            GUI.DrawTexture(new Rect(0, 70, 100, 100), personagem.manager.textureManager.pistas[bobValue].sprite.texture);
            GUILayout.Space(120);

            //Pose
            int poseValue = EditorGUILayout.Popup("Pose padrão", personagem.manager.textureManager.getIndexByKeyPose(personagem.data.defaultImage), poseKeys);
            personagem.data.defaultImage = poseKeys[poseValue];
            GUI.DrawTexture(new Rect(0, 210, 100, 100), personagem.manager.textureManager.sprites[poseValue].sprite.texture);
            GUILayout.Space(120);

            GUILayout.Label("Diálogos:");
            if(GUILayout.Button("Adicionar diálogo")){
                Dialogo nd = new Dialogo(new List<TextData>(), "", "", true,false);
                
                //Modal para criar o diálogo
                var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), "CreateDialog", (w) =>
                {
                    nd.message = EditorGUILayout.TextField("ID da Mensagem", nd.message);
                    nd.pergunta = EditorGUILayout.TextField("Pergunta", nd.pergunta);
                    nd.enabled = EditorGUILayout.Toggle("Destravado", nd.enabled);
                    if(GUILayout.Button("OK")){
                        personagem.data.dialogos.Add(nd);
                        w.Close();
                    } 
                    if(GUILayout.Button("Cancel")) w.Close();
                    GUI.DragWindow();
                });
                modalWindows.Add(win);
            }

            if(personagem.data.dialogos == null){
                personagem.data.dialogos = new List<Dialogo>();
            }
            for(int i = 0; i < personagem.data.dialogos.Count; i ++){
                GUILayout.BeginHorizontal("box");
                if(personagem.data.dialogos[i] == null) personagem.data.dialogos[i] = new Dialogo(new List<TextData>(), "", "", true,false);
                GUILayout.Label("Pergunta");
                personagem.data.dialogos[i].pergunta = EditorGUILayout.TextField(personagem.data.dialogos[i].pergunta);
                GUILayout.Label("ID da Mensagem");
                personagem.data.dialogos[i].message = EditorGUILayout.TextField(personagem.data.dialogos[i].message);
                if(GUILayout.Button("Editar")){

                    //Modal pra editar cada texto do diálogo:
                    Dialogo dig = personagem.data.dialogos[i];
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
                            
                            int index = personagem.manager.textureManager.getIndexByKeyPose(dig.texts[j].image);
                            if(index == -1) index = 0;
                            int digValue = EditorGUILayout.Popup("Imagem", index, poseKeys);
                            dig.texts[j].image = poseKeys[digValue];
                            
                            GUILayout.EndHorizontal();
                            GUILayout.Space(10);
                        }

                        if(GUILayout.Button("Fechar")) w.Close();

                        GUI.DragWindow();
                    });
                    modalWindows.Add(win);

                }
                if(GUILayout.Button("Remover")){
                    personagem.data.dialogos.Remove(personagem.data.dialogos[i]);
                }
                GUILayout.EndHorizontal();
            }
    }

}
