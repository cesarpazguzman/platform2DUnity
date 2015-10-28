using UnityEngine;
using System.Collections;

//Componente que esta en el gameObject vacio que representa al menu
public class createManagers : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        //Con esta funcionalidad lo que hago es crear el gameObject Managers, el cual representa todos los managers del juego
        //y hacer que sólo se cree una única vez en toda la partida, llamando a DonDestroyOnLoad. 
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        if (managers == null)
        {
            managers = GameObject.Instantiate(Resources.Load("Prefabs/GamePrefabs/Managers") as GameObject);
            managers.name = "Managers";
            DontDestroyOnLoad(managers); 
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
