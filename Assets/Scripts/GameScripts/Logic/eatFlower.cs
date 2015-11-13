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
            Managers.GetInstance.GameMgr.finishLevel();
            //Destruimos la flor. La ponemos como hija de la escena, ya que va a ser reutilizada y no queremos que al destruir el nivel
            //tambien se destruya la flor   
            Managers.GetInstance.SpawnerMgr.destroyGameObject(this.gameObject);
        }
    }
}
