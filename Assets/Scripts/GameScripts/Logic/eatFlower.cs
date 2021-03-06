﻿using UnityEngine;
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
        //Solo se destruye si es el Player quien la coge y si estamos dentro del limite de comida permitido
        if (coll.gameObject.CompareTag("Player") && Managers.GetInstance.TimeMgr.getPercent()>0)
        {     
            //Llamos al metodo finalizar nivel del gameMgr. 
            Managers.GetInstance.GameMgr.finishLevel();
            //Destruimos la flor. La ponemos como hija de la escena, ya que va a ser reutilizada y no queremos que al destruir el nivel tambien se destruya    
            Managers.GetInstance.SpawnerMgr.destroyGameObject(this.gameObject);
        }
    }
}
