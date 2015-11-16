using UnityEngine;
using System.Collections;

public class gameMgr : MonoBehaviour {

    //Variable que representa la puntuación del juego
    private int m_score = 0;

    //Referencia al player. Este será el único punto desde el cual podemos obtener al player
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

    public int levelToStart;

	// Use this for initialization
	void Start () {
	    
	}

    /// <summary>
    /// Funcion que se llama cuando finaliza el tiempo antes de que el player llegue a su objetivo. En este juego no es necesario tener una escena
    /// gameOver, simplemente un aviso que indica que la cabra ha muerto, con la opción de Restart el nivel. Si fuera un roguelike donde mueres y
    /// empieza de nuevo el juego, entonces si haría falta una escena aparte. 
    /// </summary>
    public void gameOver()
    {
        //Que aparezca un menú final con la puntuación obtenida, y que tengas las opciones de Restart y Exit Game
        Managers.GetInstance.GameStateMgr.gameOver();
    }

    //Metodo usado cuando el usuario vuelve al menu principal saliendo de la escena de juego.
    public void exitGame()
    {
        restartGame();
    }

    //Funcion que se llama cuando terminamos un nivel. Aquí pausaremos el juego, y aparecerá un menú con la puntuación obtenida. 
    public void finishLevel()
    {
        //Que aparezca el menú de la puntuación al finalizar cada nivel
        Managers.GetInstance.GameStateMgr.finishLevel();
    }

    public void initGame()
    {
        //Llamamos al metodo de next level del sceneMgr para que se cargue el siguiente nivel, es decir, en este caso el primero
        Managers.GetInstance.SceneMgr.startLevel(levelToStart);
    }


}


