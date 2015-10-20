using UnityEngine;
using System.Collections;

public class gameMgr : MonoBehaviour {

    void Awake()
    {
        //When loading a new level all objects in the scene are destroyed except the gameObject which contains the Managers. 
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


