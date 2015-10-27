using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pauseMenu : MonoBehaviour
{
    public Button resumeText;
    public Button settingsText;
    public Button restartText;
    public Button exitText;

    public inputMgr isPaused;

    void Start()
    {
        resumeText = resumeText.GetComponent<Button>();
        settingsText = settingsText.GetComponent<Button>();
        restartText = restartText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        GameObject iM = GameObject.Find("Managers");
        isPaused = iM.GetComponent<inputMgr>();

    }

    //Cuando pulsamos "RESUME" volvemos al juego
    public void ResumeLevel()
    {
        isPaused.paused = false;
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
    public void ExitGame()
    {
        Application.LoadLevel(0);
        isPaused.paused = false; //Lo hago pues sigue recordando que la escena estaba en pausa pro alguna extraña razon
    }
}