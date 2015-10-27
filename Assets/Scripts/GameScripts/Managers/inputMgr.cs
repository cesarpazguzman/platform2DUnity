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

    public bool paused;

	// Use this for initialization
	void Start () 
    {
        paused = false;

        
	}
	 
	// Update is called once per frame
	void Update () 
    {
        //Si estamos en la escena de juego y pulsamos la tecla de pause
        
        if (Application.loadedLevelName == "game" && Input.GetButtonDown("Pause"))
         {
             pauseGame();           
         }

        if (paused == false)
        {
            resumeGame();
        }
	}

    private void pauseGame()
    {
        paused = !paused;
        Time.timeScale = (paused) ? 0 : 1;

        //Que aparezca el menú de pausa.

        GameObject menuPausa = Instantiate(Resources.Load("Menu Pausa", typeof(GameObject))) as GameObject;


    }

    public void resumeGame()
    {
        Time.timeScale = 1.0F;

        //Destuimos el menú

        Destroy(GameObject.Find("Menu Pausa(Clone)"));
    }

}
