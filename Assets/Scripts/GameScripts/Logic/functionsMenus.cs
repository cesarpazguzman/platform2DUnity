﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class functionsMenus : MonoBehaviour
{
    private gameStateMgr m_gameStateMgr = null;
    void Awake()
    {
        m_gameStateMgr = Managers.GetInstance.GameStateMgr;
    }

    void Start()
    {

    }

    //Main Menu
    public void startGame()
    {
        m_gameStateMgr.startGame();
    }

    //Cuando pulsamos "CREDITS" pasamos a las opciones del juego
    public void Credits()
    {

    }

    //Cuando pulsamos "EXIT" en el menú, salimos al escritorio
    public void ExitGame()
    {
        m_gameStateMgr.exitGame();
    }


    //MENU DE PAUSA

    //Cuando pulsamos "RESUME" volvemos al juego
    public void ResumeLevel()
    {
        m_gameStateMgr.reanude();
    }

    //Cuando pulsamos "SETTINGS" pasamos a las opciones del juego
    public void Settings()
    {

    }

    //Cuando pulsamos "RESTART LEVEL" volvemos a cargar el prefab del nivel en el que nos encontramos y con el tiempo inicial
    public void RestartLevel()
    {
        //Restart del nivel
        Managers.GetInstance.SceneMgr.restartLevel();
        m_gameStateMgr.restartLevel();
    }

    //Cuando pulsamos "EXIT TO MAIN MENU" salimos al menu principal
    public void ReturnMenu()
    {
        Application.LoadLevel(0);
        m_gameStateMgr.setState(gameStateMgr.states.MainMenu);

        Managers.GetInstance.GameMgr.exitGame();
    }

    //MENU COMPLETE LEVEL
    public void nextLevel()
    {
        m_gameStateMgr.nextLevel();
    }
}