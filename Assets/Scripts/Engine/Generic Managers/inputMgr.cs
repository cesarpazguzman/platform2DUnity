using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inputMgr : MonoBehaviour
{

    public delegate void ReturnEnter();

    //Delegates. Key is the GameStateMgr.states and the value is the delegate with the callbacks
    private Dictionary<int, ReturnEnter> m_returnDelegates = new Dictionary<int, ReturnEnter>();
    private Dictionary<int, ReturnEnter> m_enterDelegates = new Dictionary<int, ReturnEnter>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Return"))
        {
            onReturn();
        }
        else if (Input.GetButtonDown("Enter"))
        {
            onEnter();
        }
    }

    public void RegisterReturn(gameStateMgr.states states, ReturnEnter returnEnter)
    {
        if (m_returnDelegates.ContainsKey((int)states))
        {
            m_returnDelegates[(int)states] += returnEnter;
        }
        else
        {
            m_returnDelegates.Add((int)states, returnEnter);
        }
    }

    public void DesRegisterReturn(gameStateMgr.states states, ReturnEnter returnEnter)
    {
        if (m_returnDelegates.ContainsKey((int)states))
        {
            m_returnDelegates[(int)states] -= returnEnter;
        }
    }

    public void RegisterEnter(gameStateMgr.states states, ReturnEnter returnEnter)
    {
        if (m_enterDelegates.ContainsKey((int)states))
        {
            m_enterDelegates[(int)states] += returnEnter;
        }
        else
        {
            m_enterDelegates.Add((int)states, returnEnter);
        }
    }

    public void DesRegisterEnter(gameStateMgr.states states, ReturnEnter returnEnter)
    {
        if (m_returnDelegates.ContainsKey((int)states))
        {
            m_enterDelegates[(int)states] -= returnEnter;
        }
    }

    void onReturn()
    {
        //If exist functions registred in the delegate indicated
        if (m_returnDelegates.ContainsKey(Managers.GetInstance.GameStateMgr.currentState))
        {
            //Execute theses funtions
            m_returnDelegates[Managers.GetInstance.GameStateMgr.currentState]();
        }
    }

    void onEnter()
    {
        //If exist functions registred in the delegate indicated
        if (m_enterDelegates.ContainsKey(Managers.GetInstance.GameStateMgr.currentState))
        {
            //Execute theses funtions
            m_enterDelegates[Managers.GetInstance.GameStateMgr.currentState]();
        }
    }
}
