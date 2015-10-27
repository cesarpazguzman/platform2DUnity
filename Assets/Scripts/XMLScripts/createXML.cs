﻿using UnityEngine;
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
        time.seconds = level.GetComponent<timeManager>().seconds;
        time.alarm = level.GetComponent<timeManager>().lastSecondsAlarm;


        //Definimos un bloque de prefabs en el XML
        structureXML.Prefabs levelPrefabs = new structureXML.Prefabs();

        //Obtenemos los hijos del nivel
        Transform[] children = level.GetComponentsInChildren<Transform>();

        //Guardamos espacio para cada uno de los elementos del nivel para pasarlos al xml
        levelPrefabs.items = new structureXML.Item[children.Length];

        int i = 0;

        //Recorremos todos los hijos del prefab que representa al nivel
        foreach (Transform item in children)
        {
            //Solo se tiene en cuenta los hijos, no los nietos etc etc del prefab del nivel
            if (item.parent != level.transform) continue;

            string originalPrefabName = item.name.Split('_')[0];

            //Creamos la estructura con los datos necesarios del elemento a instanciar posteriormente.
            levelPrefabs.items[i] = new structureXML.Item();
            levelPrefabs.items[i].prefab = originalPrefabName;
            levelPrefabs.items[i].x = (item.position.x).ToString();
            levelPrefabs.items[i].y = (item.position.y).ToString();
            levelPrefabs.items[i].rotZ = (item.localRotation.eulerAngles.x).ToString();
            levelPrefabs.items[i].scaleX = (item.localScale.x).ToString();
            levelPrefabs.items[i].scaleY = (item.localScale.y).ToString();

            ++i;
        }

        //Creamos el elemento a exportar. En el XML tendrá una apartado para el tiempo, y otro que define todos los prefabs del nivel
        structureXML levelToExport = new structureXML();
        levelToExport.prefabs = levelPrefabs;
        levelToExport.time = time;

        //Exportamos
        FileStream stream = new FileStream("./Assets/Resources/" + level.name + ".xml", FileMode.Create);
        XmlSerializer s = new XmlSerializer(typeof(structureXML));
        s.Serialize(stream, levelToExport);

    }
    
}
