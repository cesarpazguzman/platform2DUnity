using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que gestiona los eventos de entrada. Como hacer click sobre el juego, saber que respuesta tendrá según sobre lo que se haga click, generalmente sobre elementos UI.
/// Un ejemplo sencillo, cuando se hace click sobre el botón Start. O incluso al darle a Enter que represente el Start también. 
/// También por ejemplo, cuando pulsemos la tecla de pause, para que se cargue la nueva escena encima de la del juego y se pare todo. Es decir,
/// eventos de entrada, bien sobre elementos UI, bien con atajos de teclado para retroceder sobre menús o hacer pause en el juego. 
/// </summary>
///
public class inputMgr : MonoBehaviour {

    private sceneMgr m_sceneMgr;


	// Use this for initialization
	void Start () 
    {
        m_sceneMgr = Managers.sceneMgr;
	}

	// Update is called once per frame
	void Update () 
    {
        //Si estamos en la escena de juego, en el estado de que estamos jugando y pulsamos la tecla de pause
        if (m_sceneMgr.getCurrentState() == sceneMgr.states.game && Input.GetButtonDown("Pause"))
        {
            //Pausamos el juego
            Time.timeScale = 0.0f;

            //Cambiamos el estado a Pause
            m_sceneMgr.addState(sceneMgr.states.pause);
        }
        //Si estamos en el estado de pausa, volvemos al estado de juego al pulsar el boton Return
        else if (m_sceneMgr.getCurrentState() == sceneMgr.states.pause && Input.GetButtonDown("Return"))
        {
            //Volvemos al estado de juego
            Time.timeScale = 1.0f;

            //Eliminamos el estado de pausa
            m_sceneMgr.removeCurrentState();
        }
	}

}
