using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class functionsMenus : MonoBehaviour
{
    //Cuando pulsamos a "PLAY" cargamos el primer nivel del juego
    public void StartLevel()
    {
        Application.LoadLevel(1);
    }

    //Cuando pulsamos "TOP SCORES" pasamos a las opciones del juego
    public void TopScores()
    {

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

    //Cuando pulsamos "RESUME" volvemos al juego
    public void ResumeLevel()
    {
        Managers.inputMgr.pauseResumeGame();
    }

    //Cuando pulsamos "SETTINGS" pasamos a las opciones del juego
    public void Settings()
    {

    }

    //Cuando pulsamos "RESTART LEVEL" volvemos a cargar el prefab del nivel en el que nos encontramos y con el tiempo inicial
    public void RestartLevel()
    {

    }

    //Cuando pulsamos "EXIT TO MAIN MENU" salimos al menu principal
    public void ReturnMenu()
    {
        Application.LoadLevel(0);
        Managers.inputMgr.pauseResumeGame(); //Lo hago pues sigue recordando que la escena estaba en pausa pro alguna extraña razon
    }
}