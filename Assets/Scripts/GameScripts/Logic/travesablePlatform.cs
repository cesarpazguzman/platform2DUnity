using UnityEngine;
using System.Collections;

/// <summary>
/// Componente colocado sobre la plataforma atravesable, que comprueba dependiendo de la posición del player, la plataforma se convierte o no en trigger.
/// </summary>
public class travesablePlatform : MonoBehaviour {

    private float m_offset = 0.3f;
    private GameObject m_player;
	// Use this for initialization
	void Start () {
        m_player = Managers.gameMgr.getPlayer;
	}

	
	// Update is called once per frame
	void Update () 
    {
        //Al principio de todo es un trigger...si la posicion del player acaba siendo superior, entonces ya no es trigger, en caso contrario si.
        gameObject.GetComponent<BoxCollider2D>().isTrigger = (m_player.transform.position.y > transform.position.y + m_offset) ? false : true;
	}

}
