﻿using UnityEngine;
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
    public List<string> levelsInGame { get { return m_levelsInGame; } }

    private int m_currentLevel;
    public int currentLevel { get { return m_currentLevel; } }
    private GameObject m_currentLevelGameObject = null;

    private createLevelFromXML createLevel;
    
    private Vector3 m_spawnPointPlayer;
    public Vector3 spawnPointPlayer
    {
        set
        {
            m_spawnPointPlayer = value;
        }
    }

    private GameObject m_rootScene = null;
    private int m_currentIndexLevel;
    public GameObject rootScene
    {
        get
        {
            //If change the scene, obtain a new rootScene
            if (m_rootScene == null || m_currentIndexLevel != Application.loadedLevel)
            {
                m_rootScene = GameObject.Find(Application.loadedLevelName);
                m_currentIndexLevel = Application.loadedLevel;
                Managers.GetInstance.SpawnerMgr.clearPool();
            }
            return m_rootScene;
        }
    }

    public delegate void m_newLevel();
    public static event m_newLevel newLevel;

    //Funcion que me devuelve el numero de niveles que tiene el juego
    public bool isLastLevel { get {return m_currentLevel == m_levelsInGame.Count-1; }}

	// Use this for initialization
    void Awake()
    {
        createLevel = new createLevelFromXML();

        m_currentIndexLevel = Application.loadedLevel;    
    }

	void Start () 
    {
	}

    void OnEnable()
    {
        gameMgr.restartGame += restartGame;
    }

    void OnDisable()
    {
        gameMgr.restartGame -= restartGame;
    }

	// Update is called once per frame
	void Update () 
    {
	
	}

    //Esta función simplemente será llamada cuando volvamos al menú principal.
    public void restartGame()
    {
        m_currentLevelGameObject = null;
    }

    public void startLevel(int level)
    {
        m_currentLevel = level - 1;
        if (m_currentLevelGameObject != null)
        {
            deactivateGameObjectsLevel();
            Managers.GetInstance.SpawnerMgr.destroyGameObject(m_currentLevelGameObject, true);
        }

        newLevel();

        //Instanciamos un nuevo nivel
        m_currentLevelGameObject = createLevel.generateLevel(m_levelsInGame[m_currentLevel]);

        //Esto lo hago para que empiece con velocidad 0, dado que si que no lo pongo, empezará con la velocidad con la que se acabó en el nivel anterior
        Managers.GetInstance.GameMgr.getPlayer.GetSafeComponent<playerController>().newLevel();

        //Spawneo al player en la posicion indicada en el nivel
        Managers.GetInstance.GameMgr.getPlayer.transform.position = m_spawnPointPlayer;
    }

    //Funcion llamada cuando se reinicia el juego.
    public void restartLevel()
    {
        GameObject player = Managers.GetInstance.GameMgr.getPlayer;

        //Ponemos primero al player como hijo de la escena principal, ya que puede ocurrir que eliminemos el nivel
        //con el player siendo hijo de una plataforma movible, eliminado tambien al player. De esta forma evitamos eso.
        player.transform.parent = rootScene.transform;

        //Para no tener que cargar de nuevo los elementos que forman este nivel, lo que hago es guardarlos en el spawnerMgr, para que al cargar
        //de nuevo el nivel, no haga falta instanciarlos de nuevo
        deactivateGameObjectsLevel();

        //Borramos el nivel de cero para volver a instanciarlo con los objetos "comida" de nuevo
        Managers.GetInstance.SpawnerMgr.destroyGameObject(m_currentLevelGameObject, true);

        //Instanciamos el mismo nivel
        m_currentLevelGameObject = createLevel.generateLevel(m_levelsInGame[m_currentLevel]);

        //Esto lo hago para que empiece con velocidad 0, dado que si que no lo pongo, empezará con la velocidad con la que se acabó en el nivel anterior
        player.GetSafeComponent<playerController>().newLevel();

        //Spawneo al player en la posicion indicada en el nivel
        player.transform.position = m_spawnPointPlayer;
    }

    public void deactivateGameObjectsLevel()
    {
        List<GameObject> goDeactivate = new List<GameObject>();

        //Dado que el gameObject que representa el nivel queremos eliminarlo, lo que hacemos es DESACTIVAR cada uno de sus hijos 
        //y ponerlo a nivel de la escena, por lo que debemos guardarlo primero en una lista para despues desactivarlos y quitarlos
        //del gameObject de la escena
        foreach (Transform child in m_currentLevelGameObject.transform)
        {
            goDeactivate.Add(child.gameObject);
        }

        foreach (GameObject go in goDeactivate)
        {
            Managers.GetInstance.SpawnerMgr.destroyGameObject(go);
        }
    }
}
