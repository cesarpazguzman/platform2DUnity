using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//Script que se encarga de transfomar un nivel a XML
public class createXML
{
    public void generateNewXML(GameObject level)
    {
        //Definimos la parte del tiempo. Se colocará en el XML segun los datos indicados en el prefab correspondiente al nivel en el componente timeManager
        structureXML.Time time = new structureXML.Time();
        time.seconds = level.GetSafeComponent<definingLevel>().seconds;

        structureXML.SpawnPoint spawnPoint = new structureXML.SpawnPoint();
        spawnPoint.x = level.GetSafeComponent<definingLevel>().spawnPoint.x;
        spawnPoint.y = level.GetSafeComponent<definingLevel>().spawnPoint.y;

        //Definimos un bloque de prefabs en el XML
        structureXML.Prefabs levelPrefabs = new structureXML.Prefabs();

        //Guardamos espacio para cada uno de los elementos del nivel para pasarlos al xml
        levelPrefabs.items = new structureXML.Item[level.transform.childCount];

        int i = 0;

        //Recorremos todos los hijos del prefab que representa al nivel
        foreach (Transform item in level.transform)
        {
            string originalPrefabName = item.name.Split('_')[0];

            //Creamos la estructura con los datos necesarios del elemento a instanciar posteriormente.
            levelPrefabs.items[i] = new structureXML.Item();
            levelPrefabs.items[i].prefab = originalPrefabName;
            levelPrefabs.items[i].x = item.position.x;
            levelPrefabs.items[i].y = item.position.y;
            levelPrefabs.items[i].rotZ = item.localRotation.eulerAngles.x;
            levelPrefabs.items[i].scaleX = item.localScale.x;
            levelPrefabs.items[i].scaleY = item.localScale.y;

            ++i;
        }

        //Creamos el elemento a exportar. En el XML tendrá una apartado para el tiempo, y otro que define todos los prefabs del nivel
        structureXML levelToExport = new structureXML();
        levelToExport.prefabs = levelPrefabs;
        levelToExport.time = time;
        levelToExport.spawnPoint = spawnPoint;

        //Exportamos
        FileStream stream = new FileStream("./Assets/Resources/LevelsXML/" + level.name + ".xml", FileMode.Create);
        XmlSerializer s = new XmlSerializer(typeof(structureXML));
        s.Serialize(stream, levelToExport);

    }
    
}
