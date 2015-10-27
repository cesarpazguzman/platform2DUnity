using UnityEngine;
using System.Collections;

public class stickyFloor : MonoBehaviour {

    private Vector3 m_EnterScale = Vector3.one;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            Debug.Log("El player entra");
            //m_EnterScale = coll.transform.localScale;
            coll.transform.parent = transform;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.transform.parent = Managers.sceneMgr.getRootScene().transform;
            //coll.transform.localScale = m_EnterScale;
        }
    }
}
