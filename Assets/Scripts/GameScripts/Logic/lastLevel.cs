﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Componente que será colocado sobre el Botón next Level del menú quen os aparecerá al superar el nivel. Sirve para desactivar el botón Next Level
/// si el nivel en el que se encuentra es el último, indicándole al usuario que ya acabó el juego.
/// </summary>
public class lastLevel : MonoBehaviour {

    public Button m_button;
	// Use this for initialization
	void Start () {
	    
	}

    void OnEnable()
    {
        
    }

    //Se hace en el OnDisable para que note el cambio de interactuable a no interactuable en caso de haberlo
    void OnDisable()
    {
        m_button.interactable = true;
        if (Managers.sceneMgr.getCurrentLevel == Managers.sceneMgr.lastLevel() - 1)
        {
            m_button.interactable = false;
        }   
    }

	// Update is called once per frame
	void Update () {
	
	}
}