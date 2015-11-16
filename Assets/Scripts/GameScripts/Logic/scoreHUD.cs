using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreHUD : MonoBehaviour {

    private Text m_hud;
	// Use this for initialization
	void Start () {
        m_hud = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        m_hud.text = Managers.GetInstance.TimeMgr.getPercent().ToString() + "%";
	}
}
