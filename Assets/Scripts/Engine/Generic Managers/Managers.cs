using UnityEngine;
using System.Collections;

public class Managers
{
    private static Managers m_instance = null;
    private GameObject m_managers = null;

    //Managers. Estaticos para que sean más fácilmente accesibles
    private gameMgr m_GameMgr = null;
    public gameMgr GameMgr { get { return m_GameMgr; } }
    private inputMgr m_InputMgr = null;
    public inputMgr InputMgr { get { return m_InputMgr; } }
    private spawnerMgr m_SpawnerMgr = null;
    public spawnerMgr SpawnerMgr { get { return m_SpawnerMgr; } }
    private gameStateMgr m_GameStateMgr = null;
    public gameStateMgr GameStateMgr { get { return m_GameStateMgr; } }
    private storageMgr m_StorageMgr = null;
    public storageMgr StorageMgr { get { return m_StorageMgr; } }
    private sceneMgr m_SceneMgr = null;
    public sceneMgr SceneMgr { get { return m_SceneMgr; } }
    private timeManager m_TimeMgr = null;
    public timeManager TimeMgr { get { return m_TimeMgr; } }

    //Constructor
    private Managers()
    {
        if (m_managers == null)
        {
            m_managers = GameObject.FindGameObjectWithTag("Managers");
          
            if (m_managers == null)
            {
                //Create the gameObject
                m_managers = GameObject.Instantiate(Resources.Load("Prefabs/GamePrefabs/Managers")) as GameObject;
                m_managers.name = "Managers";

                //If the managers is not created, then ERROR
                Assert.assert(m_managers != null, "Error creating the Managers");
            }

            m_GameMgr = m_managers.GetSafeComponent<gameMgr>();
            m_InputMgr = m_managers.GetSafeComponent<inputMgr>();
            m_SpawnerMgr = m_managers.GetSafeComponent<spawnerMgr>();
            m_GameStateMgr = m_managers.GetSafeComponent<gameStateMgr>();
            m_StorageMgr = m_managers.GetSafeComponent<storageMgr>();
            m_SceneMgr = m_managers.GetSafeComponent<sceneMgr>();
            m_TimeMgr = m_managers.GetSafeComponent<timeManager>();

            //This object is not destroyed between scenes
            GameObject.DontDestroyOnLoad(m_managers);
        }
    }

    //Property of m_instance
    public static Managers GetInstance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new Managers();
            }
            return m_instance;
        }
    }
}
