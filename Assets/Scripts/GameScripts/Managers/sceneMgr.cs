using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manager que gestiona la carga y descarga de niveles. Los niveles son ficheros XML tendrá que cargar el managers
/// cada vez que acabemos un nivel nuevo. Tambien gestiona los menús. Los menús en este juego al ser tan sencillos
/// los hicimos por prefabs (excepto menú principal y gameOver que acaba con la escena de juego), por lo que mantenemos
/// un estado del menu en el que estamos o de si estamos jugando, mediante una pila de estados del juego (gaming, pause, completeLevel, settings)
/// 
/// Ademas, dado que los niveles son independientes, cada nivel va a tener diferentes prefabs, dependiendo de la zona de la montaña en la que se encuentre
/// nuestro personaje, no es necesario almacenar y gastar en memoria elementos de niveles anteriores, que igual ni siquiera volveremos a jugar.
/// Por lo que en el spawner simplemente se conservan los elementos comunes como las flores, arbustos. Y se conservan tambien cuando le damos a restart
/// en el mismo nivel, para no tener que cargarlo de nuevo.
/// </summary>
public class sceneMgr : MonoBehaviour {

    //VARIABLES RELATIVAS A LOS NIVELES DEL JUEGO
    [SerializeField]
    private List<string> m_levelsInGame;

    private int m_currentLevel;
    private GameObject m_currentLevelGameObject;

    private createLevelFromXML createLevel;


    //VARIABLES RELATIVAS A LOS MENUS DEL JUEGO, EXCEPTO EL PRINCIPAL Y EL GAMEOVER QUE SON ESCENAS

    //Enumerado que representa los distintos estados
    public enum states { game, pause, completeLevel, settings, gameOver, nulo };

    //Pila para almacenar los menus que están actualmente activos. Almacena los estados durante la escena game
    private Stack<states> m_statesGame = new Stack<states>();
    //Diccionario con clave nombre del menu, y valor el gameObject que representa el menu
    private Dictionary<states, GameObject> m_dictionaryMenus = new Dictionary<states, GameObject>();
    //Lista de prefabs de menus
    private Dictionary<states, GameObject> m_prefabsMenus = new Dictionary<states, GameObject>();
    
    

    private Vector3 m_spawnPointPlayer;
    public Vector3 spawnPointPlayer
    {
        set
        {
            m_spawnPointPlayer = value;
        }
    }

    //Propiedad que me indica el actual nivel en el que estoy
    public int getCurrentLevel
    {
        get { return m_currentLevel; }
        set { m_currentLevel = value; }
    }

   

	// Use this for initialization
    void Awake()
    {
        m_currentLevel = 0;

        createLevel = new createLevelFromXML();
        
    }

	void Start () 
    {
        //Inicializamos los prefabs de los menus
        m_prefabsMenus.Add(states.pause, Resources.Load("Menu Pausa") as GameObject);
        m_prefabsMenus.Add(states.completeLevel, Resources.Load("MenuLevelComplete") as GameObject);
        m_prefabsMenus.Add(states.gameOver, Resources.Load("gameOver") as GameObject);

        //Inicializamos el dicciarion que contendrá los estados de menús junto con el menú activado o desactivado en la escena
        m_dictionaryMenus.Add(states.pause, null);
        m_dictionaryMenus.Add(states.completeLevel, null);
        m_dictionaryMenus.Add(states.settings, null);
        m_dictionaryMenus.Add(states.gameOver, null);
	}

    void OnEnable()
    {
        gameMgr.restartGame += clearStackStates;
        gameMgr.restartGame += restartGame;
    }

    void OnDisable()
    {
        gameMgr.restartGame -= clearStackStates;
        gameMgr.restartGame -= restartGame;
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

    //Esta función simplemente será llamada cuando volvamos al menú principal.
    public void restartGame()
    {
        m_currentLevel = 0;
        m_currentLevelGameObject = null;
    }

    public void nextLevel()
    {
        if (m_currentLevelGameObject != null)
        {
            Managers.spawnerMgr.destroyGameObject(m_currentLevelGameObject, true);
            //Nuevo nivel
            ++m_currentLevel;
        }

        //Instanciamos un nuevo nivel
        m_currentLevelGameObject = createLevel.generateLevel(m_levelsInGame[m_currentLevel]);

        //Volvemos a desactivar la pausa previa del menu de fin de nivel
        Time.timeScale = 1;

        //Esto lo hago para que empiece con velocidad 0, dado que si que no lo pongo, empezará con la velocidad con la que se acabó en el nivel anterior
        Managers.gameMgr.getPlayer.GetComponent<playerController>().newLevel();

        //Spawneo al player en la posicion indicada en el nivel
        Managers.gameMgr.getPlayer.transform.position = m_spawnPointPlayer;
    }

    //Funcion que me devuelve el numero de niveles que tiene el juego
    public int lastLevel()
    {
        return m_levelsInGame.Count;
    }

    

    //Funcion llamada cuando se reinicia el juego.
    public void restartLevel()
    {
        GameObject player = Managers.gameMgr.getPlayer;

        //Ponemos primero al player como hijo de la escena principal, ya que puede ocurrir que eliminemos el nivel
        //con el player siendo hijo de una plataforma movible, eliminado tambien al player. De esta forma evitamos eso.
        player.transform.parent = getRootScene().transform;


        //Para no tener que cargar de nuevo los elementos que forman este nivel, lo que hago es guardarlos en el spawnerMgr, para que al cargar
        //de nuevo el nivel, no haga falta instanciarlos de nuevo
        Transform[] children = m_currentLevelGameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length && children[i].transform.parent == m_currentLevelGameObject.transform; ++i)
        {
            children[i].gameObject.transform.parent = getRootScene().transform;
            Managers.spawnerMgr.destroyGameObject(children[i].gameObject, false);
        }

        //Borramos el nivel de cero para volver a instanciarlo con los objetos "comida" de nuevo
        Managers.spawnerMgr.destroyGameObject(m_currentLevelGameObject, true);

        //Instanciamos el mismo nivel
        m_currentLevelGameObject = createLevel.generateLevel(m_levelsInGame[m_currentLevel]);

        //Volvemos a desactivar la pausa previa del menu de fin de nivel
        Time.timeScale = 1;

        //Esto lo hago para que empiece con velocidad 0, dado que si que no lo pongo, empezará con la velocidad con la que se acabó en el nivel anterior
        player.GetComponent<playerController>().newLevel();

        //Spawneo al player en la posicion indicada en el nivel
        player.transform.position = m_spawnPointPlayer;
    }


    //Funciones relativas al estado de juego (game, pause, completeLevel, settings).
    public void addState(states state)
    {
        //Metemos el estado nuevo en la pila de estados activados
        m_statesGame.Push(state);

        //Si el diccionario contiene este estado
        if (m_dictionaryMenus.ContainsKey(state))
        {
            //Si el valor no es nulo, lo que hago es activar el gameObject en la escena
            if (m_dictionaryMenus[state] != null)
            {
                m_dictionaryMenus[state].SetActive(true);
            }
            else
            {
                //sino lo creo
                m_dictionaryMenus[state] = Managers.spawnerMgr.createGameObject(m_prefabsMenus[state], Vector3.zero, Quaternion.identity);
            }
        }
    }

    public void removeCurrentState()
    {
        states nameState = m_statesGame.Pop();

        if (m_dictionaryMenus.ContainsKey(nameState))
        {
            m_dictionaryMenus[nameState].SetActive(false);
        }
    }

    public void clearStackStates()
    {
        m_statesGame.Clear();
    }

    public states getCurrentState()
    {
        if (m_statesGame.Count == 0)
        {
            return states.nulo;
        }
        return m_statesGame.Peek();
    }
}
