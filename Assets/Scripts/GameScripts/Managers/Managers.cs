using UnityEngine;
using System.Collections;

/// <summary>
/// gestor de Managers. A través de esta clase estática podremos acceder a cualquiera de los managers desde cualquier parte del codigo facilmente
/// Por ejemplo, llamando desde un componente X para obtener el gameMgr: Managers.gameMgr.
/// Para esto, debe existir desde la primera escena el gameObject prefabs, el cual con la llamada DontDestroyOnLoad hará que este objeto nunca sea
/// destruido, y poder así seguir con los managers intactos a medida que avanzamos en los niveles. Esta llamada se realiza en el GameMgr, tiene que
/// hacerse en un componente, y he decidido que sea en este. 
/// </summary>
public static class Managers
{
    private static GameObject managersObject = GameObject.FindGameObjectWithTag("Managers");
    private static gameMgr m_gameMgr = getManager<gameMgr>();
    private static spawnerMgr m_spawnerMgr = getManager<spawnerMgr>();
    private static sceneMgr m_sceneMgr = getManager<sceneMgr>();
    private static eventMgr m_eventMgr = getManager<eventMgr>();

    //Propiedades de sólo lectura hacia afuera, de forma que nadie podrá modificar la referencia a los managers, salvo esta clase. 
    public static gameMgr gameMgr { get { return m_gameMgr; } }
    public static spawnerMgr spawnerMgr { get { return m_spawnerMgr; } }
    public static sceneMgr sceneMgr { get { return m_sceneMgr; } }
    public static eventMgr eventMgr { get { return m_eventMgr; } }

    private static T getManager<T>() where T : Component
    {    
        //Si no existe el gameObject Managers, entonces quiere decir que no están los managers funcionando y salimos del juego. 
        if (!managersObject)
        {
            Application.Quit();
        }

        //Si están los managers funcionando entonces obtenemos el manager que pedimos.
        return managersObject.GetComponent<T>();
    }
}
