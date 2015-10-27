﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que gestiona el tiempo que transcurre en el juego. Se colocaría en el gameObject que representa a la escena, ya que quizás se pueda contemplar
/// que cada nivel tenga un tiempo diferente para terminar, ajustable en cada nivel.
/// </summary>
public class timeManager : MonoBehaviour 
{
    [SerializeField]
    private int m_seconds;

    //Variable que indica a partir de cuantos segundos que queden, se establece como alarma para empezar a indicarle al usuario que le queda poco tiempo
    [SerializeField]
    private int m_lastSecondsAlarm = 10;

    public int seconds
    {
        get { return m_seconds; }
        set { m_seconds = value; }
    }

    public int lastSecondsAlarm
    {
        get { return m_lastSecondsAlarm; }
        set { m_lastSecondsAlarm = value; }
    }

	// Use this for initialization
	void Start () 
    {
        StartCoroutine("updateTime");
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator updateTime()
    {
        while (m_seconds > 0) //Mientras haya tiempo
        {
            --m_seconds;

            //Aqui se lo indicariamos a la interfaz, al componente Text del elemento UI que indica el tiempo
            showTimeUI();

            //Podemos reproducir un sonido de tic tic tic de reloj, cuando queden 10 segundos para indicarle al jugador que le queda poco tambien
            //por la parte del sonido
            if (m_seconds <= m_lastSecondsAlarm)
            {

            }

            yield return new WaitForSeconds(1.0f); //Esperamos un segundo
        }

        //Si se acaba el tiempo, ejecutaríamos la funcionalidad de que se acaba el tiempo antes de que el jugador consiga el objetivo. 
        Managers.gameMgr.gameOver();
    }

    //Funcion que se llamará cuando cojamos por ejemplo un elemento de Aumento de tiempo. 
    public void addTime(int timeToAdd)
    {
        m_seconds += timeToAdd;
    }

    //Funcion que se llama desde la corutina, que sirve para mostrar el tiempo en la interfaz. 
    private void showTimeUI()
    {

        //ESTO SE CAMBIARÁ POSTERIORMENTE CUANDO EXISTE ESTE ELEMENTO DE INTERFAZ PARA EL TIEMPO. Esta sería la base

        int minutesUI = m_seconds / 60;
        int secondsUI = m_seconds % 60;
        string text = "Tiempo restate: " + ((minutesUI < 10) ? "0" : "") + minutesUI + ":" + ((secondsUI < 10) ? "0" : "") + secondsUI;
        Debug.Log(text);


        //Podemos cambiar el color cuando queden los ultimos 10 segundos por ejemplo, si esta dentro del tiempo de alarma de los ultimos
        //segundos, se pone a rojo, indicando situacion limite, sino blanco indicando normalidad.
        string color = (m_seconds > m_lastSecondsAlarm) ? "blanco" : "rojo";
        //text.color = color;

    }

}
