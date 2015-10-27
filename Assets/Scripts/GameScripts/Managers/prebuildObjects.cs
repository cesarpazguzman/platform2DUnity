using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Esta clase sirve para definir qué objetos se instanciarán y cuántos de cada tipo. Hacemos esto al principio para intentar optimizar un poco más
/// y no hacer creaciones de objetos nuevos durante el juego. Este componente se colocará en cada objeto que represente una escena nueva, para 
/// simplemente cargar los objetos que necesitemos en esa escena. Es decir, en los prefabs que representan los niveles.
/// </summary>
public class prebuildObjects : MonoBehaviour {

    [System.Serializable]
    public class initialObjects
    {
        public GameObject prefab;
        public short size;
    }
    public List<initialObjects> cacheInitialObjects;

    void OnEnable() 
    {
        Managers.spawnerMgr.instanciateInitialObjects(this);
	}
    
}
