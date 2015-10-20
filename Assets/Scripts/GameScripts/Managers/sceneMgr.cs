using UnityEngine;
using System.Collections;

/// <summary>
/// Manager que gestiona la carga y descarga de escenas. Mantendrá una pila, para indicar las escenas desactivadas, por ejemplo, cuando pulsemos pause
/// se desactivará la escena de juego y ésta se mantendrá en la pila, de forma que cuando salgamos del pause, podemos volver al mismo estado de juego
/// de antes. No vamos a considerar a cargar la escenas de forma asincrona, al menos por ahora. Cuando avancemos en el juego, y veamos que vamos bien de 
/// tiempo se puede mirar. 
/// </summary>
public class sceneMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
