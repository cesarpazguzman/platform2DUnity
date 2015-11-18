using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class XMLGenerator : EditorWindow {

    private createXML m_generateXML = null;
    private GameObject m_level = null;

    [MenuItem("Our Tools/XmlGenerator... %g")]
    private static void showWindowXML()
    {
        EditorWindow.GetWindow(typeof(XMLGenerator));
    }

    void OnGUI()
    {

        if (m_generateXML == null) m_generateXML = new createXML();

        GUILayout.Label("Level Generator to XML", EditorStyles.boldLabel);
        GUILayout.Label("It generates a file XML of the levels selected. It's neccesary a GameObject with tag Level");
        m_level = (GameObject)EditorGUILayout.ObjectField(m_level, typeof(GameObject), true);

        if (GUILayout.Button("Generate a new XML") && m_level != null)
        {
            Debug.Log("Generating XML, name: "+m_level.name);
            m_generateXML.generateNewXML(m_level);
        }
    }

}
