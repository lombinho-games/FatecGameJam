using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpeechableCharacter))]
public class DialogoInspector : Editor
{
    public override void OnInspectorGUI(){

        SpeechableCharacter personagem = (SpeechableCharacter) target;

        personagem.speechCanvas = (Canvas) EditorGUILayout.ObjectField("Speech Canvas", personagem.speechCanvas, typeof(Canvas), true);
        personagem.manager = (InspectionManager) EditorGUILayout.ObjectField("Manager", personagem.manager, typeof(InspectionManager), true);
        personagem.lupa = (LupaButton) EditorGUILayout.ObjectField("Lupa", personagem.lupa, typeof(LupaButton), true);
        personagem.personagens = (GameObject) EditorGUILayout.ObjectField("Personagens", personagem.personagens, typeof(GameObject), true);
        personagem.gui = (GameObject) EditorGUILayout.ObjectField("GUI", personagem.gui, typeof(GameObject), true);
        personagem.headBob = (Sprite) EditorGUILayout.ObjectField("Head Bob", personagem.headBob, typeof(Sprite), true);

    }
}
