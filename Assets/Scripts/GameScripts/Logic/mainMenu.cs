using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenu : MonoBehaviour
{
    public Button playText;
    public Button settingsText;
    public Button topScores;
    public Button credits;
    public Button exitText;

    void Start()

    {
        playText = playText.GetComponent<Button>();
        settingsText = settingsText.GetComponent<Button>();
        topScores = topScores.GetComponent<Button>();
        credits = credits.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

    }

    //Cuando pulsamos a "PLAY" cargamos el primer nivel del juego
    public void StartLevel()

    {
        Application.LoadLevel(1);

    }

    //Cuando pulsamos "SETTINGS" pasamos a las opciones del juego
    public void Settings()
    {

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

}