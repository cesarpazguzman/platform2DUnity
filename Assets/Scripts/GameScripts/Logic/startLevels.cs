using UnityEngine;
using System.Collections;

public class startLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Start startLevel");
        Managers.gameMgr.initGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
