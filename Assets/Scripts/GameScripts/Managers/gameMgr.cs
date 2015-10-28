using UnityEngine;
using System.Collections;

public class gameMgr : MonoBehaviour {

    //Variable que representa la puntuación del juego
    private int m_score = 0;

    //Referencia al player
    private GameObject player;
    public GameObject getPlayer
    {
        get 
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            return player; 
        }
    }

    //Evento al que subscribiremos las acciones a inicializar para volver a empezar una partida.
    public delegate void Restart();
    public static event Restart restartGame;

    void Awake()
    {
   
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        restartGame += initPuntuation;
    }

    void OnDisable()
    {
        restartGame -= initPuntuation;
    }

    //Funcion que se llama cuando finaliza el tiempo antes de que el player llegue a su objetivo
    public void gameOver()
    {
        Debug.Log("GAME OVER");
        //Paramos el juego
        Time.timeScale = 0;

        //Que aparezca un menú final con la puntuación obtenida, y que tengas las opciones de Restart y Exit Game


    }

    //Funcion que se llama cuando terminamos un nivel. Aquí pausaremos el juego, y aparecerá un menú con la puntuación obtenida. 
    public void finishLevel()
    {
        Debug.Log("NIVEL SUPERADO");

        //Paramos el juego
        Time.timeScale = 0;

        //Sumamos a la puntuación obtenida en el nivel, el tiempo restante 
        m_score += Managers.timeMgr.seconds;

        //Si no era el ultimo nivel
        if (Managers.sceneMgr.getCurrentLevel < Managers.sceneMgr.lastLevel())
        {
            //Que aparezca el menú de la puntuación al finalizar cada nivel

        }
        else
        {
            //Que aparezca un menú final con la puntuación obtenida, y que tengas las opciones de Restart y Exit Game

        }
    }

    private void initPuntuation()
    {
        m_score = 0;
        Time.timeScale = 1;
    }

    public void initGame()
    {
        //Llamamos a este evento para inicializar las puntuaciones por ejemplo, por si ya había jugado antes
        restartGame();

        //Llamamos al metodo de next level del sceneMgr para que se cargue el siguiente nivel, es decir, en este caso el primero
        Managers.sceneMgr.nextLevel();
    }
}


