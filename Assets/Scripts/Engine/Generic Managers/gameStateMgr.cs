using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameStateMgr : MonoBehaviour
{

    //Prefab menu pause
    public GameObject menuPause;
    public GameObject menuCompletLevel;
    public GameObject menuGameOver;
    public GameObject menuLevels;

    private Dictionary<int, GameObject> m_menuDictionary = new Dictionary<int, GameObject>();

    public enum states { MainMenu, Game, GameOver, Pause, CompleteLevelMenu, Levels};
    public states initialState;

    //Pila de estados actuales
    private Stack<states> m_states = new Stack<states>();

    private int m_currentState = -1;
    //Property 
    public int currentState { get { return m_currentState; } }

    public bool isGame;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        m_menuDictionary.Add((int)states.Pause, null);
        m_menuDictionary.Add((int)states.CompleteLevelMenu, null);
        m_menuDictionary.Add((int)states.GameOver, null);
        m_menuDictionary.Add((int)states.Levels, null);

        //Set the initialState like current state
        setState(initialState);

        isGame = false;
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
        Managers.GetInstance.InputMgr.RegisterReturn(states.Pause, popState);
        Managers.GetInstance.InputMgr.RegisterReturn(states.GameOver, exitGame);
        Managers.GetInstance.InputMgr.RegisterReturn(states.Levels, popState);

        //Register the callbacks when Enter is pressed
        Managers.GetInstance.InputMgr.RegisterEnter(states.MainMenu, showMenuLevels);
    }

    void OnDisable()
    {
        //Desregister the callbacks when ESC is pressed
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.MainMenu, exitGame);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.Game, pause);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.Pause, popState);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.GameOver, exitGame);
        Managers.GetInstance.InputMgr.DesRegisterReturn(states.Levels, popState);

        //Desregister the callbacks when Enter is pressed
        Managers.GetInstance.InputMgr.DesRegisterEnter(states.MainMenu, showMenuLevels);
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
        if(m_menuDictionary.ContainsKey((int)m_states.Peek()))
        {
            Managers.GetInstance.SpawnerMgr.destroyGameObject(m_menuDictionary[(int)m_states.Peek()]);
        }

        m_states.Pop();

        m_currentState = (int)m_states.Peek();
    }

    private IEnumerator changeState(states state, bool clear = false)
    {
        //Wait for end frame in order to prevent problems during this frame
        yield return new WaitForEndOfFrame();

        if (clear)
        {   
            foreach (states st in m_states)
            {
                if(m_menuDictionary.ContainsKey((int)st))
                {
                    Managers.GetInstance.SpawnerMgr.destroyGameObject(m_menuDictionary[(int)st]);
                }
            }
            m_states.Clear();
            isGame = (state == states.Game) ? true : false;
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

    public void startLevel(int level)
    {
        setState(states.Game);
        if (isGame)
        {    
            //Empezamos el nuevo nivel
            Managers.GetInstance.SceneMgr.startLevel(level);
        }
        else
        {
            //Load the scene game
            Application.LoadLevel(1);
            Managers.GetInstance.GameMgr.levelToStart = level;
        }    
    }

    public void exitGame()
    {
        Managers.GetInstance.StorageMgr.writeFile();
        //Exit the application
        Application.Quit();
    }

    public void finishLevel()
    {
        m_menuDictionary[(int)states.CompleteLevelMenu] = Managers.GetInstance.SpawnerMgr.createGameObject(menuCompletLevel);
        pushState(gameStateMgr.states.CompleteLevelMenu);
    }

    public void restartLevel()
    {
        setState(gameStateMgr.states.Game);    
    }

    public void gameOver()
    {
        m_menuDictionary[(int)states.GameOver] = Managers.GetInstance.SpawnerMgr.createGameObject(menuGameOver);
        pushState(gameStateMgr.states.GameOver);
    }

    public void showMenuLevels()
    {
        m_menuDictionary[(int)states.Levels] = Managers.GetInstance.SpawnerMgr.createGameObject(menuLevels);
        m_menuDictionary[(int)states.Levels].GetSafeComponent<scoreLevels>().showScore();
        pushState(gameStateMgr.states.Levels);
    }
}
