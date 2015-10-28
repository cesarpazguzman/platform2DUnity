using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

//Clase que se usa para leer el XML y crear el nivel segun lo que ponga el XML
public class createLevelFromXML
{
    private structureXML m_structureXML;

    //Funcion que con el nombre del nivel (el mismo que el del xml) crea los gameobjects del nivel
    public GameObject generateLevel(string nameLevel)
    {
        //Extraemos el XML y lo deserializamos
        TextAsset xmlTextAsset = (TextAsset)Resources.Load("LevelsXML/"+nameLevel, typeof(TextAsset));
        StringReader stream = new StringReader(xmlTextAsset.text);
        XmlSerializer s = new XmlSerializer(typeof(structureXML));
        m_structureXML = s.Deserialize(stream) as structureXML;

        //Creamos un gameObject vacio que representará el nivel
        GameObject goLevel = new GameObject(nameLevel);
        goLevel.transform.position = Vector3.zero;
        goLevel.transform.parent = Managers.sceneMgr.getRootScene().transform;

        Managers.timeMgr.seconds = m_structureXML.time.seconds;
        Managers.sceneMgr.spawnPointPlayer = new Vector3(m_structureXML.spawnPoint.x, m_structureXML.spawnPoint.y, 0);

        //Creamos cada uno de los items definidos en el XML en la posicion alli indicada, con el scale alli indicado. 
        foreach (structureXML.Item go in m_structureXML.prefabs.items)
        {
            GameObject newGO = Managers.spawnerMgr.createGameObject(Resources.Load("Prefabs/GamePrefabs/"+go.prefab) as GameObject, new Vector3(go.x, go.y, 0), Quaternion.identity);
            newGO.transform.localScale = new Vector3(go.scaleX, go.scaleY, 1);
            //Finalmente como padre ponemos al gameObject que representa el nivel
            newGO.transform.parent = goLevel.transform;
        }

        return goLevel;
    }
}
