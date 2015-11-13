using UnityEngine;
using System.Collections;

public class startLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Managers.GetInstance.GameMgr.initGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
