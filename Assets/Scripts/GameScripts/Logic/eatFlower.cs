using UnityEngine;
using System.Collections;

public class eatFlower : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //Llamos al metodo finalizar nivel del gameMgr. Falta de implementarlo todavía. 
            Managers.gameMgr.finishLevel();
            //Destruimos la flor
            Managers.spawnerMgr.destroyGameObject(this.gameObject, false);
        }
    }
}
