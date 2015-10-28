using UnityEngine;
using System.Collections;

/// <summary>
/// gestor de Managers. A través de esta clase estática podremos acceder a cualquiera de los managers desde cualquier parte del codigo facilmente
/// Por ejemplo, llamando desde un componente X para obtener el gameMgr: Managers.gameMgr.
/// Para esto, debe existir desde la primera escena el gameObject Managers, el cual con la llamada DontDestroyOnLoad hará que este objeto nunca sea
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
    private static inputMgr m_inputMgr = getManager<inputMgr>();
    private static timeManager m_timeMgr = getManager<timeManager>();

    //Propiedades de sólo lectura hacia afuera, de forma que nadie podrá modificar la referencia a los managers, salvo esta clase. 
    public static gameMgr gameMgr { get { return m_gameMgr; } }
    public static spawnerMgr spawnerMgr { get { return m_spawnerMgr; } }
    public static sceneMgr sceneMgr { get { return m_sceneMgr; } }
    public static eventMgr eventMgr { get { return m_eventMgr; } }
    public static inputMgr inputMgr { get { return m_inputMgr; } }
    public static timeManager timeMgr { get { return m_timeMgr; } }

    private static T getManager<T>() where T : MonoBehaviour
    {    
        //Si no existe el gameObject Managers, entonces quiere decir que no están los managers funcionando y salimos del juego. 
        if (!managersObject)
        {
            Debug.LogError("Objeto Managers no encontrado");
        }
        
        //Si están los managers funcionando entonces obtenemos el manager que pedimos.
        T component = managersObject.GetComponent<T>();

        //Comprobación de error. 
        if (!component)
        {
            Debug.LogError("Componente " + typeof(T) + " no encontrado");
        }

        return component;
    }
}
