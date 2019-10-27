using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ExitPoint))]
[CanEditMultipleObjects]
public class ExitPointEditor : Editor
{
    ExitPoint exitPoint;

    void onEnable(){
        exitPoint = (ExitPoint)serializedObject.targetObject;
    }

    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        exitPoint = (ExitPoint)serializedObject.targetObject;
        serializedObject.Update();

        exitPoint.GetData().title = EditorGUILayout.TextField("Title", exitPoint.GetData().title);

        string[] cursors = new string[]{
            "Left", "Right", "Up", "Down"
        };
        int opt = EditorGUILayout.Popup("Cursor Direction", exitPoint.GetData().iCursor, cursors);
        exitPoint.SetCursorDirection(opt);

        string[] options = new string[SceneManager.sceneCountInBuildSettings];
        for(int i = 0; i < options.Length; i ++){
            options[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }
        exitPoint.SetExitPoint(EditorGUILayout.Popup("Exit Point", exitPoint.GetExitPoint(), options));
        exitPoint.SetEnabled(EditorGUILayout.Toggle("Destravado:", exitPoint.GetData().enabled));

        exitPoint.data.unlockMessage = EditorGUILayout.TextField("Mensagem de desbloqueio: ", exitPoint.data.unlockMessage);

        //serializedObject.ApplyModifiedProperties();

        if(GUI.changed){
            EditorUtility.SetDirty(exitPoint);
            EditorSceneManager.MarkSceneDirty(exitPoint.gameObject.scene);       
        }
    }
}
