using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class functionsMenus : MonoBehaviour
{
    //MENU PRINCIPAL

    //Cuando pulsamos a "PLAY" cargamos el primer nivel del juego
    public void StartLevel()
    {
        Application.LoadLevel(1);
    }

    //Cuando pulsamos "CREDITS" pasamos a las opciones del juego
    public void Credits()
    {

    }

    //Cuando pulsamos "EXIT" en el menú, salimos al escritorio
    public void ExitGame()
    {
        Application.Quit(); //this will quit our game. Note this will only work after building the game

    }


    //MENU DE PAUSA

    //Cuando pulsamos "RESUME" volvemos al juego
    public void ResumeLevel()
    {
        //Volvemos al estado de juego
        Time.timeScale = 1.0f;

        //Eliminamos el estado de pausa
        Managers.sceneMgr.removeCurrentState();
    }

    //Cuando pulsamos "SETTINGS" pasamos a las opciones del juego
    public void Settings()
    {

    }

    //Cuando pulsamos "RESTART LEVEL" volvemos a cargar el prefab del nivel en el que nos encontramos y con el tiempo inicial
    public void RestartLevel()
    {
        //Hacemos desaparecer el menu de nivel completado
        Managers.sceneMgr.removeCurrentState();

        //Restart del nivel
        Managers.sceneMgr.restartLevel();
    }

    //Cuando pulsamos "EXIT TO MAIN MENU" salimos al menu principal
    public void ReturnMenu()
    {
        //Cargamos el menú principal
        Application.LoadLevel(0);

        Managers.gameMgr.exitGame();
    }

    //MENU COMPLETE LEVEL
    public void nextLevel()
    {
        //Cargamos un nuevo nivel
        Managers.sceneMgr.nextLevel();  

        //Hacemos desaparecer el menu de nivel completado
        Managers.sceneMgr.removeCurrentState();     
    }
}