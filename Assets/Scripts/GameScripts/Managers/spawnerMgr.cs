using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnerMgr : MonoBehaviour {

    //Diccionario que almacena los objetos desactivados 
    private Dictionary<string, List<GameObject>> m_poolDeactivatedObjects;
    //id único que define al siguiente gameObject que se creara o se sacará del pool, servirá para ponerle un nombre único
    private int m_nextID;


    void Awake()
    {
        m_poolDeactivatedObjects = new Dictionary<string, List<GameObject>>();
        m_nextID = 0;
    }

	// Use this for initialization
    void Start()
    {

    }

    //Funcion que de forma transparente crea objetos desde cualquier parte del código. Desde esta funcion sacaremos bien el objeto del pool si este
    //se encuentra desactivado, o bien creamos uno nuevo.
    public GameObject createGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newObject = null;

        //Si en el pool existe una lista que represente al gameObject a crear
        if (m_poolDeactivatedObjects.ContainsKey(prefab.name.Split('_')[0]))
        {
            List<GameObject> listObjects = m_poolDeactivatedObjects[prefab.name.Split('_')[0]];
            //Si de este tipo de prefab existe alguno desactivado
            if (listObjects.Count > 0)
            {
                //Guardamos la primera aparición del gameObject desactivado que queremos activar
                newObject = listObjects[0];
                //Lo eliminamos
                listObjects.RemoveAt(0);
                //La activamos
                newObject.SetActive(true);
                //Seteamos su transform
                newObject.transform.position = position;
                newObject.transform.rotation = rotation;
            }
        }

        //Si no había ningún objeto de este tipo en el pool de objetos desactivados entonces tendremos que crearlo
        if (!newObject)
        {
            //Por tanto creamos uno nuevo
            newObject = GameObject.Instantiate(prefab, position, rotation) as GameObject;
            //Le damos un nombre unico
            newObject.name = prefab.name.Split('_')[0] + "_" + m_nextID++;
            //Y lo metemos como hijo del gameObject que representa la escena
            newObject.transform.parent = Managers.sceneMgr.getRootScene().transform;
        }

        return newObject;
    }

    //Función que simplemente manda al pool de objetos desactivados este objeto
    public void destroyGameObject(GameObject prefab, bool destroy)
    {
        if (!destroy)
        {
            //Lo primero que tenemos que hacer es obtener el nombre del prefab original
            string originalPrefabName = prefab.name.Split('_')[0];

            //Desactivo el objeto de la escena
            prefab.SetActive(false);

            //Miramos si existe un registro de este objeto en el pool
            if (!m_poolDeactivatedObjects.ContainsKey(originalPrefabName))
            {
                List<GameObject> newList = new List<GameObject>();
                newList.Add(prefab);
                m_poolDeactivatedObjects.Add(originalPrefabName, newList);
            }
            else
            {
                m_poolDeactivatedObjects[originalPrefabName].Add(prefab);
            }
        }
        else
        {
            DestroyObject(prefab);
        }
    }

    //Funcion que vacía todo el pool de objetos desactivados. 
    private void clearPool()
    {
        foreach (List<GameObject> listObject in m_poolDeactivatedObjects.Values)
        {
            foreach (GameObject go in listObject)
            {
                Destroy(go);
            }
            listObject.Clear();
        }
        m_poolDeactivatedObjects.Clear();
    }

    //Funcion que instancia todos los objetos que mandamos instanciar al principio de todo. 
    public void instanciateInitialObjects(prebuildObjects initialObjects)
    {
        //Limpiamos el pool de objetos desactivados para la nueva escena
        clearPool();

        
        
        //Obtenemos cada elemento de la lista
        foreach( prebuildObjects.initialObjects cacheInitial in initialObjects.cacheInitialObjects)
        {
            List<GameObject> listObjects = new List<GameObject>();
            //Obtenemos el número de gameObjects que hemos definido desde el Inspector a instanciar
            for (int i = 0; i < cacheInitial.size; ++i)
            {
                //Creamos el nuevo gameObject
                GameObject go = GameObject.Instantiate(cacheInitial.prefab, Vector3.zero , Quaternion.identity) as GameObject;
                //Le asignamos un nombre unico
                go.name = cacheInitial.prefab.name + "_" + m_nextID++;
                //Lo desactivamos de la escena
                go.SetActive(false);
                //Lo metemos en la lista de este tipo de objetos, para despues meterlos en el pool
                listObjects.Add(go);
                //Ponemos como padre de este objeto al gameObject que representa la escena. 
                go.transform.parent = Managers.sceneMgr.getRootScene().transform;
            }
           
            //Metemos en el diccionario de objetos desactivados la nueva lista de objetos del mismo tipo, identificado por su clave, el nombre del prefab. 
            m_poolDeactivatedObjects.Add(cacheInitial.prefab.name, listObjects);
        }
    }
}
