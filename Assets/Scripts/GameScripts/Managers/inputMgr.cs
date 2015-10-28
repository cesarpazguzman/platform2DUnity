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

    private bool paused;
    private GameObject menuPausa;

	// Use this for initialization
	void Start () 
    {
        
	}

    void OnEnable()
    {
        paused = false;
    }

	// Update is called once per frame
	void Update () 
    {
        //Si estamos en la escena de juego y pulsamos la tecla de pause
        
        if ((Application.loadedLevelName == "game" || Application.loadedLevelName == "__game_Cesar") && Input.GetButtonDown("Pause"))
         {
             pauseResumeGame();           
         }
	}

    //Funcion llamada por la intervencion de la tecla de pause y quitar pause
    public void pauseResumeGame()
    {
        paused = !paused;
        Time.timeScale = (paused) ? 0.0f : 1.0f;

        if (paused)
        {
            //Que aparezca el menú de pausa.
            menuPausa = Managers.spawnerMgr.createGameObject(Resources.Load("Menu Pausa") as GameObject, Vector3.zero, Quaternion.identity);
        }
        else
        {
            //Destuimos el menú
            Managers.spawnerMgr.destroyGameObject(menuPausa, false);
        }
    }
}
