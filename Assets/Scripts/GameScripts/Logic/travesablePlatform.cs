using UnityEngine;
using System.Collections;

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
        m_player = Managers.gameMgr.getPlayer;
        //Al principio de todo es un trigger...si la posicion del player acaba siendo superior, entonces ya no es trigger, en caso contrario si.
        gameObject.GetComponent<BoxCollider2D>().isTrigger = (m_player.transform.position.y > transform.position.y + m_offset) ? false : true;
	}

}
