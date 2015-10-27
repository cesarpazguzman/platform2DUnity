using UnityEngine;
using System.Collections;

public class takeClock : MonoBehaviour {

    [SerializeField]
    private int m_extraTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Si el reloj lo toca el player
        if (coll.gameObject.tag == "Player")
        {
            //Aumentamos el tiempo y lo borramos de la escena
            Managers.sceneMgr.getLevelGameObject().GetComponent<timeManager>().addTime(m_extraTime);
            Managers.spawnerMgr.destroyGameObject(this.gameObject,false);
        }
    }
}
