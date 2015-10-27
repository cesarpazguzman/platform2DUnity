using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(playerController))]
public class playerControllerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("Definición de los incrementos de velocidad, velocidad máxima, y 4 gameObjects vacíos usados para saber porque lado golpea el player.", MessageType.Info);
    }
}
