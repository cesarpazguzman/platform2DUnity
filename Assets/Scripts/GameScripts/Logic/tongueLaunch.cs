using UnityEngine;
using System.Collections;

public class tongueLaunch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {   
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Managers.GetInstance.GameMgr.getPlayer.transform.position;
            dir.Normalize();
            RaycastHit2D hit = Physics2D.Raycast(Managers.GetInstance.GameMgr.getPlayer.transform.position
                , dir, 1.2f, 1 << LayerMask.NameToLayer("Ground"));

            if (hit && transform.position.y < hit.point.y)
            {
                this.gameObject.GetSafeComponent<DistanceJoint2D>().connectedAnchor = new Vector2(hit.point.x, hit.point.y);
            }

            this.gameObject.GetSafeComponent<DistanceJoint2D>().enabled = (hit && transform.position.y < hit.point.y) ? true : false;
            this.gameObject.GetSafeComponent<playerController>().isDistanceJoint = (hit && transform.position.y < hit.point.y) ? true : false;
            
        }
        else if (Input.GetButtonDown("Jump"))
        {
            this.gameObject.GetSafeComponent<DistanceJoint2D>().enabled = false;
            this.gameObject.GetSafeComponent<playerController>().isDistanceJoint = false;
        }
	}
}
