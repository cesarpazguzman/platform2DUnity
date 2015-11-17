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
        if (coll.CompareTag("Player"))
        {
            coll.transform.parent = transform;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            coll.transform.parent = Managers.GetInstance.SceneMgr.rootScene.transform;
        }
    }
}
