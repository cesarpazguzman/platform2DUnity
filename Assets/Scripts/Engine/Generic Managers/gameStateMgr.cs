using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameStateMgr : MonoBehaviour
{

    //Prefab menu pause
    public GameObject menuPause;
    public GameObject menuCompletLevel;
    public GameObject menuGameOver;

    private Dictionary<int, GameObject> m_menuDictionary = new Dictionary<int, GameObject>();

    public enum states { MainMenu, Game, GameOver, Pause, CompleteLevelMenu };
    public states initialState;

    //Pila de estados actuales
    private Stack<states> m_states = new Stack<states>();

    private int m_currentState = -1;
    //Property 
    public int currentState { get { return m_currentState; } }

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        m_menuDictionary.Add((int)states.Pause, null);
        m_menuDictionary.Add((int)states.CompleteLevelMenu, null);
        m_menuDictionary.Add((int)states.GameOver, null);

        //Set the initialState like current state
        setState(initialState);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        //Register the callbacks when ESC is pressed
        Managers.GetInstance.InputMgr.RegisterReturn(states.MainMenu, exitGame);
        Managers.GetInstance.InputMgr.RegisterReturn(states.Game, pause);
        Managers.GetInstance.InputMgr.RegisterReturn(states.Pause, reanude);
        Managers.GetInstance.InputMgr.RegisterReturn(states.GameOver, exitGame);
        

        //Register the callbacks when Enter is pressed
        Managers.GetInstance.InputMgr.RegisterEnter(states.MainMenu, startGame);
        Managers.GetInstance.InputMgr.RegisterEnter(states.GameOver, startGame);
        Managers.GetInstance.InputMgr.RegisterEnter(states.CompleteLevelMenu, nextLevel);
    }

    void OnDisable()
    {
        //Desregister the callbacks when ESC is pressed
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.MainMenu, exitGame);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.Game, pause);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.Pause, reanude);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.GameOver, exitGame);

        //Desregister the callbacks when Enter is pressed
        Managers.GetInstance.InputMgr.DesRegisterEnter(states.MainMenu, startGame);
        Managers.GetInstance.InputMgr.DesRegisterEnter(states.GameOver, startGame);
        Managers.GetInstance.InputMgr.DesRegisterEnter(states.CompleteLevelMenu, nextLevel);
    }

    public void setState(states state)
    {
        StartCoroutine(changeState(state, true));
    }

    public void pushState(states state)
    {
        StartCoroutine(changeState(state));
    }

    public void popState()
    {
        StartCoroutine(popStateCoroutine());

    }

    private IEnumerator popStateCoroutine()
    {
        //Wait for end frame in order to prevent problems during this frame
        yield return new WaitForEndOfFrame();

        m_states.Pop();

        m_currentState = (int)m_states.Peek();
    }

    private IEnumerator changeState(states state, bool clear = false)
    {
        //Wait for end frame in order to prevent problems during this frame
        yield return new WaitForEndOfFrame();

        if (clear)
        {
            m_states.Clear();
        }

        m_states.Push(state);

        m_currentState = (int)m_states.Peek();
    }

    //Fuctions for the transition between the states
    public void pause()
    {
        if (currentState != (int)states.Pause)
        {
            //Create/Activate the gameObject menuPause
            m_menuDictionary[(int)states.Pause] = Managers.GetInstance.SpawnerMgr.createGameObject(menuPause);

            //Push the new state Pause
            pushState(states.Pause);
        }
    }

    public void reanude()
    {
        //Deactivate the gameObject menuPause
        Managers.GetInstance.SpawnerMgr.destroyGameObject(m_menuDictionary[(int)states.Pause]);

        //Remove the state pause
        popState();
    }

    public void startGame()
    {
        //Load the scene game
        Application.LoadLevel(1);
        setState(states.Game);
    }

    public void exitGame()
    {
        //Exit the application
        Application.Quit();
    }

    public void nextLevel()
    {
        if (!Managers.GetInstance.SceneMgr.isLastLevel)
        {
            //Cargamos un nuevo nivel
            Managers.GetInstance.SpawnerMgr.destroyGameObject(m_menuDictionary[(int)states.CompleteLevelMenu]);
            Managers.GetInstance.SceneMgr.nextLevel();
            popState();
        }
    }

    public void finishLevel()
    {
        m_menuDictionary[(int)states.CompleteLevelMenu] = Managers.GetInstance.SpawnerMgr.createGameObject(menuCompletLevel);
        pushState(gameStateMgr.states.CompleteLevelMenu);
    }

    public void restartLevel()
    {
        //Como la accion de reiniciar un nivel solo se puede hacer desde un menu, lo que hago es acceder al menu actual y destruirlo
        if (m_menuDictionary.ContainsKey(m_currentState))
        {
            Managers.GetInstance.SpawnerMgr.destroyGameObject(m_menuDictionary[m_currentState]);
        }
        setState(gameStateMgr.states.Game);    
    }

    public void gameOver()
    {
        m_menuDictionary[(int)states.GameOver] = Managers.GetInstance.SpawnerMgr.createGameObject(menuGameOver);
        pushState(gameStateMgr.states.GameOver);
    }
}
