using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manager que gestiona la carga y descarga de escenas. Mantendrá una pila, para indicar las escenas desactivadas, por ejemplo, cuando pulsemos pause
/// se desactivará la escena de juego y ésta se mantendrá en la pila, de forma que cuando salgamos del pause, podemos volver al mismo estado de juego
/// de antes. No vamos a considerar a cargar la escenas de forma asincrona, al menos por ahora. Cuando avancemos en el juego, y veamos que vamos bien de 
/// tiempo se puede mirar. 
/// </summary>
public class sceneMgr : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_levelsInGame;

    private int m_currentLevel;

    //Propiedad que me indica el actual nivel en el que estoy
    public int getCurrentLevel
    {
        get { return m_currentLevel; }
    }

    //Funcion que me devuelve el numero de niveles que tiene el juego
    public int lastLevel()
    {
        return m_levelsInGame.Count;
    }

	// Use this for initialization
	void Start () 
    {
        //Marcamos como primer nivel, el indicado primero en el Inspector
        m_currentLevel = 0;

        Managers.spawnerMgr.createGameObject(m_levelsInGame[m_currentLevel], Vector3.zero, Quaternion.identity);
	}

    void OnEnable()
    {
        //Registramos en el evento de cuando se reinicia el juego esta funcion
        gameMgr.restartGame += restartLevel;
    }

    void OnDisable()
    {    
        gameMgr.restartGame -= restartLevel;
    }

	// Update is called once per frame
	void Update () 
    {
	
	}

    //Funcion que devuelve el objeto vacio que representa la escena actual
    public GameObject getRootScene()
    {
        return GameObject.Find(Application.loadedLevelName);
    }

    public void nextLevel()
    {
        
        //Destruimos el nivel recien pasado.
        Managers.spawnerMgr.destroyGameObject(m_levelsInGame[m_currentLevel], true);

        //Nuevo nivel
        ++m_currentLevel;

        //Instanciamos un nuevo nivel
        Managers.spawnerMgr.createGameObject(m_levelsInGame[m_currentLevel], Vector3.zero, Quaternion.identity);

        //Volvemos a desactivar la pausa previa del menu de fin de nivel
        Time.timeScale = 1;

        //La posicion del player se cambiara a una variable global posteriormente.
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(3, 3, 0);
    }

    public GameObject getLevelGameObject()
    {
        return m_levelsInGame[m_currentLevel];
    }

    //Funcion llamada cuando se reinicia el juego.
    public void restartLevel()
    {
        m_currentLevel = 0;
    }
}
